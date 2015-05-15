using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour
{
    // initial position & rotation
    public static GameObject bug;

    public static Vector3[] initial_position;
    public static float[] initial_rotation_z;

	void Start() {
	}

	void Update() {
	}

    public static void InstantiateBugs()
    {
        for (int i = 0; i < initial_position.Length; i++) {
            Instantiate(bug, initial_position[i], Quaternion.AngleAxis(initial_rotation_z[i], Vector3.up));
        }
    }
}
