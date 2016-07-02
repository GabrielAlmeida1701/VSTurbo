using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CamCtrl : MonoBehaviour {

    public Slider slider;

    void Update () {
        Camera.main.orthographicSize = slider.value;
        Factory f = GameObject.Find("Factory").GetComponent<Factory>();

        if (f.objective != null) {
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, -13.8f, 15.61f);
            pos.y = Mathf.Clamp(pos.y, -9.78f, 10);

            transform.position = pos;

            if (Input.GetMouseButton(0)) {
                transform.Translate(
                    Input.GetAxis("Mouse X") * Time.deltaTime * 5,
                    Input.GetAxis("Mouse Y") * Time.deltaTime * 5,
                    0
                );
            }
        } else {
            f.EnableCitys(false);
        }
	}
}
