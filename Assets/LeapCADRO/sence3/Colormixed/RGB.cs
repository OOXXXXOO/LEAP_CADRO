using UnityEngine;
using System.Collections;

public class RGB : MonoBehaviour {
	public GameObject RedTop = null;
	public GameObject GreenTop=null;
	public GameObject BlueTop = null;
	public Color rgb=Color.white;
	float r,g,b=0;
	public GameObject applytarget = null;
	// Use this for initialization
	void Start () {
		
	
	}
	
	// Update is called once per frame
	void Update () {

		r=(RedTop.transform.localPosition.x+4.2f)/8.4f;
		g=(GreenTop .transform.localPosition.x+4.2f)/8.4f;	
		b=(BlueTop.transform.localPosition.x+4.2f)/8.4f;
		rgb=new Color(r,g,b,1);
		applytarget.gameObject.GetComponent<Renderer> ().material.color = rgb;

	
	}
}
