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

    private BugManager bugManager;
    private WallManager wallManager;

    void Start()
    {
        state = GAME_STATE.INIT;

        // scene manager
        GameObject bugManagerObject = GameObject.FindWithTag("BugManager");
        if (bugManagerObject != null) {
            bugManager = bugManagerObject.GetComponent<BugManager>();
        }
        if (bugManager == null) {
            Debug.Log("Cannot find 'BugManager' script");
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
            ResetLevel();
        }
    }

    public void ReceivedUserInput()
    {
        state = GAME_STATE.BUGS_PROCEED;
        bugManager.ProceedAllBugs();
    }

    public void Fail()
    {
        state = GAME_STATE.FAIL;
        suspendEndTime = Time.time + suspendSec;
    }

    public void ResetLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void DecrementBugCount()
    {
        bugManager.DecrementBugCount();
    }
}
