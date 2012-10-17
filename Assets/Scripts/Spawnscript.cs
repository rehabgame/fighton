using UnityEngine;
using System.Collections;

public class Spawnscript : MonoBehaviour {
	
	public Transform player1, player2;
	//public Transform ball;
	public static Vector3 offset = new Vector3(-1.5F,0,1F);
	
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
	}

	void SpawnPlayer2(){
		Network.Instantiate(player2, transform.position, transform.rotation,0);
		//Network.Instantiate(ball, transform.position - new Vector3(0,0,1F), transform.rotation,0);
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
