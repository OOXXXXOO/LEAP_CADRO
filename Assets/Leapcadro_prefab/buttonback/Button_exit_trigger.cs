using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Button_exit_trigger : MonoBehaviour {
	public GameObject effect;
	public ButtonDemoToggle toggle;

	// Use this for initialization

	void OnTriggerEnter(Collider collider)
	{
		toggle.ButtonTurnsOn ();
		effect.gameObject.GetComponent<ParticleRenderer> ().enabled = true;

	}
	void OnTriggerExit(Collider collider)
	{
		toggle.ButtonTurnsOff ();

		effect.gameObject.GetComponent<ParticleRenderer> ().enabled = false;

	}
}
