using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;

// [ExecuteInEditMode]
public class EnhancelScrollView : MonoBehaviour
{
    // 含有滑动项目的面板
    public GameObject enhanceScrollView;

    // 缩放曲线
    public AnimationCurve scaleCurve;
    // 位移曲线
    public AnimationCurve positionCurve;
    // 动画时间
    public float duration = 0.2f;
    
    // 宽度
    public float width = 800.0f;
    // y轴坐标固定值(所有的item的y坐标一致)
    public float yPositionValue = 46.0f;

    // 中旬显示目标时间线(0显示第一个，0.5显示中间一个)
    public float horizontalTargetValue = 0.0f;

    // 滑动起始力
    public float touchStartPower = 0.5f;
    // 滑动阻力
    public int touchForce = 120;

    // 目标对象列表
    private List<EnhanceItem> scrollViewItems;
    // 目标对象Widget脚本，用于depth排序
    private List<UITexture> textureTargets;

    // 开始X坐标
    private float startXPos = 0f;




    // 当前处于中间的item
	public EnhanceItem centerItem;







    // 当前出移动中，不能进行点击切换
    private bool isInChange = false;   

    // 位置动画的中间位置时间
    private float positionCenterTime = 0.5f;
    // 当前精度小数位
    private int curACC = 4;

    // 横向变量值
    private float horizontalValue = 0.0f;

    // 移动动画参数
    private float originHorizontalValue = 0.0f;
    private float currentDuration = 0.0f;

    private static EnhancelScrollView instance;
    internal static EnhancelScrollView GetInstance()
    {
        return instance;
    }



	//__________________________________________________________
//	动作识别部分定义
	public HandController hc;


	Hand lefthand=null;
	Hand righthand = null;
	Hand last_lefthand = null;
	Hand last_righthand = null;
	Frame currentFrame = null;//定义当前帧


	bool lefthandexist = false;//判断左右手是否在场景中存在
	bool righthandexist = false;

	float sweptAngle = 0;//初始化角度为零

	int mark=0;//标记  与下面的函数作用
//	0代表不转动，-1代表向左转，1代表向右转
	//____________________________________________________________

	void checkmark(int sign)
	{
		if (sign == 1) {
			SetHorizontalTargetItemIndex(centerItem.front.scrollViewItemIndex);
		}
		if (sign == -1) {
			SetHorizontalTargetItemIndex(centerItem.back.scrollViewItemIndex);
		}
		mark = 0;
	}


    void Awake()
    {
        instance = this;
    }

    void Start()
    {

		hc.GetLeapController().EnableGesture(Gesture.GestureType.TYPECIRCLE);
		hc.GetLeapController().EnableGesture(Gesture.GestureType.TYPESWIPE);
		hc.GetLeapController().EnableGesture(Gesture.GestureType.TYPE_SCREEN_TAP);
		hc.GetLeapController ().EnableGesture (Gesture.GestureType.TYPEKEYTAP);
		hc.GetLeapController ().EnableGesture (Gesture.GestureType.TYPEINVALID);
		


//		____________________________________________













        InitData();

        // 设置第一个为选中状态
        SetHorizontalTargetItemIndex(0);
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    private void InitData()
    {
        startXPos = -(width / 2);

        scrollViewItems = new List<EnhanceItem>();
        scrollViewItems.AddRange(enhanceScrollView.GetComponentsInChildren<EnhanceItem>());

        if (textureTargets == null)
            textureTargets = new List<UITexture>();

        float anglaDValue = 360 / scrollViewItems.Count;

        int centerIndex = scrollViewItems.Count / 2;
        for (int i = 0; i < scrollViewItems.Count; i++)
        {
            scrollViewItems[i].scrollViewItemIndex = i;
            scrollViewItems[i].angla = anglaDValue * i;
            scrollViewItems[i].dValueTime = GetCurveTimePos(scrollViewItems[i].angla);

            // 构造环形链
            scrollViewItems[i].front = i == 0 ? scrollViewItems[scrollViewItems.Count - 1] : scrollViewItems[i - 1];
            scrollViewItems[i].back = i == (scrollViewItems.Count - 1) ? scrollViewItems[0] : scrollViewItems[i + 1];

            UITexture tmpTexture = scrollViewItems[i].gameObject.GetComponent<UITexture>();
            textureTargets.Add(tmpTexture);

            scrollViewItems[i].SetSelectColor(false);
        }
    }
	//_____________________________________________________________________________________________
    void Update()
    {

//		动作识别部分代码


	

























//		——————————————————————————————————————————————————————————————————————

        if (!isInChange)
        {
            touch();
            return;
        }

        currentDuration += Time.deltaTime;
        float percent = currentDuration / duration;
        horizontalValue = Mathf.Lerp(originHorizontalValue, horizontalTargetValue, percent);
        UpdateEnhanceScrollView(horizontalValue);

        SortDepth();

        if (currentDuration > duration)
        {
            centerItem = textureTargets[textureTargets.Count - 1].gameObject.GetComponent<EnhanceItem>();
            centerItem.SetSelectColor(true);
            isInChange = false;
        }
    }

    /// <summary>
    /// 更新水平滚动
    /// </summary>
    /// <param name="fValue"></param>
    private void UpdateEnhanceScrollView(float fValue)
    {
        for (int i = 0; i < scrollViewItems.Count; i++)
        {
            EnhanceItem itemScript = scrollViewItems[i];
            float xValue = GetXPosValue(fValue, itemScript.dValueTime);
            float scaleValue = GetScaleValue(fValue, itemScript.dValueTime);

            itemScript.UpdateScrollViewItems(xValue, yPositionValue, scaleValue);
        }
    }

    //滑动X轴增量位置
    float xMoved;
    private void touch()
    {
        // 记录滑动位置
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            //获取手指自最后一帧的移动
            float x = Input.GetTouch(0).deltaPosition.x;
            xMoved = x;
        }

        // 滑动结束时判断故事翻页
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            if (centerItem == null || Mathf.Abs(xMoved) < touchStartPower)
                return;

            int count = (int)(Mathf.Abs(xMoved * scrollViewItems.Count/ touchForce)) + 1;   
            int minHalfCount = Mathf.CeilToInt((float)scrollViewItems.Count / 2) - 1;
            if (count > minHalfCount)
            {
                count = minHalfCount;
            }

            if(xMoved > 0)
            {
                SetHorizontalTargetItemIndex(GetMoveIndex(centerItem, -count));
            }
            else if (xMoved < 0)
            {
                SetHorizontalTargetItemIndex(GetMoveIndex(centerItem, count));
            }
            xMoved = 0;
        }
    }

    /// <summary>
    /// 缩放曲线模拟当前缩放值
    /// </summary>
    private float GetScaleValue(float sliderValue, float added)
    {
        float scaleValue = scaleCurve.Evaluate(positionCenterTime + sliderValue + added);
        return scaleValue;
    }

    /// <summary>
    /// 位置曲线模拟当前x轴位置
    /// </summary>
    private float GetXPosValue(float sliderValue, float added)
    {
        float evaluateValue = startXPos + positionCurve.Evaluate(positionCenterTime + sliderValue + added) * width;
        return evaluateValue;
    }

    /// <summary>
    /// 计算位置动画中的时间点
    /// </summary>
    /// <param name="anga">角度值,360度=1</param>
    /// <returns></returns>
    private float GetCurveTimePos(float anga)
    {
        // 设定0.5为位置中间
        return Round(anga / 360f, curACC);
    }

    // 获取项目A到项目B之间最小的时间差值（圆形角度计算,1=360度）
    private float GetCurveTimeDValue(EnhanceItem itemA, EnhanceItem itemB)
    {
        return Round((Mathf.DeltaAngle(itemA.angla, itemB.angla)) / 360f, curACC);
    }

    private void SortDepth()
    {
        textureTargets.Sort(new CompareDepthMethod());
        for (int i = 0; i < textureTargets.Count; i++)
            textureTargets[i].depth = i;
    }

    /// <summary>
    /// 用于层级对比接口
    /// </summary>
    private class CompareDepthMethod : IComparer<UITexture>
    {
        public int Compare(UITexture left, UITexture right)
        {
            if (left.transform.localScale.x > right.transform.localScale.x)
                return 1;
            else if (left.transform.localScale.x < right.transform.localScale.x)
                return -1;
            else
                return 0;
        }
    }








	//核心滚动函数




    /// <summary>
    /// 设置横向轴参数，根据缩放曲线和位移曲线更新缩放和位置
    /// </summary>

    internal void SetHorizontalTargetItemIndex(int itemIndex)
    {
        if (isInChange)
            return;

        EnhanceItem item = scrollViewItems[itemIndex];//___________________________________________________________新场景中根据这个来判断
        if (centerItem == item)
            return;
		Debug.Log ("item = " + item.name);
		if (item.name == "Texture01") {
			Debug.Log("YES");
		}
//		_________________________________________________________________________________________




        float dvalue = centerItem == null ? 0 : GetCurveTimeDValue(centerItem, item);
        // 更改target数值，平滑移动,设负数倒着转
        horizontalTargetValue += -dvalue;
        beginScroll(horizontalValue, horizontalTargetValue);
    }




		





















	void FixedUpdate()
	{
//		___________________________________________________________________________________手势swipe模块
		this.currentFrame = hc.GetFrame ();
		Frame frame = hc.GetFrame ();
		Frame lastframe = hc.getlastframe ();
		GestureList gestures = this.currentFrame.Gestures ();
		Vector swipedirection=null;
		
		foreach (Gesture g in gestures) {
			

			if(g.Type==Gesture.GestureType.TYPE_SWIPE)
			{
				SwipeGesture swipe=new SwipeGesture(g);
				swipedirection=swipe.Direction;
				//Debug.Log("direction is "+swipedirection);
				
			}
			
			
			
			
			
			
		}
		if (swipedirection.x > 0) {
			Debug.Log("right");		
			mark=1;
		}
		if (swipedirection.x < 0) {
			Debug.Log("left");
			mark=-1;
		}


		checkmark (mark);

//		————————————————————————————————————————————————————————————————————————————————————————————————————————————
	}

		















    /// <summary>
    /// 开始滚动
    /// </summary>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    private void beginScroll(float startTime, float endTime)
    {
        if (isInChange)
            return;

        foreach (EnhanceItem item in scrollViewItems)
        {
            item.SetSelectColor(false);
        }

        originHorizontalValue = Round(startTime, curACC);
        horizontalTargetValue = Round(endTime, curACC);
        currentDuration = 0.0f;

        isInChange = true;
    }











    /// <summary>
    /// 向右选择角色按钮
    /// </summary>
    public void OnBtnRightClick()
    {
        if (isInChange)
            return;
        SetHorizontalTargetItemIndex(centerItem.back.scrollViewItemIndex);

    }

    /// <summary>
    /// 向左选择按钮
    /// </summary>
    public void OnBtnLeftClick()
    {
        if (isInChange)
            return;
        SetHorizontalTargetItemIndex(centerItem.front.scrollViewItemIndex);
    }










//	__________________________________________________________________





    /// <summary>
    /// 获取移动后的项目索引
    /// </summary>
    /// <param name="item">当前项目</param>
    /// <param name="count">移动位数，负数表示倒移</param>
    /// <returns></returns>
    private int GetMoveIndex(EnhanceItem item, int count)
    {
        EnhanceItem curItem = item;
        for (int i = 0; i < Mathf.Abs(count); i++)
        {
            curItem = count > 0 ? curItem.back : curItem.front;

        }

        return curItem.scrollViewItemIndex;
    }

    /// <summary>
    /// 按指定小数位舍入
    /// </summary>
    /// <param name="f"></param>
    /// <param name="acc"></param>
    /// <returns></returns>
    private float Round(float f, int acc)
    {
        float temp = f * Mathf.Pow(10, acc);
        return Mathf.Round(temp) / Mathf.Pow(10, acc);
    }

    /// <summary>
    /// 截取小数
    /// </summary>
    /// <param name="f"></param>
    /// <returns></returns>
    private float CutDecimal(float f) {
        return f - (int)f;
    }
}

