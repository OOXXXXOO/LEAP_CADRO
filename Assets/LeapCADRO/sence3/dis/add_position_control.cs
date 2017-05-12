


using UnityEngine;
using System.Collections;

public class add_position_control : MonoBehaviour {

    /// <summary>
    /// 添加器
    /// </summary>
    public  GameObject goal;    
    public float disperse_time;
    public float restore_time;
    public float disperse_range;
	public bool bol=false;
	public hand_script2 script;
    public static add_position_control instance;
	public Vector3 reset_position = new Vector3 (0, 0, 300);
	public Quaternion reset_rotation = new Quaternion(0, 180, 0,0);

    void Awake()
    {

        instance = this;


        bol = false;
    }

    void Start()//程序开始为每个子物体添加刚体和寄存器
    {
		goal = script.cube;
		
        foreach (Transform child in goal.transform)
        {
			
            foreach (Transform grandson in child.transform)
            {
                if (grandson)
                {
                    grandson.gameObject.AddComponent<Rigidbody>();
                    grandson.gameObject.AddComponent(typeof(register));
                    grandson.gameObject.GetComponent<Rigidbody>().useGravity = false;
                }
                else
                    break;

            }
       
            child.gameObject.AddComponent(typeof(register));
            child.gameObject.AddComponent<Rigidbody>();	
            child.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }       
    }
    public void disperse()//        添加散开功能（2秒散开到2倍位置）使用后功能自动销毁s
    {
        if (!bol)
		{
			bol = true;

            foreach (Transform child in goal.transform)
            {

                foreach (Transform grandson in child.transform)
                {
                    if (grandson)
                    {
						grandson.gameObject.GetComponent<register> ().refresh_primary ();
                        grandson.gameObject.AddComponent(typeof(positioncontorl));
                    }
                    else
                        break;

                }
				child.gameObject.GetComponent<register> ().refresh_primary ();
                child.gameObject.AddComponent(typeof(positioncontorl));
			
            }
            
        }
    }


	void addsad()
	{
		goal.gameObject.AddComponent<positioncontorl> ();
	}

    public void aroud_spread()
    {
		
        foreach (Transform child in goal.transform)
        {
            foreach (Transform grandson in child.transform)
            {
                if (grandson)
                {
                    grandson.GetComponent<register>().Way_of_dis = 0;
                }
                else
                    break;
            }
            child.GetComponent<register>().Way_of_dis = 0;
        }
        disperse();
    }//               扩散散开
    public void transverse()
    {
        foreach (Transform child in goal.transform)
        {

            foreach (Transform grandson in child.transform)
            {
                if (grandson)
                {

                    grandson.GetComponent<register>().Way_of_dis = 1;
                }
                else
                    break;
            }
            child.GetComponent<register>().Way_of_dis = 1;
        }
        disperse();
    }//                 横向展开
    public void lengthways()
    {
        foreach (Transform child in goal.transform)
        {

            foreach (Transform grandson in child.transform)
            {
                if (grandson)
                {

                    grandson.GetComponent<register>().Way_of_dis = 2;
                }
                else
                    break;
            }
            child.GetComponent<register>().Way_of_dis = 2;
        }
        disperse();
    }//                 纵向展开
    public void _restore()//添加一键还原位置功能（1.5秒回到原位置）      使用后功能自动销毁
    {
		if (!bol)
        {
            bol = true;
            foreach (Transform child in goal.transform)
            {

                foreach (Transform grandson in child.transform)
                {
                    if (grandson)
                    {
                        grandson.gameObject.AddComponent(typeof(restore));
                    }
                    else
                        break;

                }
                child.gameObject.AddComponent(typeof(restore));
            }
        }
    }
}
