using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	public class UserData{
		
		public string name;
		public string age;
		public string height;
		public string hand;
		
		public UserData(string _name, string _age,string _height)
		{
			age = _age;
			height = _height;
			name = _name;
		}
		
	}
	public class Globe
	{
		
		public static GameObject ListPanel;
		public static int list_count;
		public static int list_currentIndex;
		public static int list_offset;
		public static string list_go_name;
		
	}
	//是否触摸
	bool isTouch = false;
	//是否向左滑动
	bool isRight = false;
	//是否向右滑动
	bool isLeft = false;
	//是否正在滑动中
	bool isOnDrag = false;
	
	//滑动中事件
	void OnDrag (Vector2 delta)
	{
		//为了避免事件冲突
		//这里只判断一个滑动的事件
		if(!isTouch)
		{
			if(delta.x > 0.5)
			{
				//向左滑动
				isRight = true;
				isOnDrag = true;
			}else if(delta.x < -0.5)
			{
				//向右滑动
				isLeft = true;
				isOnDrag = true;
			}
			isTouch = true;
		}
	}
	
	//滑动后松手调用OnPress事件
	void OnPress()
	{
		//重新计算当前界面的ID
		if(Globe.list_currentIndex < Globe.list_count && isLeft)
		{
			Globe.list_currentIndex++;
		}
		
		if(Globe.list_currentIndex >0 && isRight)
		{
			Globe.list_currentIndex--;
		}
		
		//表示一次滑动事件结束
		isTouch = false;
		isLeft = false;
		isRight = false;
	}
	
	void Update()
	{
		//这个方法就是本节内容的核心
		//Globe.ListPanel 这个对象是我们在historyInit脚本中得到的，它用来当面相册面板对象使用插值，让面板有惯性的滑动。
		
		//Vector3.Lerp() 这个是一个插值的方法， 参数1 表示开始的位置 参数2 表示结束的位置  参数3 表示一共所用的时间， 在Time.deltaTime * 5 时间以内 Update每一帧中得到插值当前的数值，只到插值结束
		
		//-(Globe.list_currentIndex * Globe.list_offset) 得到当前需要滑动的目标点。
		//请大家仔细看这个方法。
		
		Globe.ListPanel.transform.localPosition =Vector3.Lerp(Globe.ListPanel.transform.localPosition, new Vector3(-(Globe.list_currentIndex * Globe.list_offset),0,0), Time.deltaTime * 5);
	}
	
	void OnClick ()
	{
		//当点击某个item时进入这里
		
		if(!isOnDrag)
		{
			//如果不是在拖动中 进入另一个场景
			Application.LoadLevel(Globe.list_go_name);
		}
		else
		{
			//否则等待用户重新发生触摸事件
			isOnDrag = false;
		}
	}
}