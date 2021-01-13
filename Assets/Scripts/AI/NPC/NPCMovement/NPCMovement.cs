using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class NPCMovement
{
    public static Dictionary<string, Vector3> destinations;
    public static Dictionary<string, DestinationInfo> destinationsInfo;

    public string currentDestinationName = "";
    protected Vector3 currentDestination;
    protected GameObject NPC;
    protected NavMeshAgent npc_m_Agent;
    protected Animator managedNPC_animator;

    protected float openDoorDist = 3f;

    public float walkSpeed = 2.5f;
    public float runSpeed = 4f;

    private AudioManager audioManager;

    public virtual void Initialize(GameObject npc, Animator managedNPC_animator, AudioManager audioManager)
    {
        if(destinations == null && destinationsInfo == null)
        {
            destinations = new Dictionary<string, Vector3>();
            destinationsInfo = new Dictionary<string, DestinationInfo>();
            foreach (GameObject destination in GameObject.FindGameObjectsWithTag("NPCDestination"))
            {
                destinations.Add(destination.name, destination.transform.position);
                DestinationInfo dInfo = destination.GetComponent<DestinationInfo>();
                destinationsInfo.Add(destination.name, destination.GetComponent<DestinationInfo>());
            }
        }
        this.audioManager = audioManager;
        this.NPC = npc;
        npc_m_Agent = this.NPC.GetComponent<NavMeshAgent>();
        this.managedNPC_animator = managedNPC_animator;
    }

    public bool IsMoving()
    {
        return npc_m_Agent.hasPath;
    }

    public abstract void SetNewDestination();

    //return Animator of door if found a door in path close to it and opened it
    public virtual Animator checkForDoor()
    {

        if (Physics.Linecast(NPC.transform.position, npc_m_Agent.steeringTarget, out RaycastHit hit, 1 << 11))
        {
            //There is a CLOSED door on the path
            if(hit.distance < openDoorDist)
            {
                Animator doorAnim = hit.collider.gameObject.GetComponent<Animator>();
                doorAnim.SetBool("isOpenDoor", true);
                return doorAnim;
            }
        }

        return null;
    }

    public void GoTo(Vector3 position)
    {
        audioManager.Play("Walk");
        this.npc_m_Agent.speed = walkSpeed;
        this.managedNPC_animator.SetFloat("Speed", walkSpeed);
        this.npc_m_Agent.destination = position;
    }

    public void RunTo(Vector3 position)
    {
        audioManager.Play("Sprint");
        this.npc_m_Agent.speed = runSpeed;
        this.managedNPC_animator.SetFloat("Speed", runSpeed);
        this.npc_m_Agent.destination = position;
    }

    public void Idle()
    {
        this.npc_m_Agent.speed = 0;
        this.managedNPC_animator.SetFloat("Speed", 0);
    }

    public IEnumerator HideOnBedRoom()
    {
        
        this.npc_m_Agent.speed = runSpeed;
        this.managedNPC_animator.SetFloat("Speed",runSpeed);

        
        this.npc_m_Agent.destination = destinations["BedDestination"];
        while (this.npc_m_Agent.pathPending || npc_m_Agent.remainingDistance > 0.3f)
        {
            yield return null;
        }
        StopMoving();
    }

    public bool FollowNPC(Transform npcTransform)
    {
        RunTo(npcTransform.position);
        return (npc_m_Agent.remainingDistance > 1f);
    }

    public void StopMoving()
    {
        audioManager.StopAll();
        this.npc_m_Agent.speed = 0;
        this.managedNPC_animator.SetFloat("Speed", 0);
    }

    public void ReactScared()
    {
        this.managedNPC_animator.SetTrigger("React");
    }

    public Vector3 GetCurrentDestination()
    {
        return this.currentDestination;
    }

    public DestinationInfo GetCurrentDestinationInfo()
    {
        return destinationsInfo[currentDestinationName];
    }

    public bool PathPending()
    {
        return this.npc_m_Agent.pathPending;
    }

    public bool ReachedCurrentDestination()
    {
        return npc_m_Agent.remainingDistance <= 0.3f;
    }
}
