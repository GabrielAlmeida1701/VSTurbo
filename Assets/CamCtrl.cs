using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CamCtrl : MonoBehaviour {

    public Slider slider;

    void Update () {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -13.8f, 15.61f);
        pos.y = Mathf.Clamp(pos.y, -9.78f, 10);

        transform.position = pos;

        Camera.main.orthographicSize = slider.value;

        if (Input.GetMouseButton(0)) {
            transform.Translate(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
        }
	}
}
