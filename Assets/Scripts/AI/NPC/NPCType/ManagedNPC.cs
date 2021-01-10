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
    private const float doorCloseDelay = 2f;
    public bool canCallCops;

    private bool copsCalled;

    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        var head = transform.Find("Model/Head");
        Animator managedNPC_animator = transform.Find("Model").GetComponent<Animator>();

        copsCalled = false;
        myMovement = getMovement();
        myMovement.Initialize(gameObject, managedNPC_animator);
        
        head.GetComponent<NPCVision>().Initialize(transform, head);
        audioManager = this.gameObject.GetComponent<AudioManager>();
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

        if (!myMovement.IsMoving())
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

    private IEnumerator DefaultMovementWithLights()
    {
        NPCManager npcManager = NPCManager.Instance;
        Vector3 currentDest;
        DestinationInfo dInfo;
        bool sharingDestination;

        if (myMovement.currentDestinationName != "")
        {
            //2 Get currrent destination info
            dInfo = myMovement.GetCurrentDestinationInfo();
            //3 Check if another NPC has the same destination info
            sharingDestination = npcManager.SameDestinationInfo(dInfo, this);
            //4 If not,turn the light off, by moving to its lightSwitch and setting intensity to 0. If yes, skip 4 & 5
            if (!sharingDestination && dInfo.lightSwitch.isOn())
            {
                myMovement.GoTo(dInfo.lightSwitch.transform.position);

                while (myMovement.PathPending())
                {
                    yield return null;
                }

                //5 Wait until NPC reaches the light
                while (!myMovement.ReachedCurrentDestination())
                {
                    yield return null;
                }
                dInfo.lightSwitch.interact();
            }
        }


        //TODO: Should only think about turning light on after starting to get close, otherwise an NPC there might leave,
        //      turning the light off, and this NPC won't turn it on, because it was on when he checked, and was far.

        //1 Get new destination
        myMovement.Move();
        currentDest = myMovement.GetCurrentDestination();
        //2 Find closest light to that destination
        dInfo = myMovement.GetCurrentDestinationInfo();
        //3 Check if on/off
        bool lightOn = dInfo.lightSwitch.isOn();
        sharingDestination = npcManager.SameDestinationInfo(dInfo, this);
        //4 If off, turn it on, by moving to it and setting intensity to 1
        if (!sharingDestination && !lightOn)
        {
            myMovement.GoTo(dInfo.lightSwitch.transform.position);

            while (myMovement.PathPending())
            {
                yield return null;
            }

            //5 Wait until NPC reaches the light
            while (!myMovement.ReachedCurrentDestination())
            {
                yield return null;
            }
            dInfo.lightSwitch.interact();
        }
        //6 Resume new destination
        myMovement.GoTo(currentDest);
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
        while (myMovement.FollowNPC(npc.transform))
        {
            yield return new WaitForSeconds(0.25f);
        }

        NPCManager.Instance.CallCops(npc);
    }


    public void HideOnBedRoom()
    {
        StartCoroutine(this.myMovement.HideOnBedRoom());
    }

    public void ReactScared()
    {
        myMovement.ReactScared();
    }
}
