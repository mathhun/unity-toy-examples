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
    };

    public float delaySec = 0.5f;
    public Vector3 movement;

    private BUG_STATE state;

    void Start()
    {
        InstantiateBugs();
    }

	void Update()
    {
        if (state == BUG_STATE.WILL_TURN_RIGHT || state == BUG_STATE.WILL_TURN_LEFT) {
            transform.position += movement;
        } else if (state == BUG_STATE.WILL_TURN_RIGHT || state == BUG_STATE.WILL_TURN_LEFT) {
            transform.position += movement;
        }

        if (state == BUG_STATE.STOP && GenerateWall.hasGeneratedWall) {
            Proceed();
        }
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
    }

    // Reset bugs with their initial settings.
    // Called when user tapped while bugs are moving, or when user fails (all bugs disappeared).
    public void ResetAll()
    {
        KillBugs();
        InstantiateBugs();
        state = BUG_STATE.STOP;
    }

    // Destroy all existing bugs
    private void KillBugs()
    {
        GameObject[] bugs;

        bugs = GameObject.FindGameObjectsWithTag("Bug");
        foreach (GameObject bug in bugs) {
            Destroy(bug);
        }
    }

    // Instantiate bugs in their initial positions
    private void InstantiateBugs()
    {
        SceneManager.InstantiateBugs();
        state = BUG_STATE.STOP;
    }
}
