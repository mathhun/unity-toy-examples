using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BugManager : MonoBehaviour
{
    public GameObject bugPrefab;
    private GameObject[] bugs;
    private List<Vector3> initial_position;
    private List<Quaternion> initial_quaternion;
    private int active_bug_count;

    private GameController gameController;

    void Start()
    {
        bugs = GameObject.FindGameObjectsWithTag("Bug");
        active_bug_count = bugs.Length;

        initial_position = new List<Vector3>();
        initial_quaternion = new List<Quaternion>();

        foreach (GameObject bug in bugs) {
            initial_position.Add(bug.GetComponent<Bug>().transform.position);
            initial_quaternion.Add(bug.GetComponent<Bug>().transform.rotation);
        }

        // game controller
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null) {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null) {
            Debug.Log("Cannot find 'gameController' script");
        }
    }

    public void ProceedAllBugs()
    {
        foreach (GameObject bug in bugs) {
            bug.GetComponent<Bug>().Proceed();
        }
    }

    public void ResetAllBugs()
    {
        KillAllBugs();
        InitializeAllBugs();
    }

    public void DecrementBugCount()
    {
        active_bug_count--;

        if (active_bug_count == 0) {
            gameController.Fail();
        }
    }

    private void KillAllBugs()
    {
        active_bug_count = 0;

        foreach (GameObject bug in bugs) {
            Destroy(bug);
        }
    }

    private void InitializeAllBugs()
    {
        active_bug_count = bugs.Length;

        for (int i = 0; i < bugs.Length; i++) {
            Instantiate(bugPrefab, initial_position[i], initial_quaternion[i]);
        }
    }
}
