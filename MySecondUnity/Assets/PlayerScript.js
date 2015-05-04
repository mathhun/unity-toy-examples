#pragma strict

//var x : int = 5;

function Start () {
    //Debug.Log("hello unity -> " + x);
}

function Update () {
    //transform.position.z += 0.5;
    //transform.position += Vector3(0, 0, 1);
    //transform.Translate(0, 0, 2);

    if (Input.GetButtonUp("Jump")) {
        Debug.Log("Jumped");
    }

    var x : float = Input.GetAxis("Horizontal");
    transform.Translate(x * 0.2, 0, 0);
}

function OnCollisionEnter (obj : Collision) {
    if (obj.gameObject.name == "RightWall") {
        Debug.Log("Hit R!");
    }
}