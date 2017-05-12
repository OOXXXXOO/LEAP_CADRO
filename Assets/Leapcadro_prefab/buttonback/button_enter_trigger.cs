using UnityEngine;
using System.Collections;

public class button_enter_trigger : MonoBehaviour {
	public GameObject effect;
	public ButtonDemoToggle toggle;
	public GameObject enhance;
	public string str;

	void OnTriggerEnter(Collider collider)
	{
		toggle.ButtonTurnsOn ();
		effect.gameObject.GetComponent<ParticleRenderer> ().enabled = true;
		str = enhance.GetComponent<EnhancelScrollView> ().centerItem.name.ToString();
		//Debug.Log ("asdfasdfasvafcd");
		FileLoad.getFileLoadInstance ().setEnhance (str);
	}
	void OnTriggerExit(Collider collider)
	{
		toggle.ButtonTurnsOff ();

		effect.gameObject.GetComponent<ParticleRenderer> ().enabled = false;

	}

}
