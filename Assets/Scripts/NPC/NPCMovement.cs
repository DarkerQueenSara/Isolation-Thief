using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
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

    //Ir direto a uma direçao
    private AIPath pathfinder;

    //SETTERS DE DESTINO (só um ligado)    
    //Ir em volta de um array de targets
    private Patrol patroler;
    //Escolher ao calhas à volta de um raio
    private WanderingDestinationSetter wanderer;
    //Ir direto
    private AIDestinationSetter setter;

    [SerializeField]
    public List<PatrolTargets> roomsToPatrol;
    public List<Transform> telephones;

    // Start is called before the first frame update
    void Start()
    {
        pathfinder = this.GetComponent<AIPath>();
        patroler = this.GetComponent<Patrol>();
        wanderer = this.GetComponent<WanderingDestinationSetter>();
        setter = this.GetComponent<AIDestinationSetter>();
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

        //Se for preciso tenho de tirar o tempo dos timers
        if (patroler.isActiveAndEnabled)
        {
            patrolTimeLeft -= Time.deltaTime;
        }
        if (wanderer.isActiveAndEnabled)
        {
            wanderTimeLeft -= Time.deltaTime;
        }

        //Chamou os policias vai barricar-se no quarto
        if (GameManager.Instance.copsCalled)
        {
            //Debug.Log("Policias chamados");
            GoTo(spawnPoint);
            patrolTimeLeft = patrolTime;
            wanderTimeLeft = wanderTime;
        }
        //Ele sabe onde está o player e vai chamar a policia
        else if (callingCops)
        {
            //Debug.Log("Vai chamar policias");
            GoTo(GetClosestPhone());
            patrolTimeLeft = patrolTime;
            wanderTimeLeft = wanderTime;
        }
        //Ele não sabe onde está o player, mas sabe do último barulho
        else if (lastKnownDistractionPosition != null)
        {
            Debug.Log("Sabe de um som (de alguma forma)");
            GoTo(lastKnownDistractionPosition);
            patrolTimeLeft = patrolTime;
            wanderTimeLeft = wanderTime;
        }
        //Ele não sabe onde está o player, já chegou ao barulho vai patrulhar a área
        else if (patrolTimeLeft > 0)
        {
            Debug.Log("Vai patrulhar (de alguma forma)");
            Patrol();
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

    void GoTo(Transform target)
    {
        patroler.enabled = false;
        wanderer.enabled = false;
        setter.enabled = true;
        setter.target = target;
    }

    void Patrol()
    {
        wanderer.enabled = false;
        setter.enabled = false;
        patroler.enabled = true;
        patroler.targets = GetClosestRoom().targets;
    }

    PatrolTargets GetClosestRoom()
    {
        Vector3 NPC = this.gameObject.transform.position;
        PatrolTargets aux = null;

        foreach (PatrolTargets item in roomsToPatrol)
        {
            if (aux == null)
            {
                aux = item;
            }
            else if (Vector3.Distance(NPC, item.center.position) < Vector3.Distance(NPC, aux.center.position))
            {
                aux = item;
            }
        }

        return aux;
    }

    Transform GetClosestPhone()
    {
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
        return aux;
    }

    void Wander()
    {
        patroler.enabled = false;
        setter.enabled = false;
        wanderer.enabled = true;
    }
}
