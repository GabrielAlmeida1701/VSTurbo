using UnityEngine;
using System.Collections.Generic;

public class CarStuff : MonoBehaviour {

	public List<Transform> path = new List<Transform>();

	public float GasTank;
	public float mass;
	public float speed;

	public bool free;
    public bool forward;

    private int crrPnt;
    private float waitTime;

	void Update(){
        if(path.Count != 0) {
		    if (!free) {
                if (forward)
                    GoForward();
                else
                    GoBack();
		    } else {
                if (crrPnt >= path.Count) {
                    waitTime += Time.deltaTime;
                    if(waitTime >= 5) {
                        forward = false;
                        free = false;
                        crrPnt--;
                        waitTime = 0;
                    }
                }
            }
        }

	}

    void GoForward() {
        if (crrPnt >= path.Count) {
            free = true;
            print("Delivery");

            return;
        }

        transform.Translate (0, 0, speed * Time.deltaTime);
		transform.LookAt (path [crrPnt]);

        if (Vector3.Distance(transform.position, path[crrPnt].position) < 1f)
            crrPnt++;
    }

    void GoBack() {
        if (crrPnt < 0) {
            free = true;
            path.Clear();
            print("BackToBase");

            return;
        }

        transform.Translate (0, 0, speed * Time.deltaTime);
		transform.LookAt (path [crrPnt]);

        if (Vector3.Distance(transform.position, path[crrPnt].position) < 1f)
            crrPnt--;
    }
}
