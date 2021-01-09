using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{

    public static NPCManager Instance;

    private List<ManagedNPC> managedNPCS;

    public List<Transform> telephones;

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
        if (CopsCalled) return;

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
}
