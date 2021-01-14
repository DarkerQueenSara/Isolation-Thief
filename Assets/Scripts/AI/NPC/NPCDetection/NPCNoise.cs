using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCNoise : MonoBehaviour
{

    ManagedNPC NPC;

    public void Initialize(ManagedNPC managedNPC)
    {
        this.NPC = managedNPC;
    }

    public void Investigate(Vector3 pos, float maxDistance, int weakness, float timeToWait)
    {
        if (NPC.playerDetected) return;

        //only 1 floor
        Vector3 myPos = new Vector3(gameObject.transform.position.x,0.0f, gameObject.transform.position.z);
        Vector3 flatPlayerPos = new Vector3(pos.x, 0.0f, pos.z);

        //sneak 5,5
        //walk : maxDist = 10 ; intentsity = 10
        //sprint 15,15
        float realDistance = Vector3.Distance(flatPlayerPos, myPos);
        if(realDistance > maxDistance) { return; }

        float probability;

        if (realDistance <= maxDistance / 2) { probability = 1; }
        else
        {
            probability = 1 - (realDistance / weakness) + 0.075f;
        }

        Debug.Log("Probability: " + probability);

        int n = Random.Range(1, 100);
        if(n <= probability * 100) //Investigate
        {
            //Debug.Log("WHAT WAS THAT?");
            StartCoroutine(InvestigateSound(pos, timeToWait));

        } else // Don't investigate
        {

        }

    }

    private IEnumerator InvestigateSound(Vector3 position, float timeToWait)
    {
        //NPC.StopCoroutine(NPC.DefaultMovementWithLights());
        NPC.StopAllCoroutines(); // Do I seriosly have more than 1 co routine running of the same method?
        NPC.busy = true;
        NPCMovement npcMovement = NPC.myMovement;
        npcMovement.GoTo(position);
        while (npcMovement.PathPending())
        {
            yield return null;
        }

        // Wait until NPC reaches position
        while (!npcMovement.ReachedCurrentDestination())
        {
            yield return null;
        }

        npcMovement.Idle();
        yield return new WaitForSeconds(timeToWait);
        NPC.busy = false;
    }
}
