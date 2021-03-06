﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CarStuff : MonoBehaviour {

	public List<Transform> path = new List<Transform>();

    public GameObject loadIcon;
    public GameObject lowGas;

    public float gasTankMax = 50;
    public float GasTank;
	public float timeSpent;
	public float speed;

	public bool free;
    public bool forward;

    private int crrPnt;
    private int streetPnt;
    public float waitTime;
    private Factory factory;

    public string job;
    
	public CarStuff StartCar() {
        int indx = PlayerPrefs.GetInt("InitialCity");
        Transform go = GameObject.Find("Map").transform;
        transform.position = go.GetChild(indx).position;

        lowGas = Instantiate(lowGas, transform.position, Quaternion.identity) as GameObject;
        lowGas.SetActive(false);

		return this;
    }

    float t;
    Color cor = Color.red;
    void ShowLowGas() {
        lowGas.SetActive(true);
        lowGas.transform.position = transform.position;
        
        t += Time.deltaTime;
        if (t >= 1) {
            if (cor == Color.red)
                cor = Color.white;
            else
                cor = Color.red;

            t = 0;
        }

        lowGas.transform.GetChild(0).GetComponent<SpriteRenderer>().color = cor;
    }

	void Update(){
        if(path.Count != 0) {
		    if (!free) {
                if (forward) {
                    if (GasTank > 0)
                        GoForward();
                    else
                        ShowLowGas();
                } else {
                    if (GasTank > 0)
                        GoBack();
                    else
                        ShowLowGas();
                }
		    } else {
                if (crrPnt >= path.Count-1) {
                    waitTime += Time.deltaTime;

                    Vector3 scl = loadIconGO.transform.FindChild("bg").localScale;
                    scl.x = waitTime / 5;
                    loadIconGO.transform.FindChild("bg").localScale = scl;

                    if (waitTime >= 5) {
                        forward = false;
                        free = false;
                        Destroy(loadIconGO);
                        crrPnt--;
                        waitTime = 0;
                    }
                } else {
                    int indx = PlayerPrefs.GetInt("InitialCity");
                    Transform go = GameObject.Find("Map").transform;
                    transform.position = go.GetChild(indx).position;
                }
            }
        }
	}

    private GameObject loadIconGO;
    void GoForward() {
        if (crrPnt >= path.Count-1) {
            free = true;
            loadIconGO = Instantiate(loadIcon, path[crrPnt].position, loadIcon.transform.rotation) as GameObject;
            loadIconGO.transform.SetParent(GameObject.Find("CanvasUI").transform);
            loadIconGO.transform.localScale = Vector3.one;

            return;
        }
        
        lowGas.SetActive(false);

        transform.Translate (0, 0, speed * Time.deltaTime);

        int pathway = path[crrPnt].GetComponent<city>().chosenPathway;
        Transform look = path[crrPnt].GetComponent<city>().paths[pathway];

        transform.LookAt (look.GetChild(streetPnt));

        if (Vector3.Distance(transform.position, look.GetChild(streetPnt).position) < 1f)
            streetPnt++;

        if(streetPnt >= look.childCount) {
            int id = getIndexCity(path[crrPnt]);
            timeSpent += factory.graph.listNodes[id].time;
            GasTank -= factory.graph.listNodes[id].time * 0.8f;

            streetPnt = 0;
            crrPnt++;
        }
    }
    
    void tst() {
        if (crrPnt < 0) {
            free = true;
            path.Clear();
            factory.FinishJob(job);
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
            factory.FinishJob(job);
            crrPnt = 0;
	        GetTime();

            return;
        }
        
        lowGas.SetActive(false);

        transform.Translate (0, 0, speed * Time.deltaTime);
		transform.LookAt (path [crrPnt]);

        if (Vector3.Distance(transform.position, path[crrPnt].position) < 1f) {
            int id = getIndexCity(path[crrPnt]);
            timeSpent += factory.graph.listNodes[id].time;
            GasTank -= factory.graph.listNodes[id].time * 0.7f;

            crrPnt--;
        }
    }

	public CarStuff SetFactory(Factory factory) {
        this.factory = factory;
		return this;
    }
    
    void GetTime(){
        string time = "time:[";
        for (int i = 0; i < factory.graph.nodesSize; i++){
            time += factory.graph.listNodes[i].time + ",";
        }
        time = time.Substring(0, time.Length - 2);
        time += "]";

        PlayerPrefs.SetString("Time", time);
    }

    void Load(){
        string time = PlayerPrefs.GetString("Time");
    }
}
