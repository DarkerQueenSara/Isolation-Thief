using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDetection : MonoBehaviour
{
    //Estes numeros podem ser melhorados sabendo melhor o tamanho exato do cone, que se pode ir buscar
    public float darkDistance = 9.0f;
    public float lightDistance = 14.5f;

    private NPCMovement NPC;
    private Player player;

    void Awake()
    {
        NPC = this.GetComponentInParent<NPCMovement>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Vector3 diff = (this.transform.position - other.gameObject.transform.position).normalized;
            //forward nao vai dar para o detetar no chao mas já revemos isto
            if (!Physics.Linecast(this.transform.position, other.gameObject.transform.position, out RaycastHit hit))
            {
                //Debug.DrawRay(transform.position, diff * hit.distance, Color.yellow);
                //if (hit.collider.gameObject.CompareTag("Player"))
                //{
                if ((!player.isLit && hit.distance <= darkDistance) || (player.isLit && hit.distance <= lightDistance))
                {
                    NPC.callingCops = true;
                }
                //}
            }
        }
    }

}
