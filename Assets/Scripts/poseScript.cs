using UnityEngine;
using System.Collections;

public class poseScript : MonoBehaviour 
{
	//positions:
	//1. Arms above head
	//2. Legs apart
	//3. Standing onleft leg
	//4. Standing on right leg
	//5. Arms in front
	//6. Arms to the right
	//7. Arms to the left
	//8. Standby
	//9. Hands below Knees
	//10. Arms Stretched Out
	//11. Right Arm Stretched out
	//12. Left Arm stretched out
	//13. Left Arm in Front
	//14. Right Arm in Front
	//15. Left Arm Above Head
	//16. Right arm above head
	// Use this for initialization
	
	private float startTime;
	private int prevResult;
	private int up;
	private int side;
	
	//check prevResult every update
	//then check if(prevResult==whatever) and (result==whatever), then increment the respective counter
	//make bools for left and right side
	
	void Start () 
	{
		startTime = 0.0F;
		up = 0;
		side = 0; 
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if(gestureScript.result == 8)
		{
			startTime = 0.0F;
					
		}
		startTime += Time.deltaTime;
	
		//gesture checking stuff happens here
		if((gestureScript.result==11)&&(prevResult == 8))
		
		if((gestureScript.result==12)&&(prevResult == 8))
				
		if((gestureScript.result==13)&&(prevResult == 8))
					
		if((gestureScript.result==14)&&(prevResult == 8))
		
		if((gestureScript.result==15)&&(prevResult == 8))
				
		if((gestureScript.result==16)&&(prevResult == 8))
		
		
		//ends here
		if((up == 2)||(side==2)||(startTime>60.0F))
		{
			startTime = 0.0F;
		}
		prevResult = gestureScript.result;
		
	}
}
