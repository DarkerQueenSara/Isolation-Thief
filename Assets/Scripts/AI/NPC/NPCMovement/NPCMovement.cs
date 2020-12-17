using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class NPCMovement : MonoBehaviour
{
    protected static Dictionary<string, Vector3> destinations;
    protected Vector3 currentDestination;
    protected GameObject NPC;
    protected NavMeshAgent npc_m_Agent;

    private float openDoorDist = 3f;
    private float doorCloseDelay = 2f;

    public virtual void Initialize(GameObject npc)
    {
        destinations = new Dictionary<string, Vector3>();
        foreach (GameObject destination in GameObject.FindGameObjectsWithTag("NPCDestination"))
        {
            destinations.Add(destination.name, destination.transform.position);
        }

        this.NPC = npc;
        npc_m_Agent = this.NPC.GetComponent<NavMeshAgent>();
    }

    public bool IsMoving()
    {
        return npc_m_Agent.hasPath;
    }

    public abstract void Move();

    //return Animator of door if found a door in path close to it and opened it
    public virtual void checkForDoor()
    {

        if (Physics.Linecast(NPC.transform.position, npc_m_Agent.steeringTarget, out RaycastHit hit, 1 << 11))
        {
            //There is a CLOSED door on the path
            if(hit.distance < openDoorDist)
            {
                Animator doorAnim = hit.collider.gameObject.GetComponent<Animator>();
                doorAnim.SetBool("isOpenDoor", true);
                StartCoroutine(CloseDoor(doorAnim, doorCloseDelay));
            }
        }
    }
    private IEnumerator CloseDoor(Animator doorAnim, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        bool isOpen = doorAnim.GetBool("isOpenDoor");
        if (isOpen)
        {
            doorAnim.SetBool("isOpenDoor", false);

        }
    }

}
