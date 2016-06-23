using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets;

public class Factory : MonoBehaviour {

	public List<CarStuff> carsList = new List<CarStuff>();
	public List<Transform> selectedPath = new List<Transform> ();

    public Sprite normal, adjacent, destination, target_adjacent, frstCity_icon;
    public GameObject confirmBnt;

    public int selectedCar;
	public GameObject bnt;
    public GameObject job;
    public Graph graph;

    public float totalMoney;
    public float totalXP;

    private Transform frstCity;
    public Transform objective;
    public GameObject carModel;

    private int jobsCount;

    void Awake() {
        if (PlayerPrefs.HasKey("InitialCity")) {
            SetInitialCity();

            jobsCount = Random.Range(1, 5);
            for (int i = 0; i < jobsCount; i++) {
                Vector3 pos = new Vector3(0, 30 - (i * 30), 0);
                GameObject go = Instantiate(job, pos, Quaternion.identity) as GameObject;
                go.transform.FindChild("Text").GetComponent<Text>().text = "Job " + (i + 1);
                go.transform.SetParent(GameObject.Find("jobs").transform);
                go.transform.localScale = Vector3.one;
                go.transform.localPosition = pos;
                go.name = "Job " + (i + 1);

                go.transform.FindChild("cancelJob")
                    .GetComponent<Button>().onClick.AddListener(() => CancelJob(go));

                go.GetComponent<Button>().onClick.AddListener(() => TakeJob(go));
            }
        }

        graph = GetComponent<MainGraph>().InitializeGraph();
    }

	public void SetInitialCity(){
		int indx = PlayerPrefs.GetInt ("InitialCity");
		Transform go = GameObject.Find ("Map").transform;
		frstCity = go.GetChild (indx);

        int carAmmount = PlayerPrefs.GetInt("Cars_Ammount");
        for (int i = 0; i < carAmmount; i++)
            Instantiate(carModel, Vector3.zero, carModel.transform.rotation);

        SetCarsBnts();
	}

    void Start(){
		SetCarsBnts ();

		SetBnts ();
	}

	void SetCarsBnts(){
		SetCarsList();

		int qnt = carsList.Count;
		GameObject originalPos = GameObject.Find ("BntsPosition");

        for(int j=0; j<originalPos.transform.childCount; j++)
            Destroy(originalPos.transform.GetChild(j).gameObject);

		for (int i = 0; i < qnt; i++) {
			GameObject go = Instantiate (bnt, Vector3.zero, Quaternion.identity) as GameObject;

			go.transform.SetParent(originalPos.transform);
			go.transform.localPosition = new Vector3 (0, i*-41, 0);
			go.transform.localRotation = Quaternion.identity;
			go.transform.localScale = Vector3.one;

			go.GetComponent<Button> ().onClick.AddListener (() => {
				selectedCar = i;
			});
		}
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
        int canAdd = 0;
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

            if (canAdd != -1) {
                selectedPath[selectedPath.Count - 1].GetComponent<city>().chosenPathway = canAdd;

                Transform go = GameObject.Find("Map").transform;
                for (int i = 0; i < graph.nodesSize; i++) {
                    if (i != PlayerPrefs.GetInt("Destination"))
                        go.GetChild(i).GetComponent<Image>().sprite = normal;
                }

                objective.GetComponent<Image>().sprite = destination;

                List<Edge> edges = graph.GetAdjacents(next);
                foreach(Edge ed in edges) {
                    go.GetChild(ed.adjacent.id).GetComponent<Image>().sprite = adjacent;
                    if(ed.adjacent.id == PlayerPrefs.GetInt("Destination"))
                        objective.GetComponent<Image>().sprite = target_adjacent;
                }

                frstCity.GetComponent<Image>().sprite = frstCity_icon;
            }
        }

        if (!selectedPath.Contains(pnt) && canAdd != -1 && !selectedPath.Contains(objective))
            selectedPath.Add(pnt);

        confirmBnt.SetActive(selectedPath.Contains(objective));
	}

	private void SetCarsList(){
		GameObject[] cars = GameObject.FindGameObjectsWithTag ("Player");
		for (int i = 0; i < cars.Length; i++)
			carsList.Add (cars [i].GetComponent<CarStuff> ().SetFactory(this).StartCar());
	}

	public void ConfirmPath(){
		if (selectedPath.Contains (objective) && carsList[selectedCar].free) {
            carsList[selectedCar].forward = true;
            carsList[selectedCar].free = false;
            makingPath = false;

            CloneList();
			selectedPath.Clear ();
		} else
			print ("selecione o caminho até o objetivo final");
	}

    bool makingPath;
    string jobName;
    public void TakeJob(GameObject jobGO) {
        if (carsList[selectedCar].free && !makingPath) {
            makingPath = true;
            jobName = jobGO.name;

            if (!selectedPath.Contains(frstCity))
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
                if (ed.adjacent.id == PlayerPrefs.GetInt("Destination"))
                    objective.GetComponent<Image>().sprite = target_adjacent;
            }

            frstCity.GetComponent<Image>().sprite = frstCity_icon;
        }
    }

    public void CancelJob(GameObject go) {
        foreach(CarStuff cs in carsList) {
            if (cs.job == jobName)
                cs.free = true;
        }
        jobsCount--;
        print(jobsCount);
        Transform j = GameObject.Find("jobs").transform;
        for(int i=0; i<jobsCount; i++) {
            Vector3 pos = new Vector3(0, 30 - (i * 30), 0);
            j.GetChild(i).localPosition = pos;
        }

        makingPath = false;

        Destroy(go);
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
