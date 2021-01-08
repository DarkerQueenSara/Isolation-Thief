using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCVision : MonoBehaviour
{
    Transform npc_transform;
    Transform npcHead_transform;
    ManagedNPC NPC;
    private int obstacleLayerMask; //1 << 9 | 1 << 11; // 9 because obstacle layer is number 9 on inspector
    private bool detectedPlayer;

    public void Initialize(Transform npc_transform, Transform npc_head)
    {
        obstacleLayerMask = LayerMask.GetMask("Player");
        this.npc_transform = npc_transform;
        this.npcHead_transform = npc_head;
        this.NPC = npc_transform.GetComponent<ManagedNPC>();
        this.detectedPlayer = false;
    }

    public void OnTriggerStay(Collider other)
    {
        if (detectedPlayer) return;

        if (other.CompareTag("Player")) //check for player
        {
            RaycastHit hit;
            bool gotHit = Physics.Linecast(
                npc_transform.position + new Vector3(0f, 1.5f, 0f),
                other.transform.position + new Vector3(0f, 1.5f, 0f),
                out hit);

            if (gotHit && !hit.transform.CompareTag("Player"))
            {
                return;
            }
            else
            {

                if (Player.Instance.isLit)
                {
                    DetectPlayer(other);
                } else
                {
                    if(hit.distance <= 4f)
                    {
                        DetectPlayer(other);
                    }
                }
            }
        }
    }

    private void DetectPlayer(Collider other)
    {
        Debug.Log("I found the player");
        detectedPlayer = true;
        LevelManager.Instance.timesDetected++;
        StartCoroutine(reactScared(other.transform));
    }

    private IEnumerator reactScared(Transform toLookAt)
    {
        NavMeshAgent agent = npc_transform.GetComponent<NavMeshAgent>();
        agent.isStopped = true;

        NPC.ReactScared();
        npcHead_transform.LookAt(toLookAt);
        yield return new WaitForSeconds(3.0f);
        agent.isStopped = false;
        //Look at player, and scared animation

        if (NPC.canCallCops)
        {
            NPCManager.Instance.CallCops(NPC);
        } else
        {
            NPCManager.Instance.WarnOtherNPC(NPC);
        }
    }

}
