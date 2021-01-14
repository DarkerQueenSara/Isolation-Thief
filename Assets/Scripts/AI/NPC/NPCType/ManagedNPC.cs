using Assets.Scripts.Player.Controls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagedNPC : MonoBehaviour
{
    public enum MovementType
    {
        RandomPatrol,

    }
    //Set from outside
    public MovementType movementType;

    [HideInInspector] public NPCMovement myMovement;

    public NPCNoise npcNoise;
    public NPCVision npcVision;

    private const float doorCloseDelay = 2f;
    public bool canCallCops;

    [HideInInspector] public bool playerDetected = false;
    [HideInInspector] public bool busy = false;

    // Start is called before the first frame update
    void Start()
    {
        var head = transform.Find("Model/Head");
        Animator managedNPC_animator = transform.Find("Model").GetComponent<Animator>();

        myMovement = getMovement();
        myMovement.Initialize(gameObject, managedNPC_animator, this.gameObject.GetComponent<AudioManager>());

        npcVision = head.GetComponent<NPCVision>();
        npcVision.Initialize(transform, head);

        npcNoise = gameObject.GetComponent<NPCNoise>();
        npcNoise.Initialize(this);
    }
    private NPCMovement getMovement()
    {
        switch (movementType)
        {
            case MovementType.RandomPatrol:
                return new RandomPatrol();
            default:
                return new RandomPatrol();
        }
    }

    // Update is called once per frame
    public void UpdateMovement()
    {

        if (!busy && !myMovement.IsMoving() && !NPCManager.Instance.CopsCalled)
        {
            StartCoroutine(DefaultMovementWithLights());
            //myMovement.Move();
        } else
        {
            Animator doorAnim = myMovement.checkForDoor();
            if(doorAnim != null)
            {
                StartCoroutine(CloseDoor(doorAnim, doorCloseDelay));
            }
        }
    }

    public IEnumerator DefaultMovementWithLights()
    {
        this.busy = true;
        NPCManager npcManager = NPCManager.Instance;

        Vector3 currentDest;
        Vector3 newDest;
        DestinationInfo currentdInfo = null;
        DestinationInfo newdInfo = null;

        bool sharingDestination;

        if(myMovement.currentDestinationName != "")
        {
            currentDest = myMovement.GetCurrentDestination();
            currentdInfo = myMovement.GetCurrentDestinationInfo();
        }

        myMovement.SetNewDestination();

        newDest = myMovement.GetCurrentDestination();
        newdInfo = myMovement.GetCurrentDestinationInfo();

        if (currentdInfo != null && newdInfo.lightSwitch != currentdInfo.lightSwitch)
        {
            sharingDestination = npcManager.SameDestinationInfo(currentdInfo, this);



            if (!sharingDestination && currentdInfo.lightSwitch.isOn())
            {
                myMovement.GoTo(currentdInfo.lightSwitch.transform.position);

                while (myMovement.PathPending())
                {
                    yield return null;
                }

                // Wait until NPC reaches the light
                while (!myMovement.ReachedCurrentDestination())
                {
                    yield return null;
                }
                currentdInfo.lightSwitch.interact();
            }
        }


        //TODO: Should only think about turning light on after starting to get close, otherwise an NPC there might leave,
        //      turning the light off, and this NPC won't turn it on, because it was on when he checked, and was far.

        sharingDestination = npcManager.SameDestinationInfo(newdInfo, this);

        if (!sharingDestination && !newdInfo.lightSwitch.isOn() )
        {
            myMovement.GoTo(newdInfo.lightSwitch.transform.position);

            while (myMovement.PathPending())
            {
                yield return null;
            }

            //Wait until NPC reaches the light
            while (!myMovement.ReachedCurrentDestination())
            {
                yield return null;
            }
            newdInfo.lightSwitch.interact();
        }
        //Resume new destination
        myMovement.GoTo(newDest);
        while (myMovement.PathPending())
        {
            yield return null;
        }

        //Wait until NPC reaches the light
        while (!myMovement.ReachedCurrentDestination())
        {
            yield return null;
        }

        myMovement.Idle();
        yield return new WaitForSeconds(3f);

        busy = false;
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

    public void CallCops(Transform closestPhoneTransform)
    {
        StopAllCoroutines();
        this.myMovement.RunTo(closestPhoneTransform.position);
        StartCoroutine(DoCallCops());

    }

    private IEnumerator DoCallCops()
    {
        while (myMovement.IsMoving())
        {
            yield return null;
        }

        CallPhone.Instance.CallPolice();

        while (!LevelManager.Instance.copsCalled)
        {
            yield return null;
        }

        StartCoroutine(this.myMovement.HideOnBedRoom());
        //this.HideOnBedRoom();
    }

    public IEnumerator WarnOtherNPC(ManagedNPC npc)
    {
        StopAllCoroutines();
        while (myMovement.FollowNPC(npc.transform))
        {
            yield return new WaitForSeconds(0.25f);
        }

        NPCManager.Instance.CallCops(npc);
    }


    public void HideOnBedRoom()
    {
        StopAllCoroutines();
        StartCoroutine(this.myMovement.HideOnBedRoom());
    }

    public void ReactScared()
    {
        myMovement.ReactScared();
    }
}
