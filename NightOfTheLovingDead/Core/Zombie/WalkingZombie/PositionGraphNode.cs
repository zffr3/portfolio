using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionGraphNode : MonoBehaviour
{
    [SerializeField]
    private List<PositionGraphNode> _graph;

    public PositionGraphNode GetRandomPositionFromGraph()
    {
        this.transform.rotation = new Quaternion(0,Random.Range(0,180),0,0);

        if (this._graph == null || this._graph.Count != 0)
            return this._graph[Random.Range(0,this._graph.Count)];

        return this;
    }
}
