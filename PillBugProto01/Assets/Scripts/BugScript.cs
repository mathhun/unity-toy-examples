using UnityEngine;
using System.Collections;

public class BugScript : MonoBehaviour
{
	void Update () {
        transform.position += new Vector3(0.02f, 0.0f, 0.0f);
	}
}
