using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Factory : MonoBehaviour {

	public GameObject objective;

	List<CarStuff> carsList = new List<CarStuff>();
	List<Transform> selectedPath = new List<Transform> ();

	public int selectedCar;
	public GameObject bnt;

	void Start(){
		//carrega quantidade de carros na lista
		DEVELOP_ONLY();

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
			go.GetChild (i).GetComponent<Button> ().onClick.AddListener (() => {
				AddToPath(go.GetChild(i));
			});
		}
	}

	void AddToPath(Transform pnt){
		//if regras do grafo
		print("add "+pnt.name);
		selectedPath.Add (pnt);
	}

	private void DEVELOP_ONLY(){
		GameObject[] cars = GameObject.FindGameObjectsWithTag ("Player");
		for (int i = 0; i < cars.Length; i++)
			carsList.Add (cars [i].GetComponent<CarStuff> ());
	}

	public void ConfirmPath(){
		if (selectedPath.Contains (objective.transform) && carsList[selectedCar].free) {
			carsList [selectedCar].path = selectedPath;
			selectedPath.Clear ();
		} else
			print ("selecione o caminho até o objetivo final");
	}
}
