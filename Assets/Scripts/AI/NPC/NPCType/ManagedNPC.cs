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

    private NPCMovement myMovement;
    private const float doorCloseDelay = 2f;
    public bool canCallCops;
    private NPCManager NPCManager;

    private bool copsCalled;

    // Start is called before the first frame update
    void Start()
    {
        this.NPCManager = NPCManager.Instance;
        copsCalled = false;
        myMovement = getMovement();
        myMovement.Initialize(gameObject);
        var head = transform.Find("Capsule").Find("Head");
        head.GetComponent<NPCVision>().Initialize(transform, head);
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
            myMovement.Move();
        } else
        {
            Animator doorAnim = myMovement.checkForDoor();
            if(doorAnim != null)
            {
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

    public void CallCops(Transform closestPhoneTransform)
    {
        this.myMovement.GoTo(closestPhoneTransform.position);
        StartCoroutine(DoCallCops());

    }

    private IEnumerator DoCallCops()
    {
        while (myMovement.IsMoving())
        {
            yield return null;
        }

        CallPhone.Instance.CallPolice();

        while (!GameManager.Instance.copsCalled)
        {
            yield return null;
        }

        this.myMovement.HideOnBedRoom();
    }

    public void HideOnBedRoom()
    {
        this.myMovement.HideOnBedRoom();
    }
}
