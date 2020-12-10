using UnityEngine;
using System.Collections;
using Pathfinding;

public class WanderingDestinationSetter : MonoBehaviour
{
    IAstarAI ai;

    public bool stoppedMoving;
    private Vector3 lastPosition;

    void Start()
    {
        ai = GetComponent<IAstarAI>();
        stoppedMoving = false;
        lastPosition = this.gameObject.transform.position;
        InvokeRepeating(nameof(CheckStoppedMoving), 0, 1.0f);
    }

    void CheckStoppedMoving()
    {
        if ((this.gameObject.transform.position - lastPosition).magnitude < 0.1f)
        {
            //Debug.Log("PAROU DE MOVER");
            stoppedMoving = true;
        }
        lastPosition = this.gameObject.transform.position;

    }

    Vector3 PickRandomPoint()
    {
        GraphNode randomNode;

        GridGraph grid = AstarPath.active.data.gridGraph;
        int c = 0;
        while (true && c < 1000)
        {
            randomNode = grid.nodes[Random.Range(0, grid.nodes.Length)];
            GraphNode node = AstarPath.active.GetNearest(transform.position).node;

            if (randomNode.Walkable && PathUtilities.IsPathPossible(node, randomNode))
            {
                return (Vector3)randomNode.position;
            }
            c++;

            //Debug.Log(c);

        }
        Debug.Log("ERRO: NÃO ENCONTROU RANDOMNODE EM 1000 ITERAÇÕES " + stoppedMoving);
        //ai.SetPath(null);
        return (Vector3)grid.nodes[Random.Range(0, grid.nodes.Length)].position;
    }

    void Update()
    {
        if ((!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath)) || (stoppedMoving))
        {
            ai.destination = PickRandomPoint();
            ai.SearchPath();
            stoppedMoving = false;
        }
    }



}