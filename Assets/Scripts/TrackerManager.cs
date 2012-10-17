/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackerManager : MonoBehaviour {
	
	public List<KinectSkeleton> trackers = new List<KinectSkeleton>();	//All trackers for person 0
		
	public KinectSkeleton skeleton0;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
			//Just set the markers (position and rotation)
			for(int i=0; i<24; i++)
			{
				if(trackers[0].markers[i] == null)
				{
					continue;
				}
				Vector3 v = Vector3.zero;
				Quaternion q = Quaternion.identity;
				v = trackers[0].markers[i].position;
				q = trackers[0].markers[i].rotation;
			}			

	}
}
 */