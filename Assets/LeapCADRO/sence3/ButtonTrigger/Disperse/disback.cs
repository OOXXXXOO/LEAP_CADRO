using UnityEngine;
using System.Collections;

public class disback : MonoBehaviour {
	public GameObject cameral1;
	public GameObject buttonmain;
	void OnTriggerExit(Collider collider)
	{
		iTween.RotateTo (buttonmain, iTween.Hash ("x", 180, "time", 0.5, "delay", 0.2));
		cameral1.gameObject.GetComponent<add_position_control> ().enabled = false;

	}
}
