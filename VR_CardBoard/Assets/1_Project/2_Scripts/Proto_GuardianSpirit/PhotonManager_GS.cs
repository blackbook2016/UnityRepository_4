using UnityEngine;
using System.Collections;

public class PhotonManager_GS : Photon.PunBehaviour 
{		
	public static PhotonManager_GS Instance {
		get {
			if (instance == null) {
				instance = UnityEngine.Object.FindObjectOfType<PhotonManager_GS>();
			}
			if (instance == null) {
				var go = new GameObject("PhotonManager");
				instance = go.AddComponent<PhotonManager_GS>();
				go.transform.localPosition = Vector3.zero;
			}
			return instance;
		}
	}
	private static PhotonManager_GS instance = null;
	
	public void Connect()
	{
//		PhotonPlayer
		PhotonNetwork.ConnectUsingSettings("0.4");
	}
	
	public override void OnJoinedLobby()
	{
		print ("OnJoinedLobby");
		if(PhotonNetwork.playerList.Length == 0)
		{
			RoomOptions roomOptions = new RoomOptions() { isVisible = true, maxPlayers = 2 };
			PhotonNetwork.JoinOrCreateRoom("roomName", roomOptions, TypedLobby.Default);
			//		PhotonNetwork.JoinRandomRoom();
		}
			foreach(var pl in PhotonNetwork.playerList)
			{
				print (pl.allProperties);
			}
	}
	
	public void OnPhotonRandomJoinFailed()
	{		
		print ("OnPhotonRandomJoinFailed");
		//		PhotonNetwork.CreateRoom(null);
		RoomOptions roomOptions = new RoomOptions() { isVisible = true, maxPlayers = 2 };
		PhotonNetwork.JoinOrCreateRoom("roomName", roomOptions, TypedLobby.Default);
		
	}
	
	public override void OnJoinedRoom()
	{
		print ("OnJoinedRoom");
		UIManager_GS.Instance.ButtonReadyEnable();
	}
	
	public override void OnConnectedToMaster()
	{
		print ("OnConnectedToMaster");
//		PhotonNetwork.JoinLobby();
	}
	
	public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
	{
		if (PhotonNetwork.playerList.Length % 2 == 0) 
		{
			GameManager_GS.Instance.StartGame();
		} 
	}
}