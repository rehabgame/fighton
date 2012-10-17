using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class KinectSkeleton : MonoBehaviour
{
    public string tracker = "Tracker0";
    public string server = "localhost";

    IntPtr[] trackerDataPointer = new IntPtr[24];
    TrackerData[] trackerData = new TrackerData[24];

    public Transform[] markers = new Transform[24];
    public Transform[] limbs = new Transform[19];
    
	public float[] jointConfidence = new float[24];
	
	public Transform[] jointSend = new Transform[24];
    bool initialized = false;
	
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TrackerData
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public double[] position;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public double[] rotation;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public double[] positionSum;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public double[] rotationSum;

        public int numReports;
    }

    [DllImport("VRPNClient.dll")]
    static extern IntPtr initializeTracker(string serverName, int sensorNumber);

    [DllImport("VRPNClient.dll")]
    static extern void updateTrackers();

	// Use this for initialization
	void Start ()
    {
		locateMarkersAndLimbs();
		
		if(!networkView.isMine)
			enabled = false;
		
        for (int i = 0; i < 24; i++)
        {
			//jointSend[i] = markers[i];
            trackerDataPointer[i] = initializeTracker(tracker + "@" + server, i);
			
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
     	// update all trackers
        updateTrackers();

        if (!initialized)
        {
            for (int i = 0; i < markers.Length; i++)
                markers[i] = null;
			
			initialized = true;
			
			locateMarkersAndLimbs();

            
            
        }

		if (networkView.isMine)
		{
		        // update marker locations
	        for (int i = 0; i < markers.Length; i++)
	        {
	            if(markers[i] != null)
				{
					getMostRecentReport(markers[i], i);
					jointSend[i].position = markers[i].position;
					jointSend[i].rotation = markers[i].rotation;
				}
	        }
		}
			
			//Debug limb calculations
			SetLimbsFromMarkers();
		
		
	}
	
	void locateMarkersAndLimbs()
	{
			markers[3] = transform.FindChild("Markers/MarkerWaist");
            markers[2] = transform.FindChild("Markers/MarkerTorso");
            markers[1] = transform.FindChild("Markers/MarkerNeck");
            markers[0] = transform.FindChild("Markers/MarkerHead");
		
			markers[4] = transform.FindChild("Markers/MarkerNeck");
            markers[5] = transform.FindChild("Markers/MarkerLeftShoulder");
            markers[6] = transform.FindChild("Markers/MarkerLeftElbow");
            markers[7] = transform.FindChild("Markers/MarkerLeftWrist");
            markers[8] = transform.FindChild("Markers/MarkerLeftHand");
			
			markers[9] = transform.FindChild("Markers/MarkerNeck");
			markers[10] = transform.FindChild("Markers/MarkerNeck");
		
            markers[11] = transform.FindChild("Markers/MarkerRightShoulder");
            markers[12] = transform.FindChild("Markers/MarkerRightElbow");
            markers[13] = transform.FindChild("Markers/MarkerRightWrist");
            markers[14] = transform.FindChild("Markers/MarkerRightHand");
		
			markers[15] = transform.FindChild("Markers/MarkerNeck");

            markers[16] = transform.FindChild("Markers/MarkerLeftHip");
            markers[17] = transform.FindChild("Markers/MarkerLeftKnee");
            markers[18] = transform.FindChild("Markers/MarkerLeftAnkle");
            markers[19] = transform.FindChild("Markers/MarkerLeftFoot");

            markers[20] = transform.FindChild("Markers/MarkerRightHip");
            markers[21] = transform.FindChild("Markers/MarkerRightKnee");
            markers[22] = transform.FindChild("Markers/MarkerRightAnkle");
            markers[23] = transform.FindChild("Markers/MarkerRightFoot");
			
			/*for(int i=0;i<markers.Length;i++)
			{
				jointSend[i]=markers[i];
				Debug.Log(i+"  "+"JOINTSEND");
			}*/
			
			
	        limbs[0] = transform.FindChild("Limbs/LimbSpineLower");
	        limbs[1] = transform.FindChild("Limbs/LimbSpineUpper");
	        limbs[2] = transform.FindChild("Limbs/LimbNeck");
	
	        limbs[3] = transform.FindChild("Limbs/LimbLeftShoulder");
	        limbs[4] = transform.FindChild("Limbs/LimbLeftArmUpper");
	        limbs[5] = transform.FindChild("Limbs/LimbLeftArmLower");
	        limbs[6] = transform.FindChild("Limbs/LimbLeftHand");
	
	        limbs[7] = transform.FindChild("Limbs/LimbRightShoulder");
	        limbs[8] = transform.FindChild("Limbs/LimbRightArmUpper");
	        limbs[9] = transform.FindChild("Limbs/LimbRightArmLower");
	        limbs[10] = transform.FindChild("Limbs/LimbRightHand");
	
	        limbs[11] = transform.FindChild("Limbs/LimbLeftHip");
	        limbs[12] = transform.FindChild("Limbs/LimbLeftLegUpper");
	        limbs[13] = transform.FindChild("Limbs/LimbLeftLegLower");
	        limbs[14] = transform.FindChild("Limbs/LimbLeftFoot");
	
	        limbs[15] = transform.FindChild("Limbs/LimbRightHip");
	        limbs[16] = transform.FindChild("Limbs/LimbRightLegUpper");
	        limbs[17] = transform.FindChild("Limbs/LimbRightLegLower");
	        limbs[18] = transform.FindChild("Limbs/LimbRightFoot");
			
		jointSend = markers;
		
	}
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		//locateMarkersAndLimbs();
			
		if(stream.isWriting)
		{	
			//jointSend[4].position=jointSend[9].position=jointSend[10].position=jointSend[15].position= Vector3.zero;
			//jointSend[4].rotation=jointSend[9].rotation=jointSend[10].rotation=jointSend[15].rotation= Quaternion.identity;
			locateMarkersAndLimbs();
			for(int i=0;i<markers.Length;i++)
			{
								
				Vector3 tmpPos = jointSend[i].position;
				Quaternion tmpRot = jointSend[i].rotation;
				//Debug.LogError((jointSend[i])?"Exists"+i:"Does Not Exist!"+":"+i); 
			    stream.Serialize(ref tmpPos);
				stream.Serialize(ref tmpRot);
				
			}
		}
		else
		{
			locateMarkersAndLimbs();
			
			Vector3 tempPos = Vector3.zero;
			Quaternion tempRot = Quaternion.identity;
		
			for(int i=0;i<markers.Length;i++)
			{
					
				stream.Serialize(ref  tempPos);
				stream.Serialize(ref tempRot);
				//Debug.Log(tempPos);
				//Debug.Log(tempRot);
				markers[i].position = tempPos;
				markers[i].rotation = tempRot;
				
			}			
			
			SetLimbsFromMarkers();
		}
		
	}

    void getMostRecentReport(Transform joint, int trackerNumber)
    {
		
		if((trackerNumber == 4)||(trackerNumber == 9)||(trackerNumber == 10)||(trackerNumber == 15))
			return;
			
		//TO DO - Put in some error tracking here. If a Kinect goes offline this throws errors.
        trackerData[trackerNumber] = (TrackerData)Marshal.PtrToStructure(trackerDataPointer[trackerNumber], typeof(TrackerData));

        Quaternion trackerRotation = Quaternion.identity;
        trackerRotation.x = (float)trackerData[trackerNumber].rotation[0];
        trackerRotation.y = (float)trackerData[trackerNumber].rotation[1];
        trackerRotation.z = (float)trackerData[trackerNumber].rotation[2];
        trackerRotation.w = (float)trackerData[trackerNumber].rotation[3];
		
        Vector3 trackerPosition = Vector3.zero;
        trackerPosition.x = (float)trackerData[trackerNumber].position[0];
        trackerPosition.y = (float)trackerData[trackerNumber].position[1];
        trackerPosition.z = (float)trackerData[trackerNumber].position[2];
				
        joint.localPosition = trackerPosition;
		joint.localRotation = trackerRotation;
		
				
    }
	
    void setLimb(Transform joint, Transform start, Transform end)
    {
		Quaternion limbRotation = new Quaternion();
        Vector3 limbVector = end.position - start.position;

        joint.position = start.position;
        limbRotation.SetFromToRotation(Vector3.up, limbVector.normalized);
        joint.rotation = limbRotation;

        Vector3 limbScale = joint.localScale;
        limbScale.y = getDistanceBetween(start, end) / 2.0f;
        joint.localScale = limbScale;

        joint.Translate(0.0f, limbScale.y, 0.0f);
    }
	
    float getDistanceBetween(Transform joint1, Transform joint2)
    {
        Vector3 position1 = joint1.position;
        Vector3 position2 = joint2.position;

        return (float)Math.Sqrt(
            (position1.x - position2.x) * (position1.x - position2.x) +
            (position1.y - position2.y) * (position1.y - position2.y) +
            (position1.z - position2.z) * (position1.z - position2.z)
         );
    }
	
	public void SetLimbsFromMarkers(){
		//Set limb positions based on markers
		setLimb(limbs[0], markers[3], markers[2]);
		setLimb(limbs[1], markers[2], markers[1]);
		setLimb(limbs[2], markers[1], markers[0]);
		
		setLimb(limbs[3], markers[1], markers[5]);
		setLimb(limbs[4], markers[5], markers[6]);
		setLimb(limbs[5], markers[6], markers[7]);
		setLimb(limbs[6], markers[7], markers[8]);
		
		setLimb(limbs[7], markers[1], markers[11]);
		setLimb(limbs[8], markers[11], markers[12]);
		setLimb(limbs[9], markers[12], markers[13]);
		setLimb(limbs[10], markers[13], markers[14]);
		
		setLimb(limbs[11], markers[3], markers[16]);
		setLimb(limbs[12], markers[16], markers[17]);
		setLimb(limbs[13], markers[17], markers[18]);
		setLimb(limbs[14], markers[18], markers[19]);
		
		setLimb(limbs[15], markers[3], markers[20]);
		setLimb(limbs[16], markers[20], markers[21]);
		setLimb(limbs[17], markers[21], markers[22]);
		setLimb(limbs[18], markers[22], markers[23]);	
		
		for(int i=0;i<limbs.Length;i++)
		{
			//Debug.Log(limbs[i]);
		}
	}
}

