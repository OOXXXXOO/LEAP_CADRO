using UnityEngine;
using System.Collections;

public class EnhanceItem : MonoBehaviour
{

    // 在ScrollViewitem中的索引
    internal int scrollViewItemIndex = 0;

    // 夹角大小
    internal float angla = 0f;
    // 动画时间值
    internal float dValueTime = 0f;

    // 前后项
    internal EnhanceItem front, back;



	/*
	 * 
	 * 
	 * 
	 * 
	 *   internal关键字是类型和类型成员的访问修饰符。只有在同一个程序集的文件中，内部类型或者是成员才可以访问。
	 * 这是msdn上对internal的描述。
	 * 类型就是enum（枚举类型），class（类），interface（接口），struct（结构）等类型。
	 * 类型成员如函数，成员变量等。
	 * 
	 * 一个完整的.exe或者是.dll文件就是一个程序集，一般伴随着exe程序集产生的还有一个程序集清单
	 * ，.exe.config文件。下面我就用一个例子来说明“internal关键字是类型和类型成员的访问修饰符。
	 * 只有在同一个程序集的文件中，内部类型或者是成员才可以访问”。
	 * 

	 * 
	 */



	public int flag = 777;


    private Vector3 targetPos = Vector3.one;
    private Vector3 targetScale = Vector3.one;

    private Transform mTrs;
    private UITexture mTexture;

    void Awake()
    {
        mTrs = this.transform;
        mTexture = this.GetComponent<UITexture>();
    }

    void Start()
    {
        UIEventListener.Get(this.gameObject).onClick = OnClickScrollViewItem;
    }

    // 当点击Item，将该item移动到中间位置
    private void OnClickScrollViewItem(GameObject obj)
    {
        EnhancelScrollView.GetInstance().SetHorizontalTargetItemIndex(scrollViewItemIndex);
    }

    /// <summary>
    /// 更新该Item的缩放和位移
    /// </summary>
    public void UpdateScrollViewItems(float xValue, float yValue, float scaleValue)
    {
        targetPos.x = xValue;
        targetPos.y = yValue;
        targetScale.x = targetScale.y = scaleValue;

        mTrs.localPosition = targetPos;
        mTrs.localScale = targetScale;
    }

    public void SetSelectColor(bool isCenter)
    {
        if (mTexture == null)
            mTexture = this.GetComponent<UITexture>();

        if (isCenter)
            mTexture.color = Color.white;
        else
            mTexture.color = Color.gray;
    }
}
