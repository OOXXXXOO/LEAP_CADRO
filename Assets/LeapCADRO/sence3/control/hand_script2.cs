using UnityEngine;
using System.Collections;
using Leap;
using System.Collections.Generic;
using System;

public class hand_script2 : MonoBehaviour {


	public Vector3 reset_position = new Vector3 (0, 0, 300);
	public Quaternion reset_rotation = new Quaternion(0, 180, 0,0);


	public string gameobjectname="logo";//文件名


	public GameObject cube;
	public HandController  hc;
	Frame currentFrame = null;//定义当前帧
	Hand lefthand=null;
	Hand righthand = null;
	bool lefthandexist = false;//判断左右手是否在场景中存在
	bool righthandexist = false;
	// Use this for initialization
	float XR,YR,ZR=0f;
	bool mark=false;
	float sweptAngle = 0;//初始化角度为零
	Vector swipedirection=null;

//	_______________________________________________________________手势识别定义


	List<Color> mat=new List<Color>();
	List<Transform> OB = new List<Transform> ();
	List<Transform> START = new List<Transform> ();
	public int OBJ_number=0;//子物体数量
	int i=0;






//	——————————————————————————————————————————————————————————————————————————————材质转换定义


	public GameObject main_son;



	double dis=0f;
	double last_dis= 0f;

	Hand last_lefthand = null;
	Hand last_righthand = null;

	public float moveindex=0.01f;
	public float scaleindex=0.01f;
	public GameObject main_MK;//选定子物体
	float length,width,height=0;

//	————————————————————————————————————————————————————————————————子物体选定定义

	double handdis(Hand a,Hand b)

	{
		double c=Math.Sqrt (Math.Pow ((a.PalmPosition.x-b.PalmPosition.x), 2) + Math.Pow ((a.PalmPosition.y-b.PalmPosition.y), 2) + Math.Pow ((a.PalmPosition.z-b.PalmPosition.z), 2));
		return c;
	}





	bool squize(float radius)//判断手势是否为握持  阀值为35
	{
		if (radius < 37)
			return true;
		else
			return false;
	}
	int checknumber(int ob_number,int number)
	{
		if (number < 0)
			return (number % ob_number) + ob_number;
		if(number>=0&&(number<ob_number))
			return number;
		if (number == ob_number)
			return 0;
		else
			return number % ob_number;

		//数据检查函数  
	}

	void setcolor(Transform obj)
	{
		obj.gameObject.GetComponent<MeshRenderer> ().material.color = Color.red;
	
		//设置颜色
	}
	void backcolor(Transform obj,int i)
	{
		Debug.Log ("mat " + mat [i]);
		obj.gameObject.GetComponent<MeshRenderer> ().material.color = mat [i];
	
	//backup color
	}




//	——————————————————————————————————————————————————————————————————————————————————————————————————————————————————————









	void Awake(){
	
	
	

		Debug.Log("___________script2_________Awake");

		gameobjectname = FileLoad.getFileLoadInstance ().getEnhance ();//获取文件名
		Debug.Log ("gameobjectname ="+gameobjectname);
		GameObject pre = (GameObject)Resources.Load(gameobjectname);//加载物体

		cube=(GameObject)Instantiate (pre, reset_position, reset_rotation);//实例化物体
	
	
	}













	// Use this for initialization
	void Start () {

		//Destroy (cube);



		hc.GetLeapController().EnableGesture(Gesture.GestureType.TYPECIRCLE);
		hc.GetLeapController().EnableGesture(Gesture.GestureType.TYPESWIPE);
		hc.GetLeapController().EnableGesture(Gesture.GestureType.TYPE_SCREEN_TAP);
		hc.GetLeapController ().EnableGesture (Gesture.GestureType.TYPEKEYTAP);
		hc.GetLeapController ().EnableGesture (Gesture.GestureType.TYPEINVALID);

		foreach (Transform child in cube.transform) {
		

			/*//针对孙物体的循环
			foreach (Transform grandson in child) {
			
				if (!grandson) {
					break;
				}
				else {
					OB.Add (child);
					START.Add (child);
					grandson.gameObject.AddComponent<Rigidbody> ();
					grandson.gameObject.GetComponent<Rigidbody> ().useGravity = false;	
				}
			
			}
           */

			//Debug.Log ("child name :" + child + " is NO." + i+"color="+child.gameObject.GetComponent<MeshRenderer>().material.color);
			i++;
			if (child.gameObject.GetComponent<MeshRenderer> () != null) {
				mat.Add (child.gameObject.GetComponent<MeshRenderer> ().material.color);
			}
			OB.Add (child);
			child.gameObject.AddComponent<Rigidbody> ();
        //______________________________________________________长宽高计算
		//  length = child.gameObject.GetComponent<MeshRenderer> ().bounds.max.x - child.gameObject.GetComponent<MeshRenderer> ().bounds.min.x;
		//	width=child.gameObject.GetComponent<MeshRenderer> ().bounds.max.y - child.gameObject.GetComponent<MeshRenderer> ().bounds.min.y;
		//	height=child.gameObject.GetComponent<MeshRenderer> ().bounds.max.z - child.gameObject.GetComponent<MeshRenderer> ().bounds.min.z;

			child.gameObject.AddComponent<BoxCollider> ();
			child.gameObject.GetComponent<BoxCollider> ().isTrigger = true;
			//child.gameObject.GetComponent<BoxCollider> ().center = child.gameObject.transform.position;
			//child.gameObject.GetComponent<BoxCollider> ().size = new Vector3 (length, width, height);
        //__________________________________________________________动态添加结束
			//代码实验总结 ：物体添加boxcollider的过程中  直接生成而不用计算的准确度更高

			child.gameObject.GetComponent<Rigidbody> ().useGravity = false;
			START .Add (child);
        //		——————————————————————————————————————————————————————————————————————————————————————————————————————————————————
			if(i>0)
				Debug.Log ("child : " + (i-1)+" name is"+START[i-1].name + " position is " + START[i-1].gameObject.GetComponent<Transform>().position);


		}
		OBJ_number = i;
		Debug.Log ("object  number" + OBJ_number);
		i = 0;
//		循环变量
		//______________________________________________结束此功能
		//this.gameObject.GetComponent<add_position_control> ().enabled = false;
		this.gameObject.GetComponent<ray>().enabled=false;


		this.gameObject.GetComponent<hand_script2>().enabled=false;


	}
//————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————











	// Update is called once per frame
	void Update () {

		this.currentFrame = hc.GetFrame ();
		GestureList gestures = this.currentFrame.Gestures ();
        Frame frame = hc.GetFrame();
        Frame lastframe = hc.getlastframe();



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

//		__________________________________________左右手取值




		if (squize(lefthand.SphereRadius) && (!squize (righthand.SphereRadius))) {

			XR= righthand.RotationAngle(lastframe,Vector.XAxis);
			YR= righthand.RotationAngle(lastframe,Vector.YAxis);
			ZR= righthand.RotationAngle(lastframe,Vector.ZAxis);
			main_son.transform.Rotate(new Vector3(XR*100,YR*100,-ZR*100),Space.World);

		}





		//_________________________________________________________________________________________

		if(squize(righthand.SphereRadius)&&(!squize(lefthand.SphereRadius)))
			this.main_son.transform.Translate(righthand.PalmVelocity.x*scaleindex,
				righthand.PalmVelocity.y*scaleindex,
				-righthand.PalmVelocity.z*scaleindex,Space.World);







//		______________________________________________________________________________________
		if (squize (lefthand.SphereRadius) && squize (righthand.SphereRadius)) {
			foreach (Hand hand in frame.Hands) {//当前帧的手

				foreach (Hand lastframe_hand in lastframe.Hands) {

					if (hand.IsLeft) {
						lefthand = hand;
					}
					if (hand.IsRight) {
						righthand = hand;
					}
					if (lastframe_hand.IsLeft) {
						last_lefthand = lastframe_hand;
					}
					if (lastframe_hand.IsRight) {
						last_righthand = lastframe_hand;
					}
				}
			}

			dis = handdis (lefthand, righthand);
			last_dis = handdis (last_lefthand, last_righthand);
			if (dis > last_dis) {
				Debug.Log ("++++++++++");
				main_son.transform.localScale += (new Vector3 (scaleindex, scaleindex, scaleindex));
			}
			if (dis < last_dis) {
				Debug.Log ("__________");
				main_son.transform.localScale += (new Vector3 (-scaleindex, -scaleindex, -scaleindex));
			}
		}














			//		______________________________________________________________________________________close hand guerture


		//Debug.Log ("hand dis= " + handdis (lefthand, righthand)); 



		if (handdis (lefthand, righthand) <=40.0f) {
			Debug.Log ("___________________________________");
			//Application.LoadLevel ("controller_sence");
			foreach(Transform CHILD in cube.transform){
				main_MK = CHILD.gameObject;

			    i = 0;

				main_MK.transform.position=new Vector3(START[i].transform.position.x,START[i].transform.position.y,
					START[i].transform.position.z
				);
				//main_MK.transform.rotation=
		

				i++;
			}

		
		
		
		}


//		_______________________________________________





	}




	void FixedUpdate()
	{
		
		this.currentFrame = hc.GetFrame ();
		GestureList gestures = this.currentFrame.Gestures ();

		//foreach (Gesture g in gestures) {


			/*
			Debug.Log (g.Type);
			if (g.Type == Gesture.GestureType.TYPE_KEY_TAP) {
				Debug.Log ("tap____________________________");


				setcolor (OB[i]);




			}
			*/
			/*
			if (g.Type == Gesture.GestureType.TYPE_CIRCLE) {


				CircleGesture circle=new CircleGesture(g);
				string clockwiseness;
				if (circle.Pointable.Direction.AngleTo (circle.Normal) <= Math.PI / 2) {

					sweptAngle += circle.Progress/50;
					i = (int)sweptAngle;
					//					____________________________________________________________________________________________________________颜色变换&子物体选定
					Debug.Log("up");	
			

					setcolor (OB [checknumber(OBJ_number,i)]);
					main_son = OB [checknumber (OBJ_number,i)].gameObject;
					backcolor (OB [checknumber (OBJ_number,i-1)], checknumber (OBJ_number,i-1));
					Debug.Log ("checknum="+checknumber (OBJ_number,i-1)+" i="+i+"  "+OBJ_number);
					//					________________________________________________________________________________________________________通用部分
					}


				else {
					sweptAngle -= circle.Progress/50;
					i = (int)sweptAngle;
					//		          	________________________________________________________________________________________________

					Debug.Log("down");


					setcolor (OB [checknumber (OBJ_number,i)]);
					main_son = OB [checknumber (OBJ_number,i)].gameObject;
					backcolor (OB [checknumber (OBJ_number,i+1)], checknumber (OBJ_number,i+1));
					Debug.Log ("checknum="+checknumber (OBJ_number,i+1)+"  i="+i);
					//					______________________________________________________________________________________________________通用部分
		         }
		    }
		    */




		//}







	}






}
