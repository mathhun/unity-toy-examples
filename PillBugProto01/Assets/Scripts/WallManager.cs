using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using System;

public class WallManager : MonoBehaviour
{
    public GameObject wall;
    public float intervalSec = 0.1f;
    public static bool hasGeneratedWall = false;

    private float nextCheckTime;
    private bool isDragging = false;
    private List<Vector2> positions = new List<Vector2>();

    private SceneManager sceneManager;
    private GameController gameController;

    private List<GameObject> generatedWalls;

    void Start()
    {
        // scene manager
        GameObject sceneManagerObject = GameObject.FindWithTag("SceneManager");
        if (sceneManagerObject != null) {
            sceneManager = sceneManagerObject.GetComponent<SceneManager>();
        }
        if (sceneManager == null) {
            Debug.Log("Cannot find 'SceneManager' script");
        }

        // game controller
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null) {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null) {
            Debug.Log("Cannot find 'gameController' script");
        }

        generatedWalls = new List<GameObject>();
    }

	void Update()
    {
        // mouse input start
        if (Input.GetMouseButtonDown(0)) {
            positions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            isDragging = true;
        }

        // mouse input end
        if (Input.GetMouseButtonUp(0)) {
            positions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            InstantiateWalls(positions);
            positions = new List<Vector2>();

            isDragging = false;
            hasGeneratedWall = true;

            sceneManager.ProceedAllBugs();
            gameController.ReceivedUserInput();
        }

        if (isDragging && Time.time >= nextCheckTime) {
            nextCheckTime = Time.time + intervalSec;
            positions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        // reset bugs
        if (hasGeneratedWall && Input.GetMouseButtonDown(0)) {
            sceneManager.ResetAllBugs();
        }
	}

    void InstantiateWalls(List<Vector2> pos)
    {
        GameObject prev = null;
        for (var i = 0; i < pos.Count - 1; i++) {
            GameObject obj = InstantiateWall(pos[i], pos[i + 1]);
            generatedWalls.Add(obj);

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

    public void ResetWalls()
    {
        foreach (GameObject wall in generatedWalls) {
            Destroy(wall);
        }
    }
}
