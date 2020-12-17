using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{

    public NPCManager Instance;

    public List<ManagedNPC> managedNPCS;

    private void Awake()
    {
        if(Instance == null)
        {
            this.Instance = this;
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
        foreach(ManagedNPC managedNPC in managedNPCS)
        {
            managedNPC.UpdateMovement();
        }
    }
}
