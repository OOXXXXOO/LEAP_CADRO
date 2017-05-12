using UnityEngine;
using System.Collections;

public class reload : MonoBehaviour {


	void OnTriggerExit()
	{
		Application.LoadLevel ("sence2");
	}
}
