using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tutorial : MonoBehaviour {

	public Sprite[] tuts = new Sprite[10];
	public int crrTuts = 0;

	private int[] stopPoints = {1, 3, 6, 7};
	private int crrStopPoint = 0;

	Image bg;

	GameObject map, slcFrstCity;

	void Start () {
		PlayerPrefs.DeleteAll ();
		if (!PlayerPrefs.HasKey ("InitialCity"))
			StartTutorial ();
		else
			Destroy (GameObject.Find ("Canvas_Tutorial"));
	}

	void Update(){
		if(crrTuts >= tuts.Length)
			Destroy (GameObject.Find ("Canvas_Tutorial"));
		else
			bg.sprite = tuts [crrTuts];

		if (crrStopPoint < stopPoints.Length) {
			if (stopPoints [crrStopPoint] == 1)
				map.SetActive (false);

			if (stopPoints [crrStopPoint] == 3) {
				if (GameObject.Find ("Factory").GetComponent<Factory> ().objective != null) {
					bg.gameObject.SetActive (true);
					crrTuts++;
					crrStopPoint++;
				}
			}

			if (stopPoints [crrStopPoint] == 6) {
				Factory f = GameObject.Find ("Factory").GetComponent<Factory> ();
				if (f.selectedPath.Contains (f.objective)) {
					bg.gameObject.SetActive (true);
					crrTuts++;
					crrStopPoint++;
				}
			}
		}
	}

	void StartTutorial(){
		bg = transform.FindChild ("bg").GetComponent<Image> ();

		map = GameObject.Find ("Map");
		slcFrstCity = GameObject.Find ("FrstCityMap");
		selectFrstCity ();
	}

	void selectFrstCity(){
		Transform go = GameObject.Find ("FrstCityMap").transform;

		for (int i = 0; i < go.childCount; i++) {
			Transform tr = go.GetChild(i);
			go.GetChild (i).GetComponent<Button> ().onClick.AddListener (() => {
				SetFrstCity(tr);
			});
		}
	}

	void SetFrstCity(Transform pnt){
		string index = "";
		if (pnt.name.IndexOf("(") != -1) {
			int str = pnt.name.IndexOf("(");
			int end = pnt.name.IndexOf(")");
			index = pnt.name.Substring(str).Replace("(", "").Replace(")", "");
		} else
			index = "0";

		Destroy (GameObject.Find ("FrstCityMap"));
		PlayerPrefs.SetInt("InitialCity", int.Parse(index));
		crrTuts++;
		crrStopPoint++;
		bg.gameObject.SetActive (true);
		map.SetActive (true);
		GameObject.Find ("Factory").GetComponent<Factory> ().SetInitialCity ();
	}

	public void Next(){
		if (crrStopPoint < stopPoints.Length) {
			if (crrTuts != stopPoints [crrStopPoint])
				crrTuts++;
			else
				bg.gameObject.SetActive (false);
		} else
			crrTuts++;
	}

	public void Close(){
		Destroy (GameObject.Find ("Canvas_Tutorial"));
	}
}
