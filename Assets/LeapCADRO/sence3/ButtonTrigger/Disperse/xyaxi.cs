using UnityEngine;
using System.Collections;

public class xyaxi : MonoBehaviour {
	public GameObject cameral1;

	public add_position_control script;
	void OnTriggerExit(Collider collider)
	{
		script = cameral1.gameObject.GetComponent<add_position_control> ();

		script.aroud_spread ();


	}
}
