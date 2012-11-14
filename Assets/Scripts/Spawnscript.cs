using UnityEngine;
using System.Collections;

public class Spawnscript : MonoBehaviour {
	
	public Transform player1;
	public Transform player2;
	//public Transform ball;
	public static Vector3 offset = new Vector3(-6F,0,0.25F);
	
	void Update()
	{
		if(Network.isServer)
		{
			if((GameObject.Find("KinectTracker 1 2(Clone)"))!=null)
			{
				Ball.leftHandP2 = GameObject.Find("KinectTracker 1 2(Clone)/Markers/MarkerLeftHand");
				Ball.rightHandP2 = GameObject.Find("KinectTracker 1 2(Clone)/Markers/MarkerRightHand");
			}
		}
		
		if(Network.isClient)
		{
			if((GameObject.Find("KinectTracker 1 1(Clone)"))!=null)
			{
				Ball.leftHandP1 = GameObject.Find("KinectTracker 1 1(Clone)/Markers/MarkerLeftHand");
				Ball.rightHandP1 = GameObject.Find("KinectTracker 1 1(Clone)/Markers/MarkerRightHand");
			}
		}
	}
	
	void OnServerInitialized()
	{
		SpawnPlayer1();
		
	}
	
	void OnConnectedToServer()
	{
		transform.position += offset;
		SpawnPlayer2();
		//Debug.Log("Sunil shows");
	}
	
	
	void SpawnPlayer1(){
		Network.Instantiate(player1, transform.position, transform.rotation,0);
		//Network.Instantiate(ball, transform.position - new Vector3(0,0,1F), transform.rotation,0);
		Ball.leftHandP1 = GameObject.Find("KinectTracker 1 1(Clone)/Markers/MarkerLeftHand");
		Ball.rightHandP1 = GameObject.Find("KinectTracker 1 1(Clone)/Markers/MarkerRightHand");
		
	}

	void SpawnPlayer2(){
		Network.Instantiate(player2, transform.position, transform.rotation,0);
		//Network.Instantiate(ball, transform.position - new Vector3(0,0,1F), transform.rotation,0);
		Ball.leftHandP2 = GameObject.Find("KinectTracker 1 2(Clone)/Markers/MarkerLeftHand");
		Ball.rightHandP2 = GameObject.Find("KinectTracker 1 2(Clone)/Markers/MarkerRightHand");
	}
	
	void OnPlayerDisconnected(NetworkPlayer player)
	{
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
	}
	
	void OnDisconnectedFromServer(NetworkDisconnection info)
	{
		Network.RemoveRPCs(Network.player);
		Network.DestroyPlayerObjects(Network.player);
		Application.LoadLevel(Application.loadedLevel);
		
	}
	
	
}
