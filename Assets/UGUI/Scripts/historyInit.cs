using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class historyInit  :MonoBehaviour
{
	public class Globe
	{
		
		public static GameObject ListPanel;
		public static int list_count;
		public static int list_currentIndex;
		public static int list_offset;
		public static string list_go_name;
		
	}
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
	//相册列表的每一个item
	public GameObject prefab;
	//灰色的小点
	public GameObject prefabhui;
	//白色的小点
	public GameObject prefabbai;
	//另外一个显示面板
	//用来放置灰色、白色小点
	public Transform  ponit;
	//白色小点的临时对象
	private GameObject bai;
	
	//链表，用来记录每一个相册中的一些用户信息
	List<UserData> users = new List<UserData>();
	//灰色、白色小点下方的起始位置。
	int start;
	
	void Start ()
	{
		//将当前面板对象储存在全局静态变量中
		Globe.ListPanel = gameObject;
		loadSQL ();
		initItem();
	}
	
	//以前是读取数据库
	//写例子程序就没必要使用数据库了
	//这里直接写4个死值，当然数量是灵活使用的
	
	void loadSQL ()
	{
		//表示一共向U3D世界中添加横向4个相册队列
		for(int i =0; i< 4; i ++)
		{
			//简单的对象储存
			string  name =  "momo =" + i;
			string  age =  "26 = " + i;
			string  height = "183 ="+ i;
			users.Add(new UserData(name,age,height));
		}
		
	}
	
	void initItem()
	{
		//因为下方灰色 白色的小点需要根据相册列表的数量来计算居中显示
		int size = users.Count;
		//乘以16表示计算所有小点加起来的宽度
		int length = (size - 1) * 16;
		//得到下方灰色 白色 小点的居中起始位置
		start = (-length) >>1;
		
		for(int i=0; i< size; i++)
		{
			//把每一个相册加入相册列表
			GameObject o  =(GameObject) Instantiate(prefab);
			//设置这个对象的父类为 当前面板
			o.transform.parent = transform;
			//设置相对父类的坐标，这些值可根据自己的情况而设定，
			//总之就是设置相册列表中每一个item的坐标，让它们横向的排列下来就行
			o.transform.localPosition = new Vector3(25 + i* 243,-145f,-86f);
			//设置相对父类的缩放
			o.transform.localScale= new Vector3(0.7349999f,0.66f,0.7349999f);
			
			//得到每一个user的信息
			UserData data = users[i];
			//遍历每一个相册对象的子组件，
			UILabel []label = o.GetComponentsInChildren<UILabel>();
			//拿到UILabel并且设置它们的数据
			label[0].text = data.age;
			label[1].text = data.height;
			label[2].text = data.name;
			
			//把每一个灰色小点加入3D世界
			GameObject hui  =(GameObject) Instantiate(prefabhui);
			//设置灰色小点的父类为另外一个面板
			hui.transform.parent = ponit;
			//设置每一个灰色小点的位置与缩放，总之让它们居中排列显示在相册列表下方。
			hui.transform.localPosition = new Vector3(start + i* 16,-120f,0f);
			hui.transform.localScale= new Vector3(8,8,1);
			
			//深度 因为是先在屏幕下方绘制4个灰色的小点， 然后在灰色上面绘制白色小点
			//表示当前的窗口ID 所以深度是为了设置白色小点在灰色小点之上绘制
			hui.GetComponent<UISprite>().depth = 0;
		}
		
		//下面的数据是把当前初始化的数据放在一个static类中
		//在Move脚本中就可以根据这里的数据来判断了。
		//滑动列表的长度
		Globe.list_count = size -1;
		//相册每一项的宽度
		Globe.list_offset = 243;
		//当前滑动的索引
		Globe.list_currentIndex = 0;
		//点击后打开的新游戏场景
		Globe.list_go_name= "LoadScene";
		
		//把白色小点也加载在3D世界中
		bai  =(GameObject) Instantiate(prefabbai);
		//设置它的深度高于 灰色小点，让白色小点显示在灰色小点之上
		bai.GetComponent<UISprite>().depth = 1;
		//设置白色小点的位置
		setBaiPos();
	}
	
	void Update()
	{
		//当用户滑动界面
		//在Update方法中更新
		//当前白色小点的位置
		setBaiPos();
	}
	
	void setBaiPos()
	{
		//Globe.list_currentIndex 就是当前界面的ID
		//根据ID 重新计算白色小点的位置
		bai.transform.parent = ponit;
		bai.transform.localPosition = new Vector3(start + Globe.list_currentIndex* 16,-120f,-10f);
		bai.transform.localScale= new Vector3(8,8,1);
		
	}
}