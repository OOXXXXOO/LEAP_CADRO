using UnityEngine;
using System.Collections;
using Leap;
public class switchscript : MonoBehaviour {
	public ButtonDemoToggle button;
	public GameObject buttonobj=null;

	public int script=0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		 
		script = buttonobj.gameObject.GetComponent<SwitchscriptTrigger> ().mark;
		switch(script+1) {
		case 1:
			this.GetComponent<hand_script1> ().enabled = true;
			this.GetComponent<hand_script2> ().enabled = false;
			this.GetComponent<ray> ().enabled = false;
			break;
		case 2:
			this.GetComponent<hand_script1> ().enabled = false;
			this.GetComponent<hand_script2> ().enabled = true;
			this.GetComponent<ray> ().enabled = false;
			break;
		case 3:
			this.GetComponent<hand_script1> ().enabled = false;
			this.GetComponent<hand_script2> ().enabled = false;
			this.GetComponent<ray> ().enabled = true;
			break;
		default:
			break;
		}
	
	}
}
