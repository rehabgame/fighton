using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour 
{
	public GameObject placeholder;
	public GameObject leftHand;
	public GameObject rightHand;
	//Physics.gravity = Vector3(0, -1.0, 0);
	void Start ()
    {
		if(!networkView.isMine)
			enabled = false;
		
		leftHand = GameObject.Find("Markers/MarkerLeftHand");
		rightHand = GameObject.Find("Markers/MarkerRightHand");
		
	}
	
	void OnCollisionEnter(Collision collision)
	{
		
		placeholder = collision.contacts[0].otherCollider.gameObject;
		Debug.Log(placeholder.transform.position);
				
		if(placeholder.tag.Equals("Plane"))
		{
			
			rigidbody.AddForce(0,150,0); 
			//Debug.Log("Collided with plaNE");
		}
		if(placeholder.tag.Equals("Left Hand"))
		{
			
			//rigidbody.AddForce(Vector3.right * 200); 
			Debug.Log("Collided with left hand");
			transform.parent = leftHand.transform;
			rigidbody.isKinematic = true;
			transform.position = leftHand.transform.position + new Vector3(0,0,0.1F); 
			transform.rotation = leftHand.transform.rotation;
			
		}
		if(placeholder.tag.Equals("Right Hand"))
		{
			//rigidbody.AddForce(-Vector3.right * 200); 
			Debug.Log("Collided with right hand");
			transform.parent = rightHand.transform;
			rigidbody.isKinematic = true;
			transform.position = rightHand.transform.position + new Vector3(0,0,0.1F); 
			transform.rotation = rightHand.transform.rotation;
			
		}
		if(placeholder.tag.Equals("Foot"))
		{			
			Debug.Log("Collided with FOOT");
			transform.parent = rightHand.transform;
			rigidbody.isKinematic = true;
			transform.position = rightHand.transform.position + new Vector3(0,0,0.1F); 
			transform.rotation = rightHand.transform.rotation;
			
		}
		/*if(collision.contacts[0].otherCollider.gameObject.tag.Equals("Player"))
		{
			//yield return new WaitForSeconds(2);
			//rigidbody.AddForce(0,10,0); 
			Debug.Log("Collided with player");
		}*/
		if(collision.contacts[0].otherCollider.gameObject.tag.Equals("Front Wall"))
		{
			
			rigidbody.AddForce(0,0,-400F); 
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
		
	void Update()
	{
		if(handContact.isContact == true)
		{
						
			if(transform.parent == leftHand.transform)
			{
				rigidbody.isKinematic = false;
				transform.parent = rightHand.transform;
				transform.position = rightHand.transform.position + new Vector3(0,0,0.1F); 
				transform.rotation = rightHand.transform.rotation;
				handContact.isContact = false;
				rigidbody.isKinematic = true;
			}
			else if(transform.parent == rightHand.transform)
			{
				rigidbody.isKinematic = false;
				transform.parent = leftHand.transform;
				transform.position = leftHand.transform.position + new Vector3(0,0,0.1F); 
				transform.rotation = leftHand.transform.rotation;
				handContact.isContact = false;
				rigidbody.isKinematic = true;
			}			
			
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
