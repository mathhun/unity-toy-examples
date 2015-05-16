using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    enum GAME_STATE {
        INIT,
        BUGS_PROCEED,
        SUCCEED,
        FAIL,
    };

    public float suspendSec = 2.0f;

    private GAME_STATE state;
    private float suspendEndTime;

    private SceneManager sceneManager;
    private WallManager wallManager;

    void Start()
    {
        state = GAME_STATE.INIT;

        // scene manager
        GameObject sceneManagerObject = GameObject.FindWithTag("SceneManager");
        if (sceneManagerObject != null) {
            sceneManager = sceneManagerObject.GetComponent<SceneManager>();
        }
        if (sceneManager == null) {
            Debug.Log("Cannot find 'SceneManager' script");
        }

        // wall manager
        GameObject wallManagerObject = GameObject.FindWithTag("WallManager");
        if (wallManagerObject != null) {
            wallManager = wallManagerObject.GetComponent<WallManager>();
        }
        if (wallManager == null) {
            Debug.Log("Cannot find 'WallManager' script");
        }
    }

    void Update()
    {
        if (state == GAME_STATE.FAIL && Time.time >= suspendEndTime) {
            state = GAME_STATE.INIT;

            sceneManager.ResetAllBugs();
            wallManager.ResetWalls();
        }
    }

    public void ReceivedUserInput()
    {
        state = GAME_STATE.BUGS_PROCEED;
    }

    public void Fail()
    {
        state = GAME_STATE.FAIL;
        suspendEndTime = Time.time + suspendSec;
    }
}
