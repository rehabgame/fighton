using UnityEngine;
using System.Collections;

public class Spawnscript : MonoBehaviour {
	
	public Transform player;
	public Transform ball;
	public static Vector3 offset = new Vector3(-7,0,1.35F);
	
	void OnServerInitialized()
	{
		SpawnPlayer();
		
	}
	
	void OnConnectedToServer()
	{
		transform.position += offset;
		SpawnPlayer();
		//Debug.Log("Sunil shows");
	}
	
	
	void SpawnPlayer(){
		Network.Instantiate(player, transform.position, transform.rotation,0);
		Network.Instantiate(ball, transform.position, transform.rotation,0);
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
