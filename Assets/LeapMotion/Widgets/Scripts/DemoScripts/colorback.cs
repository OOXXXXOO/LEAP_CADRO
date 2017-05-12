using UnityEngine;
using System.Collections;

public class colorback : MonoBehaviour {

	public GameObject colormenu=null;
	public ButtonDemoToggle script;
	public GameObject menulist = null;
	public GameObject mainc=null;
	void OnTriggerExit(Collider collider)
	{
		//colormenu.transform.position = new Vector3 (0, 25, 35);
		script.ButtonTurnsOff ();
		if(mainc.gameObject.GetComponent<switchscript>().script==2)
			mainc.gameObject.GetComponent<ray> ().enabled = false;
		iTween.MoveTo (colormenu, iTween.Hash ("x", 250, "time", 0.5, "delay", 0.2));
		iTween.MoveTo (menulist, iTween.Hash ("z", 72.8, "time", .5, "delay", .2));
	}
}
