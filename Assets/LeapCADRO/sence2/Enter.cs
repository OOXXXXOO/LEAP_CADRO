using UnityEngine;
using System.Collections;

public class Enter : MonoBehaviour {

	float time=0;

	void OnTriggerEnter (Collider collider)
	{
		Debug.Log (" Enhancescript_Enter_triggerEnter");
	}

	void OnTriggerStay(Collider collider)
	{

		time+=Time.deltaTime;
		Debug.Log (time);

		if (time >=3) {
			Debug.Log ("Appload");

			Application.LoadLevel ("Sence3");

		}

	}
	void OnTriggerExit(Collider collider)
	{
		Debug.Log ("Exit");
		time = 0;
	}
}
