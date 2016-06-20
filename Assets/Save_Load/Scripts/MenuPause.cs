using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuPause : MonoBehaviour {

	public Canvas pause;


	// Use this for initialization
	void Start () {
		pause = pause.GetComponent<Canvas>();
		pause.enabled=false;
	}
	
	// Update is called once per frame


	public void pausar(){
		Time.timeScale=0;
		pause.enabled=true;
	}

	public void Continuar(){
		pause.enabled=false;
		Time.timeScale=1;
	}

	public void voltaMenu(){
		Time.timeScale=1;
		Application.LoadLevel(0);
	}


	void Update () {
		
	}

}
