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
    public Transform lastKnownPlayerPosition = null;
    public Transform lastKnownDistractionPosition = null;

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
        if (patroler.isActiveAndEnabled)
        {
            patrolTimeLeft -= Time.deltaTime;

        }
        /*else
        {
            patrolTimeLeft = patrolTime;
        }*/

        if (wanderer.isActiveAndEnabled)
        {
            wanderTimeLeft -= Time.deltaTime;
        }
        /*else
        {
            wanderTimeLeft = wanderTime;
        }*/

        //Ele sabe onde está o player
        if (lastKnownPlayerPosition != null)
        {
            //Ele nao persegue o player tenho de trocar isto para ir ao telefone
            GoTo(lastKnownPlayerPosition);
        }
        //Ele não sabe onde está o player, mas sabe do último barulho
        else if (lastKnownDistractionPosition != null)
        {
            GoTo(lastKnownDistractionPosition);
        }
        //Ele não sabe onde está o player, já chegou ao barulho vai patrulhar a área
        else if (patrolTimeLeft > 0)
        {
            Patrol();
        }
        //Desistiu vai andar pela casa um bocado
        else if (wanderTimeLeft > 0)
        {
            Wander();
        }
        //OK, de volta para a cama
        else
        {
            GoTo(spawnPoint);
        }

    }

    void GoTo(Transform target)
    {
        patroler.enabled = false;
        wanderer.enabled = false;
        setter.enabled = true;
        setter.target = target;
        //patrolTimeLeft = patrolTime;
        //wanderTimeLeft = wanderTime;
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

    void Wander()
    {
        patroler.enabled = false;
        setter.enabled = false;
        wanderer.enabled = true;
    }
}
