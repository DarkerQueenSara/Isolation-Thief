using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class NPCMovement
{
    protected Dictionary<string, Vector3> destinations;
    protected Vector3 currentDestination;
    protected GameObject NPC;
    protected NavMeshAgent npc_m_Agent;

    public virtual void Initialize(GameObject npc)
    {
        destinations = new Dictionary<string, Vector3>();
        foreach (Transform destination in GameObject.Find("Destinations").transform.GetComponentsInChildren<Transform>())
        {
            destinations.Add(destination.name, destination.position);
        }

        this.NPC = npc;
        npc_m_Agent = this.NPC.GetComponent<NavMeshAgent>();
    }

    public bool IsMoving()
    {
        return npc_m_Agent.hasPath;
    }

    public abstract void Move();
}
