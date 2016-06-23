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
		if (!PlayerPrefs.HasKey ("InitialCity"))
			StartTutorial ();
		else
			Destroy (GameObject.Find ("Canvas_Tutorial"));
	}

	void Update(){
		if(crrTuts >= tuts.Length) {
            Time.timeScale = 1;
			Destroy (GameObject.Find ("Canvas_Tutorial"));
		} else
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

            if(stopPoints[crrStopPoint] == 7) {
                Factory f = GameObject.Find("Factory").GetComponent<Factory>();
                if (!f.carsList[f.selectedCar].forward) {
                    Time.timeScale = 0;
                    bg.gameObject.SetActive(true);
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
        PlayerPrefs.SetInt("Cars_Ammount", 1);
		crrTuts++;
		crrStopPoint++;
		bg.gameObject.SetActive (true);
		map.SetActive (true);
		GameObject.Find ("Factory").GetComponent<Factory> ().SetInitialCity ();
	}

	public void Next(){
		if (crrStopPoint < stopPoints.Length) {
            if (crrTuts != stopPoints[crrStopPoint])
                crrTuts++;
            else {
                bg.gameObject.SetActive(false);

                if (stopPoints[crrStopPoint] == 3) {
                    Factory f = GameObject.Find("Factory").GetComponent<Factory>();
                    Vector3 pos = new Vector3(0, 30, 0);
                    GameObject go = Instantiate(f.job, pos, Quaternion.identity) as GameObject;

                    int indx = Random.Range(0, 18);
                    while (indx == PlayerPrefs.GetInt("InitialCity"))
                        indx = Random.Range(0, 18);

                    float m = (Random.value * 9999);
                    go.transform.FindChild("Text")
                        .GetComponent<Text>()
                        .text = "Job 1" + ": " + (m.ToString("c2"));
                    go.transform.SetParent(GameObject.Find("jobs").transform);
                    go.transform.localScale = Vector3.one;
                    go.transform.localPosition = pos;
                    go.name = indx + "Job 1" + "-" + m;

                    go.transform.FindChild("cancelJob")
                        .GetComponent<Button>().onClick.AddListener(() => f.CancelJob(go));

                    go.GetComponent<Button>().onClick.AddListener(() => f.TakeJob(go));
                }
            }
		} else
			crrTuts++;
	}

	public void Close(){
		Destroy (GameObject.Find ("Canvas_Tutorial"));
	}
}
