using UnityEngine;
using System.Collections;

public class Bug : MonoBehaviour
{
    public float speed;

    void Update () {
        Debug.Log(transform);
        transform.position += new Vector3(speed, 0, 0);
    }
}
