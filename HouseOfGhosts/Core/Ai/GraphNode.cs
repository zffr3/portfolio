using System.Collections.Generic;
using UnityEngine;

public class GraphNode : MonoBehaviour
{
    [SerializeField]
    private List<GraphNode> _nodes;

    public GraphNode GetNextRandomNode()
    {
        return this._nodes[Random.Range(0, this._nodes.Count)];
    }
}
