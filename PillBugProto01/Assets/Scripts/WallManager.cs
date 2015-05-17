using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WallManager : MonoBehaviour
{
    enum INPUT_STATE {
        INIT,
        BUILDING_WALL,
        HAS_BUILT_WALL,
        SUSPEND,
    };

    public GameObject wall;
    public float intervalSec = 0.1f;
    public GameController gameController;

    private INPUT_STATE state;
    private List<Vector2> positions;
    private float nextCheckTime;

    private void Start()
    {
        positions = new List<Vector2>();
        state = INPUT_STATE.INIT;
    }

    private void Update()
    {
        if (state == INPUT_STATE.SUSPEND) {
            return;
        }

        if (state == INPUT_STATE.INIT && Input.GetMouseButtonDown(0)) {
            positions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            state = INPUT_STATE.BUILDING_WALL;
        }

        if (state == INPUT_STATE.BUILDING_WALL && Input.GetMouseButtonUp(0)) {
            positions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            InstantiateWalls(positions);

            state = INPUT_STATE.HAS_BUILT_WALL;
            gameController.ReceivedUserInput();
        }

        if (state == INPUT_STATE.BUILDING_WALL && Time.time >= nextCheckTime) {
            nextCheckTime = Time.time + intervalSec;
            positions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        // reset bugs && walls
        if (state == INPUT_STATE.HAS_BUILT_WALL && Input.GetMouseButtonDown(0)) {
            gameController.ResetLevel();
        }
    }

    public void SuspendInput()
    {
        state = INPUT_STATE.SUSPEND;
    }

    private void InstantiateWalls(List<Vector2> pos)
    {
        for (var i = 0; i < pos.Count - 1; i++) {
            InstantiateWall(pos[i], pos[i + 1]);
        }
    }

    private GameObject InstantiateWall(Vector2 v1, Vector2 v2)
    {
        Vector2 vec = v2 - v1;
        Vector2 pos = vec / 2.0f + v1;

        GameObject newWall = Instantiate(wall);
        newWall.transform.position = pos;
        newWall.transform.rotation = Quaternion.FromToRotation(Vector3.up, vec);
        newWall.transform.localScale = new Vector3(1.0f, vec.magnitude * 0.7f, 1.0f);

        return newWall;
    }
}
