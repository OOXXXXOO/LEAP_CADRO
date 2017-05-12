using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
using UITween;
/**** * 
 * Animation Initialized Object
 * 
 * new AnimationParts(AnimationParts.State.CLOSE, 
	                  false, 
	                  AnimationParts.EndTweenClose.DEACTIVATE, 
	                  AnimationParts.CallbackCall.END_OF_INTRO_ANIM, 
					  new UnityEvent(),
	                  new UnityEvent());
 * 
 * 
 ****/
[System.Serializable]
public class EasyTween : MonoBehaviour {

	public RectTransform rectTransform;
	public AnimationParts animationParts = new AnimationParts(AnimationParts.State.CLOSE,
	                                                          false,
	                                                          false,
	                                                          AnimationParts.EndTweenClose.DEACTIVATE,
	                                                          AnimationParts.CallbackCall.NOTHING,
	                                                          new UnityEvent(),
	                                                          new UnityEvent());

	private CurrentAnimation currentAnimationGoing;

	private Button ObjectButton;

	#region Public_Methods

	public EasyTween()
	{
		CheckIfCurrenAnimationGoingExits();
	}

	public void OpenCloseObjectAnimation()
	{
		rectTransform.gameObject.SetActive(true);

		ObjectButton = rectTransform.GetComponent<Button>();

		if (ObjectButton)
		{
			ObjectButton.enabled = false;
		}

		if (gameObject.activeSelf)
		{
			StartCoroutine(PlayAnimation());
		}
	}

	public bool IsObjectOpened()
	{
		return currentAnimationGoing.IsObjectOpened();
	}

	public void SetAnimatioDuration(float duration)
	{
		if (duration > 0f) 
			currentAnimationGoing.SetAniamtioDuration(duration); 
		else 
			currentAnimationGoing.SetAniamtioDuration(.01f);
	}

	public float GetAnimationDuration (){ return currentAnimationGoing.GetAnimationDuration(); }

	public void SetAnimationPosition(Vector2 StartAnchoredPos, Vector2 EndAnchoredPos, AnimationCurve EntryTween, AnimationCurve ExitTween)
	{
		currentAnimationGoing.SetAnimationPos(StartAnchoredPos, EndAnchoredPos, EntryTween, ExitTween, rectTransform);
	}

	public void SetAnimationScale(Vector3 StartAnchoredScale, Vector3 EndAnchoredScale, AnimationCurve EntryTween, AnimationCurve ExitTween)
	{
		currentAnimationGoing.SetAnimationScale(StartAnchoredScale, EndAnchoredScale, EntryTween, ExitTween);
	}

	public void SetAnimationRotation(Vector3 StartAnchoredEulerAng, Vector3 EndAnchoredEulerAng, AnimationCurve EntryTween, AnimationCurve ExitTween)
	{
		currentAnimationGoing.SetAnimationRotation(StartAnchoredEulerAng, EndAnchoredEulerAng, EntryTween, ExitTween);
	}

	public void SetFade(bool OverrideFade)
	{
		currentAnimationGoing.SetFade(OverrideFade);
	}

	public void SetFade()
	{
		currentAnimationGoing.SetFade(false);
	}

	public void SetAnimationProperties(AnimationParts animationParts)
	{
		this.animationParts = animationParts;
		currentAnimationGoing = new CurrentAnimation(animationParts);
	}

	public void ChangeSetState(bool opened)
	{
		currentAnimationGoing.SetStatus(opened);
	}

	#endregion

	#region Private_Methods

	private void Start () 
	{
		AnimationParts.OnDisableOrDestroy += CheckTriggerEndState;
		AnimationParts.OnCheckForButtons += CheckForButtons;
	}

	private void OnDestroy()
	{
		AnimationParts.OnDisableOrDestroy -= CheckTriggerEndState;
		AnimationParts.OnCheckForButtons -= CheckForButtons;
	}

	private void Update () 
	{
		currentAnimationGoing.AnimationFrame(rectTransform);
	}

	private void LateUpdate()
	{
		currentAnimationGoing.LateAnimationFrame(rectTransform);
	}

	private void TriggerOpenClose()
	{
		if (!currentAnimationGoing.IsObjectOpened())
		{
			currentAnimationGoing.PlayOpenAnimations();
		}
		else
		{			
			currentAnimationGoing.PlayCloseAnimations();
		}
	}

	private void CheckForButtons()
	{
		if (rectTransform)
		{
			if (ObjectButton)
			{
				ObjectButton.enabled = true;
			}
		}
	}

	private void CheckTriggerEndState(bool disable, AnimationParts part)
	{
		if (part != animationParts) return;

		if (disable)
		{
			rectTransform.gameObject.SetActive(false);
		}
		else
		{
			if (gameObject && !rectTransform.gameObject == gameObject)
			{
				Destroy(gameObject);
			}
			
			DestroyImmediate(rectTransform.gameObject);
		}
	}

	private void CheckIfCurrenAnimationGoingExits()
	{
		if (currentAnimationGoing == null)
		{
			SetAnimationProperties(this.animationParts);
		}
	}

	private IEnumerator PlayAnimation()
	{
		yield return new WaitForEndOfFrame();
		TriggerOpenClose();
	}

	#endregion
}