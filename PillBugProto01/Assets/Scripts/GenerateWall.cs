using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using System;

public class GenerateWall : MonoBehaviour
{
    public GameObject wall;
    public float intervalSec = 0.1f;
    public bool hasGeneratedWall = false;

    private float nextCheckTime;
    private bool isDragging = false;
    private List<Vector2> positions = new List<Vector2>();

    void Awake()
    {
    }

	void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            positions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0)) {
            positions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            InstantiateWalls(positions);
            positions = new List<Vector2>();

            isDragging = false;
            hasGeneratedWall = true;
        }

        if (isDragging && Time.time >= nextCheckTime) {
            nextCheckTime = Time.time + intervalSec;

            positions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
	}

    void InstantiateWalls(List<Vector2> pos)
    {
        GameObject prev = null;
        for (var i = 0; i < pos.Count - 1; i++) {
            GameObject obj = InstantiateWall(pos[i], pos[i + 1]);

            if (prev != null) {
                //prev.AddComponent<HingeJoint2D>();
                //HingeJoint2D hinge = prev.GetComponent<HingeJoint2D>();
                //hinge.anchor = pos[i];
                //Debug.Log(hinge);
                //hinge.connectedBody = obj.GetComponent<Rigidbody2D>();
            }

            prev = obj;
        }
    }

    GameObject InstantiateWall(Vector2 v1, Vector2 v2)
    {
        Vector2 vec = v2 - v1;
        Vector2 pos = vec / 2.0f + v1;
        //Debug.Log(pos);

        GameObject newWall = (GameObject)Instantiate(wall);
        newWall.transform.position = pos;
        newWall.transform.rotation = Quaternion.FromToRotation(Vector3.up, vec);
        newWall.transform.localScale = new Vector3(1.0f, vec.magnitude * 0.7f, 1.0f);

        return newWall;
    }
}
