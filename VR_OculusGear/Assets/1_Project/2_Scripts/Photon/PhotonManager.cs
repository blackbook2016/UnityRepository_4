using UnityEngine;
using System.Collections;

public class PhotonManager : Photon.PunBehaviour 
{		
	void Start()
	{
		PhotonNetwork.ConnectUsingSettings("0.2");
	}
	
	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}

	public override void OnJoinedLobby()
	{
		Debug.Log("JoinRandom");
		PhotonNetwork.JoinRandomRoom();
	}
	
	public void OnPhotonRandomJoinFailed()
	{
		Debug.Log("Can't join random room!");
		PhotonNetwork.CreateRoom(null);
	}

	public override void OnJoinedRoom()
	{
		print ("Joined Room");
	}

	public override void OnConnectedToMaster()
	{
		// when AutoJoinLobby is off, this method gets called when PUN finished the connection (instead of OnJoinedLobby())
		PhotonNetwork.JoinRandomRoom();
	}
}
