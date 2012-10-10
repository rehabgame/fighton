using UnityEngine;
using System.Collections;

public class CubeFalling : MonoBehaviour 
{
	public Transform[] Cubes = new Transform [8];
	
	public float rate1 = 1.0f;
	public float timer1 = 0.0f;
	
	// Use this for initialization
	void Start ()
	{
		Physics.gravity = new Vector3(0, -0.5F, 0);
		//Instantiate(test, new Vector3(Random.Range(-2,2), 5,Random.Range(5,8)), new Quaternion(0,0,0,0));	
	}
	
	// Update is called once per frame
	void Update () 
	{
		int zone = Random.Range(0, 8);
		int altZone = Random.Range(0, 8);
		
		if(zone!=altZone)
		{
			zone = altZone;
		}
		
		Vector3 temp = new Vector3(0,0,0);
		
		switch(zone)
		{
		case 0:
			temp = new Vector3(Random.Range(0.1F,0.2F), 4F,Random.Range(6.2F,6.7F));
			break;
			
		case 1:
			temp = new Vector3(Random.Range(0.1F,0.4F), 4F,Random.Range(6.9F,7.40F));
			break;
			
		case 2:
			temp = new Vector3(Random.Range(0.1F,1), 4F,Random.Range(7.6F,8.25F));
			break;
		
		case 3:
			temp = new Vector3(Random.Range(0.1F,1), 4F,Random.Range(8.4F,9.0F));
			break;
		
		case 4:
			temp = new Vector3(Random.Range(-0.2F,-0.1F), 4F,Random.Range(6.2F,6.7F));
			break;
			
		case 5:
			temp = new Vector3(Random.Range(-0.4F,-0.1F), 4F,Random.Range(6.9F,7.40F));
			break;
			
		case 6:
			temp = new Vector3(Random.Range(-1,-0.1F), 4F,Random.Range(7.6F,8.25F));
			break;
		
		case 7:
			temp = new Vector3(Random.Range(-1,-0.1F), 4F,Random.Range(8.4F,9.0F));
			break;
		}		
		
		if(timer1 > rate1)
		{
						
			Instantiate(Cubes[Random.Range(0,7)], temp, new Quaternion(0,0,0,0));
			
			timer1 = 0.0f;
		}
		

		else
		{
			timer1 += Time.deltaTime;

			//Debug.Log(timer1);
		}
		
	}

}
