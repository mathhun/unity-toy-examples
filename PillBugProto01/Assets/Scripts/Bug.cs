using UnityEngine;
using System.Collections;
using System;
using UniRx;

public class Bug : MonoBehaviour
{
    public float delaySec = 0.5f;
    public Vector3 movement = new Vector3(0.02f, 0.0f);

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Goal") {
            movement = new Vector3(0f, 0f, 0f);

            Observable.Timer(TimeSpan.FromSeconds(delaySec))
                .Subscribe(_ => Destroy(this.gameObject));
        }
    }

	void Update ()
    {
        transform.position += movement;
	}
}
