using UnityEngine;
using System.Collections;

public class PresentationScreen : MonoBehaviour{
	public GameObject logofont;
	public GameObject logobody;
	public GameObject logoname;



	//public GameObject iTweenLogoGT;
	//public GameObject presentsTextGT;
	//public GameObject whiteDiagonalGradient;
	public GameObject titleScreen;

	//Demonstrate how to animate GUITextures through their connected GameObject 
	//You could animate each GUITexture's Pixel Inset rect through iTween's ValueTo as well but this way is easier:
	void OnEnable(){
		//Reset (only needed since this example loops):
		Debug.Log("Run succeed");
		logobody.transform.position = new Vector3 (2f, 16f, 5f);//移动到的位置(物体本身的位置)
		logofont.transform.position = new Vector3 (5, 13, 5f);
		logoname.transform.position = new Vector3 (5,13, 5);
		//iTweenLogoGT.transform.position=new Vector3(.5f,.5f,.5f);
		//presentsTextGT.transform.position=new Vector3(.5f,.5f,.5f);
		//whiteDiagonalGradient.GetComponent<GUITexture>().color=new Color(.5f,.5f,.5f,.5f);
		
		//In:

		iTween.MoveFrom (logobody, iTween.Hash ("y", 250f, "time", 0.5, "delay", 0.2));//从x=250的地方移动到0；
		iTween.MoveFrom (logofont, iTween.Hash ("x",-250f, "time",0.5, "delay", 0.2));
		iTween.MoveFrom (logoname, iTween.Hash ("x",250f, "time",0.5, "delay", 0.2));


		//		iTween.FadeFrom(whiteDiagonalGradient,iTween.Hash("alpha",0,"time",.6,"delay",1));
//		iTween.MoveFrom(whiteDiagonalGradient,iTween.Hash("position",new Vector3(1.3f,1.3f,0),"time",.6,"delay",1));
//
//			iTween.MoveFrom(iTweenLogoGT,iTween.Hash("x",-.4,"time",.6,"delay",1.2));
//			iTween.MoveFrom(presentsTextGT,iTween.Hash("x",1.2,"time",.6,"delay",1.4));
			
		//Out:	
		//Debug.Break();
		iTween.MoveTo(logobody,iTween.Hash("y",-250f,"time",.6,"delay",2.6,"easetype","easeincubic"));
		iTween.MoveTo(logofont,iTween.Hash("y",-250f,"time",.6,"delay",2.6,"easetype","easeincubic"));
		iTween.MoveTo(logoname,iTween.Hash("y",-250f,"time",.6,"delay",2.6,"easetype","easeincubic","oncomplete","SwitchToTitleScreen","oncompletetarget",gameObject));

//		iTween.MoveTo(presentsTextGT,iTween.Hash("x",-.2,"time",.6,"delay",2.5,"easetype","easeincubic"));
//		iTween.MoveTo(iTweenLogoGT,iTween.Hash("x",1.5,"time",.6,"delay",2.6,"easetype","easeincubic"));
//		iTween.FadeTo(whiteDiagonalGradient,iTween.Hash("alpha",0,"time",.6,"delay",2.8,"easetype","easeincubic","oncomplete","SwitchToTitleScreen","oncompletetarget",gameObject));

	}
	
	void SwitchToTitleScreen(){
		gameObject.SetActiveRecursively(false);
		titleScreen.SetActiveRecursively(true);
	}
}

