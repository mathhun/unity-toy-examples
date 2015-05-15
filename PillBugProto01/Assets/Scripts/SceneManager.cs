using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneManager : MonoBehaviour
{
    public GameObject bugTemplate;
    private GameObject[] bugs;
    private List<Vector3> initial_pos;

    void Start()
    {
        bugs = GameObject.FindGameObjectsWithTag("Bug");

        initial_pos = new List<Vector3>();
        foreach (GameObject bug in bugs) {
            initial_pos.Add(bug.GetComponent<Bug>().transform.position);
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

    private void KillAllBugs()
    {
        foreach (GameObject bug in bugs) {
            Destroy(bug);
        }
    }

    private void InitializeAllBugs()
    {
        for (int i = 0; i < bugs.Length; i++) {
            Instantiate(bugTemplate, initial_pos[i], Quaternion.identity);
        }
    }
}
