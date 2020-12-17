using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToPoint : MonoBehaviour
{
    public Transform point;
    NavMeshAgent m_Agent;
    RaycastHit m_HitInfo = new RaycastHit();

    void Start()
    {
        
        m_Agent = GetComponent<NavMeshAgent>();
        m_Agent.destination = point.position;
    }

}
