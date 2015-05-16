using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneManager : MonoBehaviour
{
    public GameObject bugPrefab;
    private GameObject[] bugs;
    private List<Vector3> initial_position;
    private List<Quaternion> initial_quaternion;

    void Start()
    {
        bugs = GameObject.FindGameObjectsWithTag("Bug");

        initial_position = new List<Vector3>();
        initial_quaternion = new List<Quaternion>();

        foreach (GameObject bug in bugs) {
            initial_position.Add(bug.GetComponent<Bug>().transform.position);
            initial_quaternion.Add(bug.GetComponent<Bug>().transform.rotation);
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
            Instantiate(bugPrefab, initial_position[i], initial_quaternion[i]);
        }
    }
}
