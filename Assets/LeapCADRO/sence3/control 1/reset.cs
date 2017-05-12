using UnityEngine;
using System.Collections;

public class reset : MonoBehaviour {


	void OnTriggerExit()
	{
		Application.LoadLevel ("Sence3");
	}

}
