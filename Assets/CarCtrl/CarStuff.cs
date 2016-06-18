using UnityEngine;
using System.Collections.Generic;

public class CarStuff : MonoBehaviour {

	public List<Transform> path = new List<Transform>();

	public float GasTank;
	public float mass;
	public float speed;

	public bool free;

	private int crrPnt;
	void Update(){
		if (!free) {
			transform.Translate (0, 0, speed * Time.deltaTime);
			GasTank -= 0.05f;
			transform.LookAt (path [crrPnt]);

			if (Vector3.Distance (transform.position, path [crrPnt].position) < 1f) {
				if (crrPnt >= path.Count)
					free = true;
				else
					crrPnt++;
			}
		}
	}
}
