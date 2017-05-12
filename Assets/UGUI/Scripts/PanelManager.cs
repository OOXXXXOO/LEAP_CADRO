using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class PanelManager : MonoBehaviour {

	public Animator initiallyOpen;//在现场开始自动打开屏幕

		private int m_OpenParameterId;//我们使用散列的参数来控制转换
	private Animator m_Open;//当前打开屏幕
		private GameObject m_PreviouslySelected;//选择的对象之前，我们打开当前屏幕,当关闭一个屏幕时，我们可以回到按钮打开它

	const string k_OpenTransitionName = "Open";
	const string k_ClosedStateName = "Closed";
	public void OnEnable()
	{
		m_OpenParameterId = Animator.StringToHash (k_OpenTransitionName);//当前动画的hash值

			if (initiallyOpen == null)//如果设置，打开初始屏幕
			return;

		OpenPanel(initiallyOpen);
	}
	public void OpenPanel (Animator anim)//关闭当前打开的面板，打开所提供的面板，然后在这个面板里面选择
	{
		if (m_Open == anim)
			return;

		anim.gameObject.SetActive(true);//让对象一直保持激活状态，SetActive(false)改为将对象移到屏幕外，SetActive(true)改为将对象移回屏幕内，激活新的屏幕层次结构
			var newPreviouslySelected = EventSystem.current.currentSelectedGameObject;//保存当前选定的按钮以打开该屏幕，当前选中的对象，你可以通过这个值判断当前是否鼠标点击在对象上，因为也许你有拖动摄像机的功能，但是你又不喜欢点击某些对象的时候这个功能又被响应，所以通过这个变量判断是一个很好的办法

			//anim.transform.SetAsLastSibling();//把屏幕移到前面
		   
		CloseCurrent();

		m_PreviouslySelected = newPreviouslySelected;

		m_Open = anim;//设置新的屏幕，然后打开一个
			m_Open.SetBool(m_OpenParameterId, true);//打开动画 
			//GameObject go = FindFirstEnabledSelectable(anim.gameObject);//在新的屏幕上设置一个元素作为新的选择一个

		//SetSelected(go);
		Update();
	}
	void Update()
	{
		m_Open.SetBool("ScrollView", true);
	}
	/*void Update(){
		if(Input.GetMouseButtonDown(0)){
			//StartCoroutine(SwitchToPresentationScreen());
			if(Application.loadedLevelName=="Lighting")
			{
				
				Application.LoadLevel("Menu 3D");
			}
			else if(Application.loadedLevelName=="Menu 3D")
			{
				Application.LoadLevel("Lighting");
			}
			
		}
	}*/
	/*
	static GameObject FindFirstEnabledSelectable (GameObject gameObject)//发现providade层次结构中的第一个可选的元素
	{
		GameObject go = null;
		var selectables = gameObject.GetComponentsInChildren<Selectable> (true);
		foreach (var selectable in selectables) {
			if (selectable.IsActive () && selectable.IsInteractable ()) {
				go = selectable.gameObject;
				break;
			}
		}
		return go;
	}*/
	//关闭当前打开的屏幕,重新选择选择使用当前屏幕前开放

	public void CloseCurrent()
	{
		if (m_Open == null)
			return;

		m_Open.SetBool(m_OpenParameterId, false);//开始关闭动画
			SetSelected(m_PreviouslySelected);//回复选择选择使用当前屏幕前开放
			StartCoroutine(DisablePanelDeleyed(m_Open));//启动进程,禁用层次关闭动画结束时
		m_Open = null;
	}
	//将检测到的结束动画完成，它将关闭层次结构
	IEnumerator DisablePanelDeleyed(Animator anim)
	{
		bool closedStateReached = false;
		bool wantToClose = true;
		while (!closedStateReached && wantToClose)
		{
			if (!anim.IsInTransition(0))
				closedStateReached = anim.GetCurrentAnimatorStateInfo(0).IsName(k_ClosedStateName);

			wantToClose = !anim.GetBool(m_OpenParameterId);

			yield return new WaitForEndOfFrame();
		}

		if (wantToClose)
			anim.gameObject.SetActive(false);
	}

	//让提供的对象被选择，然后用鼠标来触摸，我们要把它设置成为之前被选择的状态和当前没被选择的状态
	private void SetSelected(GameObject go)
	{
		EventSystem.current.SetSelectedGameObject(go);//选择对象

			var standaloneInputModule = EventSystem.current.currentInputModule as StandaloneInputModule;//使用键盘的事件
		if (standaloneInputModule != null && standaloneInputModule.inputMode == StandaloneInputModule.InputMode.Buttons)
			return;

		EventSystem.current.SetSelectedGameObject(null);//用户切换到键盘，我们想从所提供的游戏对象中开始导航。
			//所以我们设置当前选定为null，所以提供的游戏对象成为最后选定的事件系统
	}
}
