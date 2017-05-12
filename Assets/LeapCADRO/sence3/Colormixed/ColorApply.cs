using UnityEngine;
using System.Collections;

public class ColorApply : MonoBehaviour {

	public GameObject camera1=null;
	public GameObject menu = null;
	public ButtonDemoToggle script;
	GameObject mask=null;
	void OnTriggerExit (Collider collider)
	{
		mask= camera1.gameObject.GetComponent<hand_script2> ().main_son;
		mask.gameObject.GetComponent<Renderer> ().material.color = menu.gameObject.GetComponent<RGB> ().rgb;
		script.ButtonTurnsOff();
		Debug.Log("apply");

		//




		//颜色应用方法
	
	}
}
