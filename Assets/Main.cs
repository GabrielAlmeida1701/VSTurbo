using UnityEngine;
using System.Collections;
using Assets;

public class Main : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Graph graph = new Graph(4);

        graph.Initialize();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}
}
