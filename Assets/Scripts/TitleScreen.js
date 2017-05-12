//guitextures:
var onePlayerButton : GameObject;
var splatTexture : GameObject;
var titleText : GameObject;
var titleTextBlock : GameObject;
var twoPlayerButton : GameObject;
var presentationScreen : GameObject;
var blackScreen : Texture2D;

//Demonstrate how to animate GUITextures through their connected GameObject 
//You could animate each GUITexture's Pixel Inset rect through iTween's ValueTo as well but this way is easier:
function OnEnable(){
	//In:
	iTween.ColorFrom(splatTexture,{"color":Color(1,1,0,0)});
	iTween.ScaleFrom(splatTexture,{"scale":Vector3(2,2,0),"time":.6});
	iTween.FadeFrom(titleTextBlock,{"alpha":0,"time":.8,"delay":.4});
	iTween.MoveFrom(titleText,{"x":-.8,"time":.8,"delay":.4});
	iTween.MoveFrom(onePlayerButton,{"y":-.5,"delay":1.4});
	iTween.MoveFrom(twoPlayerButton,{"y":-.5,"delay":1.5});
}

function SwitchToPresentationScreen(){
	iTween.CameraFadeAdd(blackScreen,99);
	iTween.CameraFadeTo(1,2);
	yield WaitForSeconds(2);
	iTween.CameraFadeDestroy();
	gameObject.SetActiveRecursively(false);
	presentationScreen.SetActiveRecursively(true);
}

function Update(){
	if(Input.GetMouseButtonDown(0)){
		SwitchToPresentationScreen();
	}
}