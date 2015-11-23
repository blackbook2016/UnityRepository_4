using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PhotonHashTable = ExitGames.Client.Photon.Hashtable;

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
	
	private PlayerStatus ps;

	void Update()
	{
//		if(PhotonNetwork.room != null)
//			print (PhotonNetwork.room.customProperties);
		print (PhotonNetwork.connectionStateDetailed.ToString());
	}
	
	public void Connect()
	{
		SetPlayerStatus();
		UpdatePlayerSettings();

		PhotonNetwork.ConnectUsingSettings("0.4");
	}

	public override void OnConnectedToMaster()
	{
		print ("OnConnectedToMaster");
		
		UIManager_GS.Instance.ButtonReadyEnable();
		ConnectToRoom();
	}

	private void ConnectToRoom()
	{		
		//		if(PhotonNetwork.playerList.Length % 2 != 0)
		//		{
		//			RoomOptions roomOptions = new RoomOptions() { isVisible = true, maxPlayers = 2 };
		//			PhotonNetwork.JoinOrCreateRoom("roomName", roomOptions, TypedLobby.Default);
		
		RoomOptions newRoomOptions = new RoomOptions();
		newRoomOptions.isOpen = true;
		newRoomOptions.isVisible = true;
		newRoomOptions.maxPlayers = 2;
		
		if(ps.type == PlayerType.PC)
		newRoomOptions.customRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "PC", false } };
		else
		newRoomOptions.customRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "VR", false } };
		
		//			newRoomOptions.customRoomPropertiesForLobby = new string[] { "C0" }; // this makes "C0" available in the lobby
		
		// let's create this room in SqlLobby "myLobby" explicitly
		//			TypedLobby sqlLobby = new TypedLobby("myLobby", LobbyType.SqlLobby);
		//			PhotonNetwork.CreateRoom(roomName, newRoomOptions, sqlLobby);
		//			PhotonNetwork.JoinRandomRoom(newRoomOptions.customRoomProperties, newRoomOptions.maxPlayers = 2);
		//		}	
		PhotonNetwork.JoinOrCreateRoom("room" + PhotonNetwork.countOfRooms, newRoomOptions, TypedLobby.Default);
	}

	public override void OnJoinedRoom()
	{
		print ("OnJoinedRoom");
		UpdatePlayerSettings();
		PhotonHashTable props = new PhotonHashTable();

		if(ps.type == PlayerType.PC)
			props.Add( "PC", true);
		else
			props.Add( "VR", true);

		PhotonNetwork.room.SetCustomProperties( props );
		print (PhotonNetwork.room.customProperties + "   " + PhotonNetwork.room.name);
	}
	
	public override void OnJoinedLobby()
	{
		print ("OnJoinedLobby");
	}
	
	public void OnPhotonRandomJoinFailed()
	{		
		print ("OnPhotonRandomJoinFailed");		
	}
	
	public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
	{
		print ("NewPlayer");
//		if((bool)PhotonNetwork.player.customProperties["RDY"])
//			MatchMaker();

//		if (PhotonNetwork.playerList.Length % 2 == 0) 
//		{
//			GameManager_GS.Instance.StartGame();
//		} 
	}

	[PunRPC]
	private void MatchMaker()
	{
		if(PhotonNetwork.playerList.Length > 1)
		{
			foreach(var pl in PhotonNetwork.playerList)
			{
				if(!(bool)pl.customProperties["RDY"])
					return;
			}
			GameManager_GS.Instance.StartGame();
		}
	}

	private void SetPlayerStatus()
	{
		#if UNITY_ANDROID		
		ps = new PlayerStatus(false, PlayerType.VR);
		#elif UNITY_STANDALONE		
		ps = new PlayerStatus(false, PlayerType.PC);
		#endif
	}
	
	private void UpdatePlayerSettings()
	{
		PhotonHashTable props = new PhotonHashTable();
		
		props.Add( "Type", ps.type);
		props.Add( "RDY", ps.isReady);
		
		PhotonNetwork.player.SetCustomProperties( props );
	}

	public void PlayerReady()
	{
		ps.isReady = true;
		photonView.RPC("MatchMaker", PhotonTargets.All);
	}
}