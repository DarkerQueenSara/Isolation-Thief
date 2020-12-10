using UnityEngine;
using System.Collections;
using Pathfinding;

public class WanderingDestinationSetter : MonoBehaviour
{
    /*
    public float radius = 20;

    IAstarAI ai;

    void Start()
    {
        ai = GetComponent<IAstarAI>();
    }

    Vector3 PickRandomPoint()
    {
        var point = Random.insideUnitSphere * radius;

        point.y = 0;
        point += ai.position;
        return point;
    }

    void Update()
    {
        // Update the destination of the AI if
        // the AI is not already calculating a path and
        // the ai has reached the end of the path or it has no path at all
        if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath))
        {
            ai.destination = PickRandomPoint();
            ai.SearchPath();
        }
    }
    */

    IAstarAI ai;

    public bool stoppedMoving;
    private Vector3 lastPosition;

    void Start()
    {
        ai = GetComponent<IAstarAI>();
        stoppedMoving = false;
        lastPosition = this.gameObject.transform.position;
        InvokeRepeating("CheckStoppedMoving", 0, 1.0f);

    }

    void CheckStoppedMoving()
    {
        if ((this.gameObject.transform.position - lastPosition).magnitude < 1f)
        {
            Debug.Log("PAROU DE MOVER");
            stoppedMoving = true;
        }        
        lastPosition = this.gameObject.transform.position;

    }

    Vector3 PickRandomPoint()
    {
        GraphNode randomNode;

        GridGraph grid = AstarPath.active.data.gridGraph;

        while (true)
        {
            randomNode = grid.nodes[Random.Range(0, grid.nodes.Length)];
            GraphNode node = AstarPath.active.GetNearest(transform.position).node;

            if (randomNode.Walkable && PathUtilities.IsPathPossible(node, randomNode)){
                return (Vector3)randomNode.position;
            }
        }
        return Vector3.zero;

    }

    void Update()
    {
        /*
        if (this.gameObject.transform == lastPosition)
        {
            Debug.Log("PAROU DE MOVER");
            stoppedMoving = true;
        }
        */

        if ((!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath)) || (stoppedMoving))
        {
            ai.destination = PickRandomPoint();
            ai.SearchPath();
            stoppedMoving = false;
        }
    }



}