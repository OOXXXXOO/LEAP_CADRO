using UnityEngine;
using System.Collections;

public class SwitchscriptTrigger : MonoBehaviour {

	public int script_mark=2;
	int i=0;
	public int mark=0;
	public GameObject view1;
	public GameObject view2;
	public GameObject view3;
	public ButtonDemoToggle script;
	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerExit()
	{
		script.ButtonTurnsOff ();
		mark = i++ % 3;

		Debug.Log (mark+1);

		switch (mark + 1) {
		case 1:
			if (script_mark == 1) {
				// iTween.MoveFrom (view1, iTween.Hash ("x", -300, "time", 0.5, "delay", 0.5));
				script_mark++;
			
			} else {
				iTween.MoveTo (view3, iTween.Hash ("x", -300, "time", 0.8, "delay", 0.5f));
				iTween.MoveTo (view1, iTween.Hash ("x", 2.2, "time", 0.8, "delay", 0.5));
				script_mark++;
			}
			break;
		case 2:
			if (script_mark == 2) {
				iTween.MoveTo (view1, iTween.Hash ("x", -300, "time", 0.8, "delay", 0.5f));
				script_mark++;
			} else {
				iTween.MoveTo (view1, iTween.Hash ("x", -300, "time", 0.8, "delay", 0.5f));
				iTween.MoveTo (view2, iTween.Hash ("x", 7.5, "time", 0.8, "delay", 0.5));
				script_mark++;
			}
			break;
		case 3:
			if (script_mark == 3) {
				iTween.MoveTo (view2, iTween.Hash ("x", -300, "time", 0.8, "delay", 0.5f));
				script_mark++;
			
			} else {
				iTween.MoveTo (view2, iTween.Hash ("x", -300, "time", 0.8, "delay", 0.5f));
				iTween.MoveTo (view3, iTween.Hash ("x", 6.5, "time", 0.8, "delay", 0.5));
				script_mark++;
			}
			break;
		default:
			break;
		}

	}

		
}
