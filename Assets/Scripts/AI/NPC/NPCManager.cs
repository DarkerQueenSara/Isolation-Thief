using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{

    public static NPCManager Instance;

    private List<ManagedNPC> managedNPCS;

    public List<Transform> telephones;

    public Lockpickable bedroomDoor;

    private bool stop = false;

    public bool CopsCalled { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        this.managedNPCS = new List<ManagedNPC>();
    }

    void Start()
    {

        var npcs = transform.Find("NPCS");

        foreach(ManagedNPC managedNPC in npcs.GetComponentsInChildren<ManagedNPC>())
        {
            managedNPCS.Add(managedNPC);
        }
    }



    void Update()
    {
        if (stop) return;

       // if (CopsCalled) return;

        foreach(ManagedNPC managedNPC in managedNPCS)
        {
            managedNPC.UpdateMovement();
        }
    }

    public void CallCops(ManagedNPC npc)
    {
        this.CopsCalled = true;
        Transform closestPhoneTransform = GetClosestPhone(npc);
        npc.CallCops(closestPhoneTransform);

        foreach (ManagedNPC managedNPC in this.managedNPCS)
        {
            if (managedNPC != npc)
            {
                managedNPC.HideOnBedRoom();
            }
        }
        this.bedroomDoor.isLocked = true;
    }

    public void WarnOtherNPC(ManagedNPC npc)
    {
        ManagedNPC closestNPC = GetClosestNPCWhoCanCallCops(npc);
        StartCoroutine(npc.WarnOtherNPC(closestNPC));
    }

    public bool SameDestinationInfo(DestinationInfo dInfo, ManagedNPC npc)
    {
        bool result = false;

        foreach (ManagedNPC managedNPC in this.managedNPCS)
        {
            if (managedNPC == npc) continue;

            if(managedNPC.myMovement.currentDestinationName != "" && managedNPC.myMovement.GetCurrentDestinationInfo().lightSwitch == dInfo.lightSwitch)
            {
                result = true;
            }
        }
        return result;
    }

    public void StopAllNPC()
    {
        this.stop = true;

        NPCMovement.destinations = null;
        NPCMovement.destinationsInfo = null;

        foreach(ManagedNPC managedNPC in this.managedNPCS)
        {
            managedNPC.StopAllCoroutines();
        }
    }

    ManagedNPC GetClosestNPCWhoCanCallCops(ManagedNPC managedNPC)
    {
        Vector3 managedNPCPosition = managedNPC.transform.position;

        ManagedNPC auxNPC = null;
        Transform aux = null;

        foreach (ManagedNPC npc in this.managedNPCS)
        {
            if (!npc.canCallCops) continue;

            if (aux == null)
            {
                auxNPC = npc;
                aux = npc.transform;
            }
            else if (Vector3.Distance(managedNPCPosition, npc.transform.position) < Vector3.Distance(managedNPCPosition, aux.position))
            {
                auxNPC = npc;
                aux = npc.transform;
            }
        }

        return auxNPC;
    }

    Transform GetClosestPhone(ManagedNPC managedNPC)
    {
        Vector3 managedNPCPosition = managedNPC.transform.position;
        Transform aux = null;

        foreach (Transform item in telephones)
        {
            if (aux == null)
            {
                aux = item;
            }
            else if (Vector3.Distance(managedNPCPosition, item.position) < Vector3.Distance(managedNPCPosition, aux.position))
            {
                aux = item;
            }
        }

        return aux;
    }

    public void InvestigateSound(Vector3 playerPos,float maxDistance, int intensity)
    {
        foreach(ManagedNPC managedNPC in this.managedNPCS)
        {
            managedNPC.npcNoise.Investigate(playerPos, maxDistance, intensity);
        }
        
    }
}
