using UnityEngine;
using System.Collections;
using UniRx;

public class GenerateWall : MonoBehaviour
{
    public GameObject wall;
    private Vector2 start = new Vector2(0.0f, 0.0f);
    private Vector2 end = new Vector2(0.0f, 0.0f);

    void Awake()
    {
        IObservable<long> updateStream = Observable.EveryUpdate();
        updateStream
            .Where(_ => Input.GetMouseButtonDown(0))
            .Select(_ => 1)
            .Scan((acc, current) => acc + current)
            .Subscribe(clickCount => Debug.Log(clickCount));
    }

	void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            start = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(start);
        }

        if (Input.GetMouseButtonUp(0)) {
            end = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(end);
            InstantiateWall(start, end);
        }
	}

    void InstantiateWall(Vector2 start, Vector2 end)
    {
        Vector2 vec = end - start;
        Vector2 pos = vec / 2.0f + start;
        Debug.Log(pos);

        GameObject newWall = (GameObject)Instantiate(wall);
        newWall.transform.position = pos;
        newWall.transform.rotation = Quaternion.FromToRotation(Vector3.up, vec);
        newWall.transform.localScale = new Vector3(1.0f, vec.magnitude, 1.0f);
    }
}
