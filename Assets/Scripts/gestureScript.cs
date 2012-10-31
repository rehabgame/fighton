using UnityEngine;
using System.Collections;

public class gestureScript : MonoBehaviour 
{
	
	
	//hands on each side
	//hands touching toes
	//standby position (hands at sides)
	
	
	
	private GameObject leftHand; 
	private GameObject rightHand; 
	private GameObject leftFoot; 
	private GameObject rightFoot; 
	private GameObject leftKnee; 
	private GameObject rightKnee; 
	private GameObject head;
	private GameObject leftElbow;
	private GameObject rightElbow;
	private GameObject rightHip;
	private GameObject leftHip;
	
	public static int result;
	public static float[] coordinates= new float[6];
	
	public Texture2D armsInFront;
    public Texture2D armsToTheLeft;
	public Texture2D armsToTheRight;
    public Texture2D onLeftLeg;
	public Texture2D onRightLeg;
    public Texture2D legsSpread;
	public Texture2D armsAboveHead;
	public Texture2D belowKnees;
    public Texture2D armsSpread;
	public Texture2D standby;
	
	public bool displayGUI = false;

	

	// Use this for initialization
	void Start () 
	{
		result = 0;
		leftHand = GameObject.Find("MarkerLeftHand");
		rightHand = GameObject.Find("MarkerRightHand");
		leftFoot = GameObject.Find("MarkerLeftFoot");
		rightFoot = GameObject.Find("MarkerRightFoot");
		leftKnee = GameObject.Find("MarkerLeftKnee");
		rightKnee = GameObject.Find("MarkerRightKnee");
		head = GameObject.Find("MarkerHead");		
		rightElbow = GameObject.Find("MarkerRightElbow");
		leftElbow = GameObject.Find("MarkerLeftElbow");
		leftHip = GameObject.Find("MarkerLeftHip");
		rightHip = GameObject.Find("MarkerRightHip");
			
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		/*give coordinates of left and right hands*/
		coordinates[0]=leftHand.transform.position.x;
		coordinates[1]=leftHand.transform.position.y;
		coordinates[2]=leftHand.transform.position.z;
		coordinates[3]=rightHand.transform.position.x;
		coordinates[4]=rightHand.transform.position.y;
		coordinates[5]=rightHand.transform.position.z;
		
		if((leftHand.transform.position.y>head.transform.position.y) &&
			(rightHand.transform.position.y>head.transform.position.y))
		{
			result = 1;
			Debug.Log("Arms above the head");
		}
		
		if(((rightFoot.transform.position.x-leftFoot.transform.position.x)>0.5)||
			((rightFoot.transform.position.z-leftFoot.transform.position.z)>0.5)||
			((leftFoot.transform.position.x-rightFoot.transform.position.x)>0.5)||
			((leftFoot.transform.position.z-rightFoot.transform.position.z)>0.5))
		{
			result = 2;
			Debug.Log("Legs Stretched Apart");
		}
		
		if((rightFoot.transform.position.y-leftFoot.transform.position.y)>0.2)
		{
			result = 3;
			Debug.Log("Standing on Right Leg");
		}
		
		if((leftFoot.transform.position.y-rightFoot.transform.position.y)>0.2)
		{
			result = 4;
			Debug.Log("Standing on Left Leg");
		}
		
		if(((rightHand.transform.position.z-head.transform.position.z)>0.5)&&
			((leftHand.transform.position.z-head.transform.position.z)>0.5))
		{
			result = 5;
			Debug.Log("Arms in Front");
			
		}
		
		if((rightHand.transform.position.x>head.transform.position.x)&&
			(leftHand.transform.position.x>head.transform.position.x))
		{
			result = 6;
			Debug.Log("Arms to the Right");
			
		}
		
		if((rightHand.transform.position.x<head.transform.position.x)&&
			(leftHand.transform.position.x<head.transform.position.x))
		{
			result = 7;
			Debug.Log("Arms to the Left");
			
		}
		if(((rightElbow.transform.position.y-rightHip.transform.position.y)<0.2)&&
			((leftElbow.transform.position.y-leftHip.transform.position.y)<0.2))
		{
			result = 8;
			Debug.Log("Hands at your Sides");
		}
		if(((rightKnee.transform.position.y-rightHand.transform.position.y)>0.2)&&
			((leftKnee.transform.position.y-leftHand.transform.position.y)>0.2))
		{
			result = 9;
			Debug.Log("Hands below your knees");
		}		
		if((rightHand.transform.position.x<head.transform.position.x-0.6)&&
			(leftHand.transform.position.x>head.transform.position.x+0.6))
		{
			result = 10;
			Debug.Log("Arms Stretched Out");
		}
		
	}
	
	void OnGUI()
	{
		if(displayGUI)
		{
		
			switch(result)
			{
			case 1:
				GUI.Box(new Rect(Screen.width-500,50, 450, 410), armsAboveHead);
				break;
				
			case 2:
				GUI.Box(new Rect(Screen.width-500,50, 450, 410), legsSpread);
				break;
				
			case 3:
				GUI.Box(new Rect(Screen.width-500,50, 450, 410), onRightLeg);
				break;
				
			case 4:
				GUI.Box(new Rect(Screen.width-500,50, 450, 410), onLeftLeg);
				break;
				
			case 5:
				GUI.Box(new Rect(Screen.width-500,50, 450, 410), armsInFront);
				break;
				
			case 6:
				GUI.Box(new Rect(Screen.width-500,50, 450, 410), armsToTheRight);
				break;
				
			case 7:
				GUI.Box(new Rect(Screen.width-500,50, 450, 410), armsToTheLeft);
				break;
				
			case 8:
				GUI.Box(new Rect(Screen.width-500,50, 450, 410), standby);
				break;
				
			case 9:
				GUI.Box(new Rect(Screen.width-500,50, 450, 410), belowKnees);
				break;
				
			case 10:
				GUI.Box(new Rect(Screen.width-500,50, 450, 410), armsSpread);
				break;
						
										
			default:
				break;
					
			}
		}
	}
}
		