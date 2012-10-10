using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour 
{
	void Start ()
    {
		if(!networkView.isMine)
			enabled = false;
	}
	
	void OnCollisionEnter(Collision collision)
	{
				
		if(collision.contacts[0].otherCollider.gameObject.tag.Equals("Plane"))
		{
			
			rigidbody.AddForce(0,150,0); 
			//Debug.Log("Collided with plaNE");
		}
		if(collision.contacts[0].otherCollider.gameObject.tag.Equals("Left Hand"))
		{
			rigidbody.AddForce(Vector3.right * 200); 
			Debug.Log("Collided with left hand");
			
		}
		if(collision.contacts[0].otherCollider.gameObject.tag.Equals("Right Hand"))
		{
			rigidbody.AddForce(-Vector3.right * 200); 
			Debug.Log("Collided with right hand");
			
		}
		if(collision.contacts[0].otherCollider.gameObject.tag.Equals("Foot"))
		{			
			rigidbody.AddForce(Vector3.forward * 200); 
			//Debug.Log("Collided with foot");
			//yield return new WaitForSeconds(3);
			
		}
		if(collision.contacts[0].otherCollider.gameObject.tag.Equals("Player"))
		{
			//yield return new WaitForSeconds(2);
			rigidbody.AddForce(0,10,0); 
			Debug.Log("Collided with player");
		}
		if(collision.contacts[0].otherCollider.gameObject.tag.Equals("Front Wall"))
		{
			
			rigidbody.AddForce(0,0,-150F); 
			//Debug.Log("Collided with plaNE");
		}
		if(collision.contacts[0].otherCollider.gameObject.tag.Equals("Back Wall"))
		{
			
			rigidbody.AddForce(0,0,400F); 
			//Debug.Log("Collided with plaNE");
		}
		if(collision.contacts[0].otherCollider.gameObject.tag.Equals("Left Wall"))
		{
			
			rigidbody.AddForce(150F,0,0); 
			//Debug.Log("Collided with plaNE");
		}
		if(collision.contacts[0].otherCollider.gameObject.tag.Equals("Right Wall"))
		{
			
			rigidbody.AddForce(-150F,0,0); 
			//Debug.Log("Collided with plaNE");
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
