using UnityEngine;
using System.Collections;
using System;
using UniRx;

public class GameController : MonoBehaviour
{
    enum GAME_STATE {
        INIT,
        BUGS_PROCEED,
        SUCCEED,
        FAIL,
    };

    public BugManager bugManager;
    public WallManager wallManager;
    public GUIText succeededText;
    public GUIText failedText;

    public float succeededDelaySec = 2.0f;
    public float failedDelaySec = 2.0f;

    private GAME_STATE state;
    private float suspendEndTime;

    private void Start()
    {
        succeededText.text = "";
        failedText.text = "";

        state = GAME_STATE.INIT;
        Debug.Log("GAME_STATE = INIT");
    }

    private void Update()
    {
    }

    public void ReceivedUserInput()
    {
        state = GAME_STATE.BUGS_PROCEED;
        Debug.Log("GAME_STATE = BUGS_PROCEED");
        bugManager.StartAll();
    }

    public void Succeed()
    {
        succeededText.text = "CLEAR!";

        state = GAME_STATE.SUCCEED;
        Debug.Log("GAME_STATE = SUCCEED");

        Observable.Timer(TimeSpan.FromSeconds(succeededDelaySec))
            .Subscribe(_ => ResetLevel());

        wallManager.SuspendInput();
    }

    public void Fail()
    {
        failedText.text = "Game Over";

        state = GAME_STATE.FAIL;
        Debug.Log("GAME_STATE = FAIL");

        Observable.Timer(TimeSpan.FromSeconds(failedDelaySec))
            .Subscribe(_ => ResetLevel());

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
