var iTweenLogoGT : GameObject;
var presentsTextGT : GameObject;
var whiteDiagonalGradient : GameObject;
var titleScreen : GameObject;

//Demonstrate how to animate GUITextures through their connected GameObject 
//You could animate each GUITexture's Pixel Inset rect through iTween's ValueTo as well but this way is easier:
function OnEnable(){
	//Reset (only needed since this example loops):
	iTweenLogoGT.transform.position=Vector3(.5,.5,.5);
	presentsTextGT.transform.position=Vector3(.5,.5,.5);
	whiteDiagonalGradient.GetComponent.<GUITexture>().color=Color(.5,.5,.5,.5);
	
	//In:
	iTween.FadeFrom(whiteDiagonalGradient,{"alpha":0,"time":.6,"delay":1});
	iTween.MoveFrom(whiteDiagonalGradient,{"position":Vector3(1.3,1.3,0),"time":.6,"delay":1});
	iTween.MoveFrom(iTweenLogoGT,{"x":-.4,"time":.6,"delay":1.2});
	iTween.MoveFrom(presentsTextGT,{"x":1.2,"time":.6,"delay":1.4});
	
	//Out:	
	iTween.MoveTo(presentsTextGT,{"x":-.2,"time":.6,"delay":2.5,"easetype":"easeincubic"});
	iTween.MoveTo(iTweenLogoGT,{"x":1.5,"time":.6,"delay":2.6,"easetype":"easeincubic"});
	iTween.FadeTo(whiteDiagonalGradient,{"alpha":0,"time":.6,"delay":2.8,"easetype":"easeincubic","oncomplete":"SwitchToTitleScreen","oncompletetarget":gameObject});
}

function SwitchToTitleScreen(){
	gameObject.SetActiveRecursively(false);
	titleScreen.SetActiveRecursively(true);
}