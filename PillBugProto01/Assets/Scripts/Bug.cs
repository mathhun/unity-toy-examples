using UnityEngine;
using System.Collections;
using System;
using UniRx;

public class Bug : MonoBehaviour
{
    enum BUG_STATE {
        STOP,
        WILL_TURN_RIGHT,
        WILL_TURN_LEFT,
        PROCEED_ALONG_WALL,
        REACHED_GOAL,
        DISAPPEARED,
    };

    public float delaySec = 0.5f;
    public Vector3 movement = new Vector3(0.02f, 0f, 0f);
    public float margin = 0.1f;

    private GameController gameController;
    private BUG_STATE state;

    void Start()
    {
        // game controller
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null) {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null) {
            Debug.Log("Cannot find 'gameController' script");
        }
    }

	void Update()
    {
        if (state == BUG_STATE.WILL_TURN_RIGHT || state == BUG_STATE.WILL_TURN_LEFT) {
            transform.position += movement;
        } else if (state == BUG_STATE.WILL_TURN_RIGHT || state == BUG_STATE.WILL_TURN_LEFT) {
            transform.position += movement;
        }

        if (state != BUG_STATE.DISAPPEARED && isOutOfScreen()) {
            Fail();
        }
	}

    bool isOutOfScreen()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        float n = 0 - margin;
        float p = 1 + margin;
        return pos.x < n || pos.x > p || pos.y < n || pos.y > p;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Goal") {
            Succeed();
        }
    }

    public void Proceed()
    {
        state = BUG_STATE.WILL_TURN_RIGHT;
    }

    private void Succeed()
    {
        state = BUG_STATE.REACHED_GOAL;
        movement = new Vector3(0f, 0f, 0f);
        gameController.IncrementSucceededCount();

        Observable.Timer(TimeSpan.FromSeconds(delaySec))
            .Subscribe(_ => Destroy(this.gameObject));
    }

    private void Fail()
    {
        state = BUG_STATE.DISAPPEARED;
        movement = new Vector3(0f, 0f, 0f);
        gameController.IncrementFailedCount();
    }
}
