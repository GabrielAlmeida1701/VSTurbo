using UnityEngine;
using System.Collections.Generic;

public class city : MonoBehaviour {
    
    public int chosenPathway;

    public List<Transform> paths = new List<Transform>();

    void Start() {
        for(int i=0; i<transform.childCount; i++)
            paths.Add(transform.GetChild(i));
    }

}
