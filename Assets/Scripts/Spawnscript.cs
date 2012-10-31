using UnityEngine;
using System.Collections;

public class Spawnscript : MonoBehaviour {
	
	public Transform player1, player2;
	//public Transform ball;
	public static Vector3 offset = new Vector3(-5F,0,-0.5F);
	
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
