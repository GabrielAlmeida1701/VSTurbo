using UnityEngine;
using System.Collections;
using Assets;

public class MainGraph : MonoBehaviour {

    public Graph InitializeGraph ()
    {
        Graph graph = new Graph(4);

        graph.Initialize();

        return graph;
	}
}
