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
    public float waitTime;
    private Factory factory;
    
	public CarStuff StartCar() {
        int indx = PlayerPrefs.GetInt("InitialCity");
        Transform go = GameObject.Find("Map").transform;
        transform.position = go.GetChild(indx).position;
		return this;
    }

	void Update(){
        if(path.Count != 0) {
		    if (!free) {
                if (forward)
                    GoForward();
                else
                    GoBack();
		    } else {
                if (crrPnt >= path.Count-1) {
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

    void OLD_GoForward() {
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

    void GoForward() {
        if (crrPnt >= path.Count-1) {
            free = true;
            print("Delivery");

            return;
        }

        transform.Translate (0, 0, speed * Time.deltaTime);

        int pathway = path[crrPnt].GetComponent<city>().chosenPathway;
        Transform look = path[crrPnt].GetComponent<city>().paths[pathway];

        transform.LookAt (look.GetChild(streetPnt));

        if (Vector3.Distance(transform.position, look.GetChild(streetPnt).position) < 1f)
            streetPnt++;

        if(streetPnt >= look.childCount) {
            int id = getIndexCity(path[crrPnt]);
            timeSpent += factory.graph.listNodes[id].time;

            streetPnt = 0;
            crrPnt++;
        }
    }
    
    void tst() {
        if (crrPnt < 0) {
            free = true;
            path.Clear();
            factory.FinishJob();
            crrPnt = 0;
	        GetTime();

            return;
        }

        int pathway = path[crrPnt].GetComponent<city>().chosenPathway;
        Transform look = path[crrPnt].GetComponent<city>().paths[pathway];

        transform.Translate (0, 0, speed * Time.deltaTime);
		transform.LookAt (path [crrPnt]);

        if (Vector3.Distance(transform.position, look.GetChild(streetPnt).position) < 1f)
            streetPnt--;

        if(streetPnt < 0) {
            int id = getIndexCity(path[crrPnt]);
            timeSpent += factory.graph.listNodes[id].time;

            streetPnt = look.childCount-1;
            crrPnt--;
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
	    GetTime();
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
    
    void GetTime(){
        string time = "time:{";
        for (int i = 0; i < factory.graph.nodesSize; i++){
            time += "[" + factory.graph.listNodes[i].time + ",";
        }
        time = time.Substring(0, time.Length - 2);
        time += "}";

        PlayerPrefs.SetString("Time", time);
    }

    void Load(){
        string time = PlayerPrefs.GetString("Time");
    }
}
