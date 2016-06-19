using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets;

public class Factory : MonoBehaviour {

	List<CarStuff> carsList = new List<CarStuff>();
	List<Transform> selectedPath = new List<Transform> ();

    public Sprite normal, adjacent, destination;

	public int selectedCar;
	public GameObject bnt;
    public Graph graph;

    public float totalMoney;
    public float totalXP;

    private Transform frstCity;
    private Transform objective;

    void Awake() {
        PlayerPrefs.SetInt("InitialCity", 16);

        int indx = PlayerPrefs.GetInt("InitialCity");
        Transform go = GameObject.Find("Map").transform;
        frstCity = go.GetChild(indx);
    }

    void Start(){
		SetCarsList();

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
        bool canAdd = true;
        if (id != -1) {
            string index = "";
            if (pnt.name.IndexOf("(") != -1) {
                int str = pnt.name.IndexOf("(");
                int end = pnt.name.IndexOf(")");
                index = pnt.name.Substring(str).Replace("(", "").Replace(")", "");
            } else
                index = "0";
            
            int next = int.Parse(index);
            canAdd = graph.IsAdjacent(id, next);

            if (canAdd) {
                Transform go = GameObject.Find("Map").transform;
                for (int i = 0; i < graph.nodesSize; i++)
                    if(i != PlayerPrefs.GetInt("Destination"))
                        go.GetChild(i).GetComponent<Image>().sprite = normal;

                objective.GetComponent<Image>().sprite = destination;

                List<Edge> edges = graph.GetAdjacents(next);
                foreach(Edge ed in edges) {
                    go.GetChild(ed.adjacent.id).GetComponent<Image>().sprite = adjacent;
                }
            }
        }

        if (!selectedPath.Contains(pnt) && canAdd)
            selectedPath.Add(pnt);
	}

	private void SetCarsList(){
		GameObject[] cars = GameObject.FindGameObjectsWithTag ("Player");
		for (int i = 0; i < cars.Length; i++)
			carsList.Add (cars [i].GetComponent<CarStuff> ().SetFactory(this));
	}

	public void ConfirmPath(){
		if (selectedPath.Contains (objective) && carsList[selectedCar].free) {
            carsList[selectedCar].forward = true;
            carsList[selectedCar].free = false;
            CloneList();
			selectedPath.Clear ();
		} else
			print ("selecione o caminho até o objetivo final");
	}

    public void TakeJob() {
        print("Job Started");
        if(!selectedPath.Contains(frstCity))
            selectedPath.Add(frstCity);

        int indx = Random.Range(0, 18);
        while(indx == PlayerPrefs.GetInt("InitialCity"))
            indx = Random.Range(0, 18);

        PlayerPrefs.SetInt("Destination", indx);
        
        Transform go = GameObject.Find("Map").transform;
        objective = go.GetChild(indx);
        objective.GetComponent<Image>().sprite = destination;

        List<Edge> edges = graph.GetAdjacents(PlayerPrefs.GetInt("InitialCity"));
        foreach(Edge ed in edges) {
            go.GetChild(ed.adjacent.id).GetComponent<Image>().sprite = adjacent;
        }
    }

    public void FinishJob() {
        print("End of Job");
        selectedPath.Add(frstCity);

        Transform go = GameObject.Find("Map").transform;
        for (int i = 0; i < graph.nodesSize; i++)
            go.GetChild(i).GetComponent<Image>().sprite = normal;

        totalMoney += Random.value * 2500;
        totalXP += Random.Range(100, 200);

        objective = null;
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
