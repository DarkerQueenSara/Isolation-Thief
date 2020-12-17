using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCVision : MonoBehaviour
{
    Transform npc_transform;
    Transform npcHead_transform;
    private int obstacleLayerMask = 1 << 9 | 1 << 11; // 9 because obstacle layer is number 9 on inspector


    public void Initialize(Transform npc_transform, Transform npc_head)
    {
        this.npc_transform = npc_transform;
        this.npcHead_transform = npc_head;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) //check for player
        {
            if (Physics.Linecast(npc_transform.position, other.transform.position, out RaycastHit hit, obstacleLayerMask))
            {
                //hit obstacle
                //Debug.Log(hit.transform.name); //whatever just show the name of what was hit
                return;
            }
            else
            {
                Debug.Log("I found the player");
                //TODO: Stop moving for 0.5 seconds, look at player, and run to call    cops
                //npcHead_transform.LookAt(other.transform);
            }
        }
    }
}
