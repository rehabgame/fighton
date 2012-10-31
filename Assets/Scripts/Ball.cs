using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour 
{
	public static GameObject leftHandP1;
	public static GameObject rightHandP1;
	public static GameObject leftHandP2;
	public static GameObject rightHandP2;
	public GameObject traveler;
	private bool touchLeftP1;
	private bool touchRightP1;
	private bool touchLeftP2;
	private bool touchRightP2;
	
	//Physics.gravity = Vector3(0, -1.0, 0);
	void Start ()
    {
		if(!networkView.isMine)
			enabled = false;
		
		touchLeftP1 = false;
	    touchRightP1 = false;
	    touchLeftP2 = false;
	    touchRightP2 = false;
		
	}

	
	void OnTriggerEnter(Collider touch)
	{
		//Debug.Log("TRIGGER ENTER");
		//Debug.Log (touch);
		if(touch.gameObject.Equals(leftHandP1))
		{
			
			touchLeftP1 = true;
	
		}
		if(touch.gameObject.Equals(rightHandP1))
		{
			//Debug.Log ("P1 just touched you with his RAIIIIIGHT hand");
			touchRightP1 = true;
		
		}
		if(touch.gameObject.Equals(leftHandP2))
		{
			touchLeftP2 = true;
	
		}
		if(touch.gameObject.Equals(rightHandP2))
		{
			touchRightP2 = true;
		
		}
		if(touch.gameObject.name.Equals("traveler"))
		{
			//make traveler the parent
			collider.isTrigger = false;
			rigidbody.isKinematic = true;
			transform.parent = null;
			transform.position = traveler.transform.position + new Vector3(0,0.3F,0);
			transform.rotation = traveler.transform.rotation;
			transform.parent = traveler.transform;
			collider.isTrigger = true;
			//transform.position = traveler.transform.position + new Vector3(0,0.1F,0); 
			
		}
		if(touch.gameObject.name.Equals("Ground"))
		{
			rigidbody.AddForce(Vector3.up * 10);
			//rigidbody.isKinematic = true;
		}
	}
	
	void OnTriggerStay(Collider touching)
	{
		//Debug.Log ("Trigger stay");
		if(touchLeftP1 && touchRightP1)
		{
			//Debug.Log("PARENTED P1");
			//make the ball a child of left hand P1
			transform.parent = leftHandP1.transform;
			rigidbody.isKinematic = true;
			transform.position = leftHandP1.transform.position + new Vector3(0,0,0.1F); 
			transform.rotation = leftHandP1.transform.rotation;
			//Debug.Log("I caught this");
			
		}
		
		
		if(touchLeftP2 && touchRightP2)
		{
			//Debug.Log("PARENTED P2");
			//make the ball a child of left hand P2
			transform.parent = leftHandP2.transform;
			rigidbody.isKinematic = true;
			transform.position = leftHandP2.transform.position + new Vector3(0,0,0.1F); 
			transform.rotation = leftHandP2.transform.rotation;
		}
	}
	void OnTriggerExit(Collider noTouch)
	{
		//Debug.Log ("Trigger exit");
	
		if(noTouch.gameObject.Equals(rightHandP1))
		{
			touchRightP1 = false;
			transform.parent = null;
			//rigidbody.isKinematic = false;
		
		}

		if(noTouch.gameObject.Equals(rightHandP2))
		{
			touchRightP2 = false;
			transform.parent = null;
			//rigidbody.isKinematic = false;
		
		}

	}		
		
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		//locateMarkersAndLimbs();
		Vector3 tempPos = Vector3.zero;
		Quaternion tempRot = Quaternion.identity;
			
			
		if(stream.isWriting)
		{	
										
			tempPos = transform.position;
			tempRot = transform.rotation;
		 
			stream.Serialize(ref tempPos);
			stream.Serialize(ref tempRot);
					
		}
		else
		{
						
			stream.Serialize(ref  tempPos);
			stream.Serialize(ref tempRot);
			
			transform.position = tempPos;
			transform.rotation = tempRot;			
					
		}
		
	}
	
	
	/*void OnGUI()
	{				
		string times = CatchingTimes.ToString();
		GUI.Label(new Rect((Screen.width)/2,10,100,30), "Caught : "+times);
	}*/
}
