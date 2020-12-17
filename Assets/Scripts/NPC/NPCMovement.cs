using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovementOld : MonoBehaviour
{

    public int patrolTime;
    public int wanderTime;

    private float patrolTimeLeft;
    private float wanderTimeLeft;

    //Passar dos filhos
    public BoxCollider shadowBox;
    public BoxCollider lightBox;

    //O y ser 1000 vai ser impossivel, como não há nulls este é o workaround 
    public Transform spawnPoint;
    public Transform lastKnownDistractionPosition = null;

    public bool callingCops = false;

    public List<Transform> telephones;

    // Start is called before the first frame update
    void Start()
    {
        patrolTimeLeft = patrolTime;
        wanderTimeLeft = wanderTime;
    }

    // Update is called once per frame
    void Update()
    {
        //Se chegou ao barulho tenho de voltar a por null
        if (lastKnownDistractionPosition != null && Vector3.Distance(this.transform.position, lastKnownDistractionPosition.position) < 1)
        {
            lastKnownDistractionPosition = null;
        }

        //Chamou os policias vai barricar-se no quarto
        if (GameManager.Instance.copsCalled)
        {
            //Debug.Log("Policias chamados");
            patrolTimeLeft = patrolTime;
            wanderTimeLeft = wanderTime;

        }
        //Ele sabe onde está o player e vai chamar a policia
        else if (callingCops)
        {
            //Debug.Log("Vai chamar policias");
            
            patrolTimeLeft = patrolTime;
            wanderTimeLeft = wanderTime;
        }
        //Ele não sabe onde está o player, mas sabe do último barulho
        else if (lastKnownDistractionPosition != null)
        {
            //Debug.Log("Sabe de um som (de alguma forma)");
            patrolTimeLeft = patrolTime;
            wanderTimeLeft = wanderTime;
        }
        //Ele não sabe onde está o player, já chegou ao barulho vai patrulhar a área
        else if (patrolTimeLeft > 0)
        {
            //Debug.Log("Vai patrulhar (de alguma forma)");

        }
        //Desistiu vai andar pela casa um bocado
        else //if (wanderTimeLeft > 0)
        {
            Wander();
        }
        //OK, de volta para a cama
        /*
        else
        {
            GoTo(spawnPoint);
        }
        */
    }


    Transform GetClosestPhone()
    {
        //Debug.Log("Entrou no GetClosestPhone()");
        Vector3 NPC = this.gameObject.transform.position;
        Transform aux = null;

        foreach (Transform item in telephones)
        {
            if (aux == null)
            {
                aux = item;
            }
            else if (Vector3.Distance(NPC, item.position) < Vector3.Distance(NPC, aux.position))
            {
                aux = item;
            }
        }
        //Debug.Log("Vai retornar " + aux.position);
        return aux;
    }

    void Wander()
    {
    }
}
