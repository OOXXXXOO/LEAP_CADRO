using UnityEngine;
using System.Collections;
using Leap;
public class ray : MonoBehaviour {
	public hand_script2 control_script;//控制脚本定义
	public HandController hc=null;
	public RaycastHit hit; 
	public Vector3 handdir;
	public Vector3 handpos;
	//public GameObject cube = null;
	public GameObject highlight=null;

	float hx, hy, hz=0;
	float dx, dy, dz = 0;
	public GameObject particle_light=null;
	float xr,yr,zr;
	private float index=0;
	Hand lefthand=null;
	Hand righthand=null;


	public GameObject color_apply=null;

	Color origin=Color.black;
	public Color apply_color;


//	______________________________________________________________
	bool squize(float radius)//判断手势是否为握持  阀值为35
	{
		if (radius < 37)
			return true;
		else
			return false;
	}
//	---------------------------------------------------------------------------
	private bool IsHand(Collider collider)
	{
		return collider.transform.parent 
            && collider.transform.parent.parent 
            && collider.transform.parent.parent.GetComponent<HandModel>();
	}


	void Awake()
	{
		
	}


	// Use this for initialization
	void Start () {
		Debug.Log ("Start");
		//空间两点距离计算：sqrt(pow(x1-x2，2)+pow(y1-y2，2)+pow(z1-z2，2))

		index = hc.transform.localScale.x / 1000;








	}

	// Update is called once per frame
	void Update () {
		bool lefthandexit = false;
		bool righthandexit = false;
		//Debug.Log ("Update");
		Vector3 fwd = new Vector3 (0, 0,10 );
		Frame frame = hc.GetFrame ();
		Frame lastframe = hc.getlastframe ();
	    apply_color = color_apply.gameObject.GetComponent<RGB> ().rgb;


		foreach (Hand hand in frame.Hands) {
			if (hand.IsLeft) {
				lefthandexit = true;
				lefthand = hand;
			}

			if (hand.IsRight) {
				righthandexit = true;
				righthand = hand;


				foreach (Finger finger in righthand.Fingers) {
					Finger.FingerType type=finger.Type();
					if (type == Finger.FingerType.TYPE_INDEX) {
						dx = finger.Direction.x;
						dy = finger.Direction.y;
						dz = finger.Direction.z;
						hx = finger.TipPosition.x;
						hy = finger.TipPosition.y;
						hz = finger.TipPosition.z;
					}

				}
				//dx = hand.Direction.x;
				//dy = hand.Direction.y;
				//dz = hand.Direction.z;
				handdir = new Vector3 (dx*index,dy*index,-dz*index);

				//				----------------------------------------------------------------------


				//hx = hand.PalmPosition.x;
				//hy = hand.PalmPosition.y;
				//hz = hand.PalmPosition.z;
				handpos = new Vector3 (hx*index, hy*index,-hz*index);

				//				----------------------------------------------------------
				//Debug.Log ("righthand_position is " + handpos + "righthand dir is " + handdir);
				//Debug.Log ("reall date is" + hand.PalmPosition + " /// " + hand.Direction);
				xr = hand.RotationAngle (lastframe, Vector.XAxis);
				yr = hand.RotationAngle (lastframe, Vector.YAxis);
				zr = hand.RotationAngle (lastframe, Vector.ZAxis);

			}
		}
		//cube.transform.Rotate (new Vector3(xr*index,yr*index,-zr*index),Space.World);


		/*
		float x = this.transform.rotation.x;
		float y = this.transform.rotation.y;
		float z = this.transform.rotation.z;
		Vector3 rota = new Vector3 (x, y, z);
		Debug.Log ("this.position=" + this.transform.position);	
		dx = this.transform.position.x;
		dy = this.transform.position.y;
		dz = this.transform.position.z;
		Vector3 position = new Vector3 (dx,dy+100,dz);

   */







		if (squize(lefthand.SphereRadius) && (!squize (righthand.SphereRadius))) {
		//bool Physics.Raycast(Ray ray, Vector3 direction, RaycastHit out hit, float distance, int layerMask)
		if (Physics.Raycast(handpos,handdir,out hit,10000,1)) {

			particle_light.transform.position =new Vector3(hit.point.x,hit.point.y,hit.point.z-2);


				if ((highlight.gameObject.name.ToString() != hit.collider.gameObject.name.ToString()) &&
					origin == Color.black&&
					(!IsHand(hit.collider))&&
					(hit.collider.gameObject.GetComponent<Renderer>()!=null)) {


				Debug.Log ("first");
				highlight = hit.collider.gameObject;
				control_script.main_son = hit.collider.gameObject;

				origin = highlight.gameObject.GetComponent<Renderer> ().material.color;
				highlight.gameObject.GetComponent<Renderer> ().material.color = Color.gray;


				}

				if ((highlight.gameObject.name.ToString () != hit.collider.gameObject.name.ToString ()) &&
					origin != Color.black&&
					(!IsHand(hit.collider))&&
					((hit.collider.gameObject.GetComponent<Renderer>()!=null)))	{
				Debug.Log ("change");
				control_script.main_son = hit.collider.gameObject;

				highlight.gameObject.GetComponent<Renderer> ().material.color = origin;
				highlight = hit.collider.gameObject;
				origin = highlight.gameObject.GetComponent<Renderer> ().material.color;
				highlight.gameObject.GetComponent<Renderer> ().material.color = Color.gray;
			}
			/*
			if((highlight.gameObject.name.ToString()==hit.collider.gameObject.name.ToString())&&origin!=Color.black)
			{
				highlight = hit.collider.gameObject;
				highlight.gameObject.GetComponent<Renderer> ().material.color = Color.red;
			}
			*/


			//hit.collider.gameObject.GetComponent<Renderer> ().material.color = Color.red;
			Debug.Log ("OBJ-name is " + hit.collider.gameObject);
			//Debug.Log ("hit point is " + hit.point + " distance = " + hit.distance);
			//Debug.Log ("Success");
			Debug.DrawLine (handpos, hit.point, Color.red);
				Debug.Break ();

		}






















		if (lefthandexit==true) {
			Debug.Log ("________________");
			//Debug.Break();
		}

	}
	}
}

	//射线脚本bug汇总：
	/*
	 * 1.发射点以世界坐标为准则：
	 * 如handcontroller的放大倍率为100则数乘index指数为0.1
	 * 2.必须将控制器的位置考虑进去
	 * 3.射线距离尽可能大
	 * 
	 * 
	 * 
	 */


