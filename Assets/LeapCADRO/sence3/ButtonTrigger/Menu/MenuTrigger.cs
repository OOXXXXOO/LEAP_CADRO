using UnityEngine;
using System.Collections;

public class MenuTrigger : MonoBehaviour {
	public GameObject menu=null;
	public ButtonDemoToggle script=null;
	public GameObject menulist = null;
	public GameObject mainc = null;
	void OnTriggerExit(Collider collider)
	{
		script.ButtonTurnsOff();

		if(mainc.gameObject.GetComponent<switchscript>().script==2)
			mainc.gameObject.GetComponent<ray> ().enabled = false;
		menu.transform.position = new Vector3 (0,74,60);
		iTween.MoveFrom (menu, iTween.Hash ("x", -200, "time", 0.5, "delay", 0.2));
		iTween.MoveTo (menulist, iTween.Hash ("z", 500, "time", 0.5, "delay", 0.2));
	}
}
	