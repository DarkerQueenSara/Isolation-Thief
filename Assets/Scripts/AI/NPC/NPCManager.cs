using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public enum MovementType
    {
        RandomPatrol,

    }

    //Set from outside
    public MovementType movementType;

    private NPCMovement myMovement;

    void Start()
    {
        myMovement = getMovement();
        myMovement.Initialize(gameObject);
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

    void Update()
    {
        if (!myMovement.IsMoving())
        {
            myMovement.Move();
        }
    }
}
