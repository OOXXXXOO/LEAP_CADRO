using UnityEngine;
using System.Collections;

public class Enhancescript_enter_trigger : MonoBehaviour {
	float time=0;




	public  EnhanceItem centreitem = null;

	//--------------------------跨场景的单列类在FileLoad类中
	//FileLoad fileLoad = FileLoad.getFileLoadInstance();


	void OnTriggerEnter (Collider collider)
	{
		Debug.Log (" Enhancescript_enter_triggerEnter");
	}

	void OnTriggerStay(Collider collider)
	{

		time+=Time.deltaTime;
		Debug.Log (time);

		if (time >=3) {
			centreitem=GameObject.Find("EnhanceScrollViewController").GetComponent<EnhancelScrollView>().centerItem;

			Debug.Log ("__________________________________________________" + centreitem);

			//添加数据
			//fileLoad.getEnhance ().Add (centreitem.ToString());


			Application.LoadLevel ("controller_sence");
			//Debug.Log("enter "+time+" s next sence");
		}

	}
	void OnTriggerExit(Collider collider)
	{
		Debug.Log ("Exit");
		time = 0;
	}

}
