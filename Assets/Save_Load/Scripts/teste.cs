using UnityEngine;
using System.Collections;

public class teste : MonoBehaviour {

	public float velMov;
	void Update () {
		transform.Translate(-velMov * Time.deltaTime, -velMov * Time.deltaTime, 0);
	}

}
