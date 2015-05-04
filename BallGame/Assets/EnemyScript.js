#pragma strict

function Update () {
    transform.position.z -= 0.1;
    var n : int = 8;
    transform.Rotate(Random.value*n, Random.value*n, Random.value*n);

    if (transform.position.z < -12.0) {
        Application.LoadLevel("GameOver");
    }
}

function OnCollisionEnter() {
    Destroy(gameObject);
}