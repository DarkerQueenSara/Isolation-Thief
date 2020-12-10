using Assets.Scripts.Player.Controls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDetection : MonoBehaviour
{
    //Estes numeros podem ser melhorados sabendo melhor o tamanho exato do cone, que se pode ir buscar
    public float darkDistance = 9.0f;
    public float lightDistance = 14.5f;
    //Se calhar temos que ter diferentes para portas diferentes, e por Tags diferentes para cada tipo de porta
    //Mas pronto isto é só para portas pequenas nao abriem prematuramente, não é o fim do mundo
    public float doorRadius = 6f;
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
            if (!Physics.Linecast(this.transform.position, other.gameObject.transform.position, out RaycastHit hit))
            {
                if ((!player.isLit && hit.distance <= darkDistance) || (player.isLit && hit.distance <= lightDistance))
                {
                    NPC.callingCops = true;
                }
            }
        } 
    }

    private void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            if (hit.collider.gameObject.CompareTag("Door") && hit.distance < doorRadius)
            {
                hit.collider.gameObject.GetComponent<Animator>().SetBool("isOpenDoor", true);
            }
        }
    }

}
