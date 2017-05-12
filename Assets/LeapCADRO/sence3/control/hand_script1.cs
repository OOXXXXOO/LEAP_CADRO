using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using Leap;

//以下为单另测试代码块的时候将代码复制粘贴进update()中
//命名规则必须统一
//即： 
/* public HandController hc;      -控制器
    private HandModel  hm;         -手的模型
	public GameObject objectPrefab;  -预制模型 
	public Vector3 squizehand;     -手的握持点用于存储手在握持的瞬间的坐标
*/
//
//

/*脚本执行顺序说明：
先执行所有子脚本中的awake();
在执行所有脚本中的start();
然后执行所有update();
当所有脚本中的update();执行一遍之后，则执行fixupdate();和lateupdate();
即unity中没有多线程概念  
且如果在awake中掺杂了关于获取对象的定义
那么脚本之间执行顺序不同就有可能出现空指针
*/


//new behavior类在新的脚本中将被替换


public class hand_script1: MonoBehaviour {
	//主要继承于mono


	public HandController  hc;
	private HandModel  hm;
	public hand_script2 SCRIPT2;
	public Vector squizehand;
	public GameObject Ob = null;
	Frame currentFrame = null;//定义当前帧
	Frame lastFrame = null;//上一帧
	public float angel=0;
	Hand lefthand=null;
	Hand righthand = null;
	Hand last_lefthand = null;
	Hand last_righthand = null;
	bool lefthandexist = false;//判断左右手是否在场景中存在
	bool righthandexist = false;
	double scale=0f;
	double dis=0f;
	double last_dis= 0f;
	bool mark=true;
	float XR,YR,ZR=0f;
	public float moveindex=0.01f;
	public float scaleindex=0.01f;
	int sign=0;
	int sign1=0;
	public  void OnFrame (HandController controller)
	{
		// Get the most recent frame and report some basic information
		Frame frame = controller.GetFrame();
		Frame lastframe = controller.getlastframe ();

		Debug.Log ("Frame id: " + frame.Id                              //帧ID
			+ ", timestamp: " + frame.Timestamp                  //帧时间戳：从捕捉开始经过了多少毫秒
			+ ", hands: " + frame.Hands.Count                    //有几只手
			+ ", fingers: " + frame.Fingers.Count                //有几只手指
			+ ", tools: " + frame.Tools.Count                    //有几个工具
			+ ", gestures: " + frame.Gestures ().Count);         //手势计数
		Debug.Log ("lastFrame id: " + lastframe.Id
			+ ", lasttimestamp: " + lastframe.Timestamp
			+ ", lasthands: " + lastframe.Hands.Count
			+ ", lastfingers: " + lastframe.Fingers.Count
			+ ", lasttools: " + lastframe.Tools.Count
			+ ", lastgestures: " + lastframe.Gestures ().Count);


	}
	//1.用于打印相邻帧的相关数据
	//如果要在update中启用打印以返回相关数据
	//则：
	//OnFrame(hc);
	//即可
	//2.官方文档中并未给出调用上一帧的方法，因此
	//需要在handcontroller.cs中定义获取上一帧的方法
	//即：
	//public Frame getlastframe(){return leap_controller_.Frame (1);}//获取上一帧
	//以此在新的脚本中才能够使用获取帧的行为
	//即调用方法为上面代码所示：
	//Frame lastframe = controller.getlastframe ();
	//
	//




	bool squize(float radius)//判断手势是否为握持  阀值为35
	{
		if (radius < 37)
			return true;
		else
			return false;
	}
	double distance(Vector a,Vector b)//双手距离判断  退出机制触发阀值为35
	{

		double c=Math.Sqrt (Math.Pow ((a.x-b.x), 2) + Math.Pow ((a.y-b.y), 2) + Math.Pow ((a.z-b.z), 2));
		Debug.Log ("TWO hand distance="+c);
		return c;
	} 
	double handdis(Hand a,Hand b)
	{
		double c=Math.Sqrt (Math.Pow ((a.PalmPosition.x-b.PalmPosition.x), 2) + Math.Pow ((a.PalmPosition.y-b.PalmPosition.y), 2) + Math.Pow ((a.PalmPosition.z-b.PalmPosition.z), 2));
		return c;
	}
	Vector mid(Vector a,Vector b)
	{
		Vector c=null;
		c.x = (a.x + b.x) / 2;
		c.y = (a.y + b.y) / 2;
		c.z = (a.z + b.z) / 2;
		return c;

	}



	//____________________________________________________________________________________________________________






	void Awake () 
	{
		Debug.Log("Script ========= Awake");





	}




	//_____________________________________________________________________________________________________________











	void Start () {
		hc.GetLeapController().EnableGesture(Gesture.GestureType.TYPECIRCLE);
		hc.GetLeapController().EnableGesture(Gesture.GestureType.TYPESWIPE);
		hc.GetLeapController().EnableGesture(Gesture.GestureType.TYPE_SCREEN_TAP);
		hc.GetLeapController ().EnableGesture (Gesture.GestureType.TYPEKEYTAP);
		hc.GetLeapController ().EnableGesture (Gesture.GestureType.TYPEINVALID);

		Ob = SCRIPT2.cube;

		//--------test--------
		//ButtonDemoToggle.asd.centreitem = new EnhanceItem ();

		//Debug.Log (ButtonDemoToggle.asd.centreitem.flag);
		//ButtonDemoToggle.asd.centreitem.flag = 888;
		//Debug.Log (ButtonDemoToggle.asd.centreitem.flag);

		//打印数据
		//Debug.Log ("List----------------"+FileLoad.getFileLoadInstance().getEnhance()[0]);

		//---------------------

		//初始化握持点

	}//初始化函数中需开启手势识别以及一系列相关初始化工作开启手势







	//_____________________________________________________________________________________________________________





	void Update () {
		//OnFrame(hc);//平时不必调用
		this.currentFrame = hc.GetFrame ();
		Frame frame = hc.GetFrame ();
		Frame lastframe = hc.getlastframe ();
		GestureList gestures = this.currentFrame.Gestures ();
		sign = 0;




		foreach (var h in hc.GetFrame().Hands) {

			if (h.IsLeft) {
				lefthandexist = true;
				lefthand = h;


			}
			if (h.IsRight) {
				righthandexist = true;
				righthand = h;

				if (squize (righthand.SphereRadius) && (!squize (lefthand.SphereRadius))) {
					sign = 1;
					this.Ob.transform.Translate (righthand.PalmVelocity.x * scaleindex,
						righthand.PalmVelocity.y * scaleindex,
						-righthand.PalmVelocity.z * scaleindex, Space.World);
				}

			}
		}

		if((lefthandexist)&&(righthandexist))
		{
			if((squize(lefthand.SphereRadius))&&(squize(righthand.SphereRadius)&&lefthandexist))
			{
				sign = 1;
				foreach (Hand hand in frame.Hands)//当前帧的手
				{
					foreach(Hand lastframe_hand in lastframe.Hands)	
					{


						if(hand.IsLeft){lefthand=hand;}
						if(hand.IsRight){righthand=hand;}
						if(lastframe_hand.IsLeft){last_lefthand=lastframe_hand;}
						if(lastframe_hand.IsRight){last_righthand=lastframe_hand;}


					}
				}
				dis=handdis(lefthand,righthand);
				last_dis=handdis(last_lefthand,last_righthand);
				if(dis>last_dis)
				{Debug.Log("++++++++++");
					this.Ob.transform.localScale+=(new Vector3(scaleindex,scaleindex,scaleindex));}
				if(dis<last_dis)
				{Debug.Log("__________");
					this.Ob.transform.localScale+=(new Vector3(-scaleindex,-scaleindex,-scaleindex));}
				
			}
		}


		if (squize(lefthand.SphereRadius) && (!squize (righthand.SphereRadius))) {
			sign = 1;
		
			XR= righthand.RotationAngle(lastframe,Vector.XAxis);
			YR= righthand.RotationAngle(lastframe,Vector.YAxis);
			ZR= righthand.RotationAngle(lastframe,Vector.ZAxis);
			this.Ob.transform.Rotate(new Vector3(XR*100,YR*100,-ZR*100),Space.World);   
	
		}
		if (!squize (lefthand.SphereRadius) && lefthandexist&&(sign==0&&(sign1==0))) {
			sign1 = 1;
			sign = 1;
			Debug.Log ("NONE_control");
		
		}


		foreach (Gesture g in gestures) {

			Debug.Log (g.Type);
			if(g.Type==Gesture.GestureType.TYPECIRCLE)
			{
				CircleGesture circle=new CircleGesture(g);
				string clockwiseness;
				if (circle.Pointable.Direction.AngleTo (circle.Normal) <= Math.PI / 2) {
					//Clockwise if angle is less than 90 degrees
					clockwiseness = "clockwise";//顺时针
					//this.Ob.transform.Rotate(Vector3.down *1f, Space.World);


				} 

				else {
					clockwiseness = "counterclockwise";//逆时针
					//this.Ob.transform.Rotate(Vector3.up *1f, Space.World);

				}
				Debug.Log("clockwiseness="+clockwiseness);

			}


			//----code----//
			//此中循环为调取手势列表中预制的手势
			//leap在API中预置了手势即：
			//Gesture.GestureType.TYPESWIPE
			//Gesture.GestureType.TYPE_SCREEN_TAP
			//Gesture.GestureType.TYPEKEYTAP
			//Gesture.GestureType.TYPECIRCLE
			//等
			//以此预制手势来实现操作则在此编码

		}

























	}//update
}//namespace