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

        if (Vector3.Distance(transform.position, path[crrPnt].position) < 1f) {
            int id = getIndexCity(path[crrPnt]);
            timeSpent += factory.graph.listNodes[id].time;
            print(factory.graph.listNodes[id].time);
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
        time = "time:{";
        for (int i = 0; i < factory.graph.nodesSize; i++){
            time += "[" + factory.graph.listNodes[i].time + ",";
        }
        time += "}";
    Save(time);
    }

    void Save(string time){
        PlayerPrefs.SetString("Time", time);
    }

    void Load(){
        time = PlayerPrefs.GetString("Time");
    }
}
