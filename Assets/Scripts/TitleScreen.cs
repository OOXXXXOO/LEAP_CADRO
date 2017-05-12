 using UnityEngine;
using System.Collections;  
using System.Reflection; 
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Leap;
public class TitleScreen : MonoBehaviour{
	//guitextures:
	public GameObject onePlayerButton;
	public GameObject splatTexture;
	public GameObject titleText;
	public GameObject titleTextBlock;
	public GameObject twoPlayerButton;
	public GameObject presentationScreen;
	public Texture2D blackScreen;
	public HandController hc;
	//_________________________________________________________________
//	以上 为title部分定义  以下  为leap部分定义


	Hand lefthand=null;
	Hand righthand = null;
	bool lefthandexist = false;//判断左右手是否在场景中存在
	bool righthandexist = false;











	
	//Demonstrate how to animate GUITextures through their connected GameObject 
	//You could animate each GUITexture's Pixel Inset rect through iTween's ValueTo as well but this way is easier:
	void OnEnable(){
		//In:
		iTween.ColorFrom(splatTexture,iTween.Hash("color",new Color(1,1,0,0)));
		iTween.ScaleFrom(splatTexture,iTween.Hash("scale",new Vector3(2,2,0),"time",.6));
		//背景设置 

		iTween.FadeFrom(titleTextBlock,iTween.Hash("alpha",0,"time",.8,"delay",.4));
		iTween.MoveFrom(titleText,iTween.Hash("x",-.8,"time",.8,"delay",.4));


		//按钮设置项  gameobject  可用于button slider的控制项
		iTween.MoveFrom(onePlayerButton,iTween.Hash("y",-.5,"delay",1.4));
		iTween.MoveFrom(twoPlayerButton,iTween.Hash("y",-.5,"delay",1.5));
	}
	
	IEnumerator SwitchToPresentationScreen(){
		iTween.CameraFadeAdd(blackScreen,99);
		iTween.CameraFadeTo(1,2);
		yield return new WaitForSeconds(2);
		iTween.CameraFadeDestroy();
		gameObject.SetActiveRecursively(false);
		presentationScreen.SetActiveRecursively(true);
	}


	/*
	 * 
	 * Enumerable接口是非常的简单，只包含一个抽象的方法GetEnumerator()，
	 * 它返回一个可用于循环访问集合的IEnumerator对象。IEnumerator对象有什么呢？
	 * 它是一个真正的集合访问器，没有它，就不能使用foreach语句遍历集合或数组，
	 * 因为只有IEnumerator对象才能访问集合中的项，假如连集合中的项都访问不了，
	 * 那么进行集合的循环遍历是不可能的事情了。那么让我们看看IEnumerator接口有定义了什么东西。
	 * IEnumerator接口定义了一个Current属性，
	 * MoveNext和Reset两个方法，
	 * 既然IEnumerator对象时一个访问器，那至少应该有一个Current属性，来获取当前集合中的项
       MoveNext方法只是将游标的内部位置向前移动（就是移到一下个元素而已），要想进行循环遍历，不向前移动一下怎么行呢？

	 */












	void Update(){


		
		foreach (var h in hc.GetFrame().Hands) {
			
			if (h.IsLeft) {
				lefthandexist = true;
				lefthand = h;
			}
			if (h.IsRight) {
				righthandexist = true;
				righthand = h;
        	}
		}
		if (lefthandexist || righthandexist) {
			
			Application.LoadLevel("Sence2");
		}





		if(Input.GetMouseButtonDown(0)){
			//StartCoroutine(SwitchToPresentationScreen());
			if(Application.loadedLevelName=="C#")
			{
				Debug.Log("Menu sence");

				Application.LoadLevel("Sence2");
			}

			
		}
	}
}