using UnityEngine;
using System.Collections;

public class restore : MonoBehaviour {
    /// <summary>
    /// 一键还原功能
    /// </summary>
	private  float time;//一次运动时间
	private GameObject cube;//目标物体
	private Vector3 tagr;//目标位置
	private Vector3 dis;//移动路程
	private Vector3 p_speed;//移动速度
    private bool IsStop;//一次运动完成标志

	void Awake()
	{
		cube = this.gameObject;
		time = add_position_control.instance.restore_time;
        IsStop = false;
		tagr=this.GetComponent<register>().primary+add_position_control.instance.goal.transform.position;
	}
	void Update()
	{
        if (!IsStop)
        {
            Asd();
        }
        else
            Qwe();

	}

    private void Asd()          //一次复原
    {
        
        if (time < 0)
        {
            cube.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);//物体停止
            IsStop = true;
        }
        else
        {
            dis = tagr - cube.transform.position;
            p_speed = dis / time;
            cube.GetComponent<Rigidbody>().velocity = p_speed;
            time -= Time.deltaTime;

        }

    }
    private void Qwe()           //二次复原（修正误差）
    {
		add_position_control.instance.bol = false;
        this.GetComponent<Transform>().position = tagr;
        Destroy(this);
    }
}
