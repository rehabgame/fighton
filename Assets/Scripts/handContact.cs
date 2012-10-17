using UnityEngine;
using System.Collections;

public class handContact : MonoBehaviour 
{	
	public static bool isContact;
	public GameObject contact;
	// Use this for initialization
	void Start () 
	{
			
		
		isContact = false;
	
	}
		
	void OnCollisionEnter(Collision boom)
	{		
		//Debug.Log("Right/Left collision happened");
		contact = boom.contacts[0].otherCollider.gameObject;
		if(contact.tag.Equals("Right Arms"))
		{
			isContact = true;
			Debug.Log ("Hands Touched");
			
		}
	}
	
}
