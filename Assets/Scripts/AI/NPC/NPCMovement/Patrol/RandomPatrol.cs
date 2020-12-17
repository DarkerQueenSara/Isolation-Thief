using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomPatrol : NPCMovement
{
    System.Random random;

    public override void Initialize(GameObject npc)
    {
        base.Initialize(npc);
        random = new System.Random();
    }

    public override void Move()
    {
        //this assumes the NPC is a navMeshAgent
        int randomN = random.Next(0,destinations.Count);
        this.currentDestination = destinations.ElementAt(randomN).Value;
        this.npc_m_Agent.destination = this.currentDestination;
    }
}
