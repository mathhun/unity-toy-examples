using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using System;

public class GenerateWall : MonoBehaviour
{
    public GameObject wall;

    //private Vector2 start = new Vector2(0.0f, 0.0f);
    //private Vector2 end = new Vector2(0.0f, 0.0f);

    public float intervalSec = 0.1f;
    private float nextCheckTime;
    //private bool doneGenerating = false;
    private bool isDragging = false;
    private List<Vector2> positions = new List<Vector2>();

    void Awake()
    {
        //IObservable<long> updateStream = Observable.EveryUpdate();
        //updateStream
        //    .Where(_ => Input.GetMouseButtonDown(0))
        //    .Select(_ => 1)
        //    .Scan((acc, current) => acc + current)
        //    .Subscribe(clickCount => Debug.Log(clickCount));
    }

	void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            //start = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(start);

            positions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0)) {
            //end = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(end);

            positions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            //InstantiateWall(start, end);
            InstantiateWalls(positions);
            positions = new List<Vector2>();

            isDragging = false;
            //doneGenerating = true;
        }

        if (isDragging && Time.time >= nextCheckTime) {
            nextCheckTime = Time.time + intervalSec;

            positions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
	}

    void InstantiateWalls(List<Vector2> pos)
    {
        for (var i = 0; i < pos.Count - 1; i++) {
            InstantiateWall(pos[i], pos[i + 1]);
        }
    }

    void InstantiateWall(Vector2 v1, Vector2 v2)
    {
        Vector2 vec = v2 - v1;
        Vector2 pos = vec / 2.0f + v1;
        //Debug.Log(pos);

        GameObject newWall = (GameObject)Instantiate(wall);
        newWall.transform.position = pos;
        newWall.transform.rotation = Quaternion.FromToRotation(Vector3.up, vec);
        newWall.transform.localScale = new Vector3(1.0f, vec.magnitude * 0.7f, 1.0f);
    }
}
