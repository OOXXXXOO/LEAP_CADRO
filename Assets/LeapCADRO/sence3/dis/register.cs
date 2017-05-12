using UnityEngine;
using System.Collections;

public class register : MonoBehaviour {
    /// <summary>
    /// 寄存器  
    /// </summary>

public Vector3 primary;//寄存的原始状态关于父物体的相对位置
public int Way_of_dis;
void Awake()
{
    this.Way_of_dis = 0;
}
	public void refresh_primary()//旋转后重置目标位置
	{
		this.primary = this.GetComponent<Transform> ().position - add_position_control.instance.goal.transform.position;
	}
}
