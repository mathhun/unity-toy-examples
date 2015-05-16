using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
    public float margin = 0.04f;

    private BUG_STATE state;

    private SceneManager sceneManager;

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
    }

	void Update()
    {
        if (state == BUG_STATE.WILL_TURN_RIGHT || state == BUG_STATE.WILL_TURN_LEFT) {
            transform.position += movement;
        } else if (state == BUG_STATE.WILL_TURN_RIGHT || state == BUG_STATE.WILL_TURN_LEFT) {
            transform.position += movement;
        }

        if (state != BUG_STATE.DISAPPEARED && isOutOfScreen()) {
            state = BUG_STATE.DISAPPEARED;
            sceneManager.DecrementBugCount();
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
            movement = new Vector3(0f, 0f, 0f);

            Observable.Timer(TimeSpan.FromSeconds(delaySec))
                .Subscribe(_ => Destroy(this.gameObject));
        }
    }

    // Set bugs state to `proceed`. Called when user has input a line
    public void Proceed()
    {
        state = BUG_STATE.WILL_TURN_RIGHT;
        //Debug.Log("proceeding");
    }
}
