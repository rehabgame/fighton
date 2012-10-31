using UnityEngine;
using System.Collections;

public class conveyorScript : MonoBehaviour 
{
	
	//goes on the traveling object
	//"traveler" in this case
	
	private bool moveRight;
	private bool moveLeft;
	
	// Use this for initialization
	void Start () 
	{
		
		moveRight = true; //to starts some initial motion
		moveLeft = false;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if(moveRight)
		{
			transform.Translate(Vector3.left * 0.01F);
		}
		if(moveLeft)
		{
			transform.Translate(-Vector3.left * 0.01F);
		}
	
	}
	void OnTriggerEnter(Collider motion)
	{
		if(motion.name.Equals("left border"))
		{
			moveRight = true;
			moveLeft = false;
		}
		
		if(motion.name.Equals("right border"))
		{
			moveLeft= true;
			moveRight = false;
		}			
			
	}
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		
		Vector3 tempPos = Vector3.zero;
		Quaternion tempRot = Quaternion.identity;
			
			
		if(Network.isServer)
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
	
		
}


