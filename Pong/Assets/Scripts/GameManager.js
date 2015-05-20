#pragma strict

static var playerScore01 : int = 0;
static var playerScore02 : int = 0;

var theSkin : GUISkin;

static function Score(wallName : String) {
    if (wallName == "RightWall") {
        playerScore01 += 1;
    } else {
        playerScore02 += 1;
    }
}

function OnGUI() {
    GUI.skin = theSkin;
    GUI.Label(new Rect(Screen.width/2-150-12, 25, 100, 100), "" + playerScore01);
    GUI.Label(new Rect(Screen.width/2+150-12, 25, 100, 100), "" + playerScore02);
}