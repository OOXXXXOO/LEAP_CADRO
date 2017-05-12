using UnityEngine;
using System.Collections;

public class Colormenu : MonoBehaviour {

	public GameObject colormenu=null;
	public GameObject menu=null;
	public ButtonDemoToggle script;
	public GameObject mainc=null;
	void OnTriggerExit(Collider collider)
	{
		colormenu.transform.position = new Vector3 (0, 60, 60);
		script.ButtonTurnsOff ();
		if(mainc.gameObject.GetComponent<switchscript>().script==2)
			mainc.gameObject.GetComponent<ray> ().enabled = false;
		iTween.MoveTo (menu, iTween.Hash ("x", -250, "time", .5, "delay", .2));
		iTween.MoveFrom (colormenu, iTween.Hash ("x", 250, "time", 0.5, "delay", 0.2));

	}
}
