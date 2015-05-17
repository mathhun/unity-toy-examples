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
    public GUIText successText;

    private GAME_STATE state;
    private float suspendEndTime;
    private BugManager bugManager;
    private WallManager wallManager;

    void Start()
    {
        // bug manager
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

        state = GAME_STATE.INIT;
        Debug.Log("GAME_STATE = INIT");
        successText.text = "";
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
        Debug.Log("GAME_STATE = BUGS_PROCEED");
        bugManager.ProceedAllBugs();
    }

    public void Succeed()
    {
        state = GAME_STATE.SUCCEED;
        successText.text = "CLEAR!";
        Debug.Log("GAME_STATE = SUCCEED");
    }

    public void Fail()
    {
        state = GAME_STATE.FAIL;
        suspendEndTime = Time.time + suspendSec;
        Debug.Log("GAME_STATE = FAIL");

        wallManager.SuspendInput();
    }

    public void ResetLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void IncrementSucceededCount()
    {
        bugManager.IncrementSucceededCount();
        Debug.Log("Succeeded++");
        if (bugManager.IsSucceeded()) {
            Succeed();
        }
    }

    public void IncrementFailedCount()
    {
        bugManager.IncrementFailedCount();
        Debug.Log("Failed++");
        if (bugManager.IsFailed()) {
            Fail();
        }
    }
}
