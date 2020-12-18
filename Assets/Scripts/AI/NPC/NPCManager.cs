using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{

    public static NPCManager Instance;

    private List<ManagedNPC> managedNPCS;

    public List<Transform> telephones;

    public bool copsCalled { get; private set; }

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
        if (copsCalled) return;

        foreach(ManagedNPC managedNPC in managedNPCS)
        {
            managedNPC.UpdateMovement();
        }
    }

    public void CallCops()
    {
        this.copsCalled = true;


        foreach (ManagedNPC managedNPC in this.managedNPCS)
        {
            if (managedNPC.canCallCops)
            {
                Transform closestPhoneTransform = GetClosestPhone(managedNPC);
                managedNPC.CallCops(closestPhoneTransform);
            } else
            {
                managedNPC.HideOnBedRoom();
            }
        }
    }

    Transform GetClosestPhone(ManagedNPC managedNPC)
    {
        //Debug.Log("Entrou no GetClosestPhone()");
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
        //Debug.Log("Vai retornar " + aux.position);
        return aux;
    }
}
