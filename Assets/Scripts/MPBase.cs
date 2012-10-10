using UnityEngine;
using System.Collections;
using System;

public class MPBase : MonoBehaviour 
{
	public string connectToIp = "127.0.0.1";
	public int connectPort = 25000;
	public bool useNAT = false;
	public string IPAddress = "";
	public string port = "";
	
	string playerName = "<NAME ME>";

	void OnGUI()
	{
		//Debug.Log("onGUI works....");
		if(Network.peerType == NetworkPeerType.Disconnected)
		{
			if(GUILayout.Button("Connect"))
			{
				if(playerName != "<NAME ME>")
				{
					Network.Connect(connectToIp, connectPort);
					PlayerPrefs.SetString("playerName", playerName);
				}
					
			}
			if(GUILayout.Button("Start Server"))
			{
				if(playerName != "<NAME ME>")
				{
					Network.InitializeServer(32, connectPort, useNAT);
					foreach(GameObject go in FindObjectsOfType(typeof(GameObject)))
					{
						go.SendMessage("OnNetworkLoadedLevel",SendMessageOptions.DontRequireReceiver);
					
					}
					PlayerPrefs.SetString("playerName", playerName);
				}
			}
		
			playerName = GUILayout.TextField(playerName);
			connectToIp = GUILayout.TextField(connectToIp);
			connectPort =  Convert.ToInt32(GUILayout.TextField(connectPort.ToString()));
		}
		else
		{
				if(Network.peerType == NetworkPeerType.Connecting)
				{
					GUILayout.Label("Connect Status: Connecting");
				}
				else if(Network.peerType == NetworkPeerType.Client)
				{
					GUILayout.Label("Connect Status: Client!");
					GUILayout.Label("Ping to Sever :"+ Network.GetAveragePing(Network.connections[0]));
				}
				else if(Network.peerType == NetworkPeerType.Server)
				{
					GUILayout.Label("Connect Status: Server!");
					GUILayout.Label("Number of Connections : "+ Network.connections.Length);
					
					if(Network.connections.Length>=1)
					{
						GUILayout.Label("Ping to Sever :"+ Network.GetAveragePing(Network.connections[0]));
					}
						
				}
				if(GUILayout.Button("Disconnect"))
				{
					Network.Disconnect(200);
				}
				IPAddress = Network.player.ipAddress;
				port = Network.player.port.ToString();
				GUILayout.Label ("IP Address :"+ IPAddress +"   Port :"+ port);
				
		}
		
	}
	void OnConnectedToServer()
	{
		foreach(GameObject go in FindObjectsOfType(typeof(GameObject)))
		{
			go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
		
		}
	}

}
