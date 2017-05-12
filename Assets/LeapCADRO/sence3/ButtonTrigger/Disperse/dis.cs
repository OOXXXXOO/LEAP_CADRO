using UnityEngine;
using System.Collections;

public class dis : MonoBehaviour {
	public GameObject camera1=null;
	public GameObject buttonmain;
	void OnTriggerExit(Collider collider)
	{
		iTween.RotateTo (buttonmain, iTween.Hash ("x", 180, "time", 0.5, "delay", 0.2));
	
		//camera1.gameObject.GetComponent<add_position_control> ().enabled = true;

		
	}
}
