using UnityEngine;
using System.Collections.Generic;

public class CarStuff : MonoBehaviour {

	public List<Transform> path = new List<Transform>();

	public float GasTank;
	public float timeSpent;
	public float speed;

	public bool free;
    public bool forward;

    private int crrPnt;
    private int streetPnt;
    private float waitTime;
    private Factory factory;

    void Start() {
        int indx = PlayerPrefs.GetInt("InitialCity");
        Transform go = GameObject.Find("Map").transform;
        transform.position = go.GetChild(indx).position;
    }

	void Update(){
        if(path.Count != 0) {
		    if (!free) {
                if (forward)
                    tst();
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

        if (Vector3.Distance(transform.position, path[crrPnt].position) < 1f) {
            int id = getIndexCity(path[crrPnt]);
            timeSpent += factory.graph.listNodes[id].time;
            print(factory.graph.listNodes[id].time);
            crrPnt++;
        }
    }

    void tst() {
        if (crrPnt >= path.Count) {
            free = true;
            print("Delivery");

            return;
        }

        transform.Translate (0, 0, speed * Time.deltaTime);

        int pathway = path[crrPnt].GetComponent<city>().chosenPathway;
        Transform look = path[crrPnt].GetComponent<city>().paths[pathway];

        transform.LookAt (look.GetChild(streetPnt));

        if (Vector3.Distance(transform.position, look.GetChild(streetPnt).position) < 1f) {
            streetPnt++;
        }

        if(streetPnt >= look.childCount) {
            int id = getIndexCity(path[crrPnt]);
            timeSpent += factory.graph.listNodes[id].time;

            streetPnt = 0;
            crrPnt++;
        }
    }

    private int getIndexCity(Transform pnt) {
        string index = "";
        if (pnt.name.IndexOf("(") != -1) {
            int str = pnt.name.IndexOf("(");
            int end = pnt.name.IndexOf(")");
            index = pnt.name.Substring(str).Replace("(", "").Replace(")", "");
        } else
            index = "0";

        return int.Parse(index);
    }

    void GoBack() {
        if (crrPnt < 0) {
            free = true;
            path.Clear();
            factory.FinishJob();
            crrPnt = 0;

            return;
        }

        transform.Translate (0, 0, speed * Time.deltaTime);
		transform.LookAt (path [crrPnt]);

        if (Vector3.Distance(transform.position, path[crrPnt].position) < 1f) {
            int id = getIndexCity(path[crrPnt]);
            timeSpent += factory.graph.listNodes[id].time;
            print(factory.graph.listNodes[id].time);
            crrPnt--;
        }
    }

    public CarStuff SetFactory(Factory factory) {
        this.factory = factory;
        return this;
    }
}
