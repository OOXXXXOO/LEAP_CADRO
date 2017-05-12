using UnityEngine;
using System.Collections;
using Leap;
public class Test : MonoBehaviour {
    public HandController hd;
    Frame currentFrame;
    Frame LastFrame;
    public Hand lefthand;
    public Hand righthand;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
       

        currentFrame = hd.GetFrame();
        foreach (Hand hand in currentFrame.Hands)
        {//
          //  if(hand.IsLeft)
      




        }


    }
}
