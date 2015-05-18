#pragma strict

function Start () {
    var randomNumber = Random.Range(0.0, 1.0);
    Debug.Log(randomNumber);
    if (randomNumber <= 0.5) {
        GetComponent.<Rigidbody2D>().AddForce(new Vector2(8, 1));
    } else {
        GetComponent.<Rigidbody2D>().AddForce(new Vector2(-8, 1));
    }
}

function OnCollisionEnter2D(colInfo : Collision2D) {
    if (colInfo.collider.tag == "Player") {
        var velY = GetComponent.<Rigidbody2D>().velocity.y;
        GetComponent.<Rigidbody2D>().velocity.y = GetComponent.<Rigidbody2D>().velocity.y / 2 + colInfo.collider.GetComponent.<Rigidbody2D>().velocity.y / 3;
    }
}