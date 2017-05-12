using UnityEngine;
using System.Collections;

public class positioncontorl : MonoBehaviour {
    /// <summary>
    /// 散开功能
    /// </summary>
    private  float time;//运动时间  
    private float a;//时间常量
    public GameObject cube;//目标物体
    private Vector3 tagr;//目标位置
    private Vector3 dis;//移动路程
    private Vector3 p_speed;//移动速度
    private float disperse_range;

    void Awake()
    {
        cube = this.gameObject;
        time = add_position_control.instance.disperse_time; 
        a = 0;
        disperse_range = add_position_control.instance.disperse_range;
        //单次散开 
        // tagr=this.GetComponent<register>().primary *disperse_range+add_position_control.instance.goal.transform.position;  
        //多次散开
        if (this.GetComponent<register>().Way_of_dis == 0)
        {
            tagr = this.GetComponent<register>().primary * disperse_range + this.GetComponent<Transform>().position;                  //扩散散开
        }
        else if (this.GetComponent<register>().Way_of_dis == 1)
        {
            tagr = new Vector3(this.GetComponent<register>().primary.x, 0, this.GetComponent<register>().primary.z) * disperse_range  //横向散开
                    + this.GetComponent<Transform>().position;
        }
        else
        {
            tagr = new Vector3(0, this.GetComponent<register>().primary.y, this.GetComponent<register>().primary.z) * disperse_range   //纵向散开
                    + this.GetComponent<Transform>().position;
        }

  
    }

    void Start()
    {
       dis=tagr-cube.transform.position;
       p_speed = dis / time;
       cube.GetComponent<Rigidbody>().velocity = p_speed;//赋予物体速度
    }
    void Update()
    {
        if (a	 > time)
        {
                cube.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);//物体停止
			add_position_control.instance.bol = false;//动作结束
                Destroy(this);//散开后自动销毁
                
        }
        else
        {
         a +=Time.deltaTime;
        }
    }
}
