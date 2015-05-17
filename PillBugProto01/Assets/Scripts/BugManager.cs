using UnityEngine;
using System.Collections;

public class BugManager : MonoBehaviour
{
    public GameObject bugPrefab;
    public GameController gameController;

    private GameObject[] bugs;
    private int active_bug_count;
    private int succeeded_count;
    private int failed_count;

    void Start()
    {
        bugs = GameObject.FindGameObjectsWithTag("Bug");
        active_bug_count = bugs.Length;
        succeeded_count = 0;
        failed_count = 0;
    }

    public void ProceedAllBugs()
    {
        foreach (GameObject bug in bugs) {
            bug.GetComponent<Bug>().Proceed();
        }
    }

    public void IncrementSucceededCount()
    {
        succeeded_count++;
        active_bug_count--;
    }

    public void IncrementFailedCount()
    {
        failed_count++;
        active_bug_count--;
    }

    public bool IsSucceeded()
    {
        return succeeded_count == bugs.Length;
    }

    public bool IsFailed()
    {
        return active_bug_count == 0 && !IsSucceeded();
    }
}
