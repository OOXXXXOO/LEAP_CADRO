using UnityEngine;
using System.Collections;

public class back : MonoBehaviour {
	public GameObject menu=null;
	public ButtonDemoToggle script;
	public GameObject menulist = null;
	public GameObject mainc = null;
	void OnTriggerExit(Collider collider)
	{
		script.ButtonTurnsOff ();
		if(mainc.gameObject.GetComponent<switchscript>().script==2)
			mainc.gameObject.GetComponent<ray> ().enabled = false;
		iTween.MoveTo (menu, iTween.Hash ("x", -250, "time", .5, "delay", .2));
		iTween.MoveTo (menulist, iTween.Hash ("z", 72.8, "time", .5, "delay", .2));
	}
}
