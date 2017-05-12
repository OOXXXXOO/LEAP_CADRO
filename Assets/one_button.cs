using UnityEngine;
using System.Collections;

public class one_button : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI()    
	{    
		//开始按钮    
		if(GUI.Button(new Rect(0,10,100,30),"press me "))    
		{    
			//System.Console.WriteLine("hello world");  
			Application.LoadLevel("EnhanceScrollView");
		}    
		
	} 
}
