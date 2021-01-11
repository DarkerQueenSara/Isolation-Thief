using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomPatrol : NPCMovement
{
    static System.Random random;

    public override void Initialize(GameObject npc, Animator managedNPC_animator, AudioManager audioSource)
    {
        base.Initialize(npc, managedNPC_animator, audioSource);
        random = new System.Random();
    }

    public override void SetNewDestination()
    {
        int randomN = random.Next(0,destinations.Count);
        string newDestName = destinations.ElementAt(randomN).Key;
        Vector3 newDest = destinations.ElementAt(randomN).Value;

        while (newDestName == this.currentDestinationName)
        {
            randomN = random.Next(0, destinations.Count);
            newDestName = destinations.ElementAt(randomN).Key;
            newDest = destinations.ElementAt(randomN).Value;
        }

        this.currentDestinationName = newDestName;
        this.currentDestination = newDest;
    }
}
