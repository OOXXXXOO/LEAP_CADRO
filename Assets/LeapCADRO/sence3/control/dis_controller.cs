using UnityEngine;
using System.Collections;
using Leap;


public class dis_controller : MonoBehaviour {

	public HandController hc;
	Hand lefthand,righthand=null;
	Hand pre_lefthand,pre_righthand=null;
	//disperse control script

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Frame frame = hc.GetFrame ();
		Frame _Xframe = hc.get_X_frame (30);
		foreach (Hand hand in frame.Hands) {//当前帧的手
			if (hand.IsLeft) {
				lefthand = hand;
			}
			if (hand.IsRight) {
			
				righthand = hand;
			}
		}
		foreach (Hand lasthand in _Xframe.Hands) {
		
			if (lasthand.IsLeft) {
				pre_lefthand = lasthand;
			}
			if (lasthand.IsRight) {
				pre_righthand = lasthand;	
			}
		
		}
		Debug.Log ("hand.pitch " + righthand.Direction.Pitch
		+ " hand.yaw " + righthand.Direction.Yaw +
		" hand.roll " + righthand.Direction.Roll);
		Debug.Log ("hand.x " + righthand.Direction.x
			+ " hand.y " + righthand.Direction.y +
			" hand.z " + righthand.Direction.z);




	}
}
