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

    // Start is called before the first frame update
    void Start()
    {
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
             myMovement.checkForDoor();
        }
    }

}
