using UnityEngine;
using System.Collections;

public class DrawLines2 : MonoBehaviour {
    public GameObject wall;
    private Vector3 start = new Vector3(0, 0, 0);
    private Vector3 end = new Vector3(0, 0, 0);
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0)) {
            start = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(start);
        }
        if (Input.GetMouseButtonUp(0)) {
            end = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(end);

            Vector3 pos = new Vector3((start.x + end.x) / 2, 1.0f, (start.z + end.z) / 2);
            Debug.Log(pos);

            Quaternion qua = Quaternion.identity;
            qua = Quaternion.AngleAxis(45, Vector3.up);
            //qua = Quaternion.AngleAxis(Vector3.Angle(start - end, end.forward), Vector3.up);
            qua = Quaternion.Euler(0, 90, 0);

            //Debug.Log("angle");
            //Debug.Log(Vector3.Angle(start - end, transform.forward));

            Debug.Log("qua");
            Debug.Log(qua);

            Instantiate(wall, pos, qua);
        }
	}
}
