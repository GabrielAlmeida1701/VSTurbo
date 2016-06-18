﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets;

public class Factory : MonoBehaviour {

	public GameObject objective;

	List<CarStuff> carsList = new List<CarStuff>();
	List<Transform> selectedPath = new List<Transform> ();

	public int selectedCar;
	public GameObject bnt;
    public Graph graph;

    public float totalMoney;
    public float totalXP;

	void Start(){
		//carrega quantidade de carros na lista
		DEVELOP_ONLY();

        graph = GetComponent<MainGraph>().InitializeGraph();

		int qnt = carsList.Count;
		GameObject originalPos = GameObject.Find ("BntsPosition");

		for (int i = 0; i < qnt; i++) {
			GameObject go = Instantiate (bnt, Vector3.zero, Quaternion.identity) as GameObject;

			go.transform.parent = originalPos.transform;
			go.transform.localPosition = new Vector3 (0, i*-41, 0);
			go.transform.localRotation = Quaternion.identity;
			go.transform.localScale = Vector3.one;

			go.GetComponent<Button> ().onClick.AddListener (() => {
				selectedCar = i;
			});
		}

		SetBnts ();
	}

	void SetBnts(){
		Transform go = GameObject.Find ("Map").transform;
		for (int i = 0; i < go.childCount; i++) {
            Transform tr = go.GetChild(i);
            go.GetChild (i).GetComponent<Button> ().onClick.AddListener (() => {
				AddToPath(tr);
			});
		}
	}

	void AddToPath(Transform pnt){
        int id = lastSelected();
        if (id != -1) {
            
            int str = pnt.name.IndexOf("(");
            int end = pnt.name.IndexOf(")");
            string index = pnt.name.Substring(str).Replace("(", "").Replace(")", "");
            int next = int.Parse(index);
            /*foreach(Edge ed in edges) {
                print(ed.adjacent.id);
            }*/
        }

        if (!selectedPath.Contains(pnt))
            selectedPath.Add(pnt);
	}

	private void DEVELOP_ONLY(){
		GameObject[] cars = GameObject.FindGameObjectsWithTag ("Player");
		for (int i = 0; i < cars.Length; i++)
			carsList.Add (cars [i].GetComponent<CarStuff> ().SetFactory(this));
	}

	public void ConfirmPath(){
		if (selectedPath.Contains (objective.transform) && carsList[selectedCar].free) {
            carsList[selectedCar].forward = true;
            carsList[selectedCar].free = false;
            CloneList();
			selectedPath.Clear ();
		} else
			print ("selecione o caminho até o objetivo final");
	}

    private void CloneList() {
        for (int i = 0; i < selectedPath.Count; i++)
            carsList[selectedCar].path.Add(selectedPath[i]);
    }

    private int lastSelected() {
        if (selectedPath.Count - 1 == -1)
            return -1;

        Transform tr = selectedPath[selectedPath.Count - 1];
        Transform go = GameObject.Find ("Map").transform;
        for (int i = 0; i < go.childCount; i++) {
            if (go.GetChild(i) == tr)
                return i;
        }

        return -1;
    }
}
