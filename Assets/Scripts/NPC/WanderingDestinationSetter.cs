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

    void Start()
    {
        ai = GetComponent<IAstarAI>();
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

        if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath))
        {
            ai.destination = PickRandomPoint();
            ai.SearchPath();
        }
    }



}