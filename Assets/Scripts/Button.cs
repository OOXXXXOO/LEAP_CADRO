using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Button : MonoBehaviour {
	public Text text; 
	public int num=0;
	// Use this for initialization
	void Start () { 
	}
	
	// Update is called once per frame
	void Update(){
		++num;
		if (num > 2) {
			//f (Input.GetMouseButtonDown (0)) {
				//StartCoroutine(SwitchToPresentationScreen());
				if (Application.loadedLevelName == "C#") {
				
					Application.LoadLevel ("Menu 3D");
				} else if (Application.loadedLevelName == "Menu 3D") {
					Application.LoadLevel ("C#");
				}
			
			//}
		}
	}
}
