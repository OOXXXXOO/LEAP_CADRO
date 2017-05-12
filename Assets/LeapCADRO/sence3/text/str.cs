using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class str : MonoBehaviour {

	string str1;
	float _updateinterval=1f;
	float _accum=0f;
	int _frames=0;
	float timeleft=0;
	public GameObject  text1;
	public GameObject mainc=null;
	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
		timeleft -= Time.deltaTime;
		_accum += Time.timeScale / Time.deltaTime;
		++_frames;
		if (timeleft <= 0) {
			float fps = _accum / _frames;
			str1 = System.String.Format ("FPS:{0:F1}\n" +
				"Model name:{1}\n" +
				"Main Obj name:{2}\n" +
				"Model son number{3}\n"
				,fps
				,mainc.GetComponent<hand_script2>().cube.name.ToString()
				,mainc.GetComponent<hand_script2>().main_son.ToString()
				,mainc.GetComponent<hand_script2>().OBJ_number,ToString());

		
		}
		text1.gameObject.GetComponent<Text> ().text = str1;

	
	}
}
