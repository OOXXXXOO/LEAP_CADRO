using UnityEngine;
using System.Collections;

public class exit : MonoBehaviour {
	float time=0;

	void OnTriggerEnter (Collider collider)
	{
		Debug.Log (" Enhancescript_EXIT_triggerEnter");
	}

	void OnTriggerStay(Collider collider)
	{

		time+=Time.deltaTime;
		Debug.Log (time);

		if (time >=3) {
			Application.Quit ();
			Debug.Log ("____________________exit__________________");
			

				
		}

	}
	void OnTriggerExit(Collider collider)
	{
		Debug.Log ("Exit");
		time = 0;
	}
}
