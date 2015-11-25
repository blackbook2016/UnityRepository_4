using UnityEngine;
using System.Collections;
using PhotonHashTable = ExitGames.Client.Photon.Hashtable;

public class PhotonManager_Border : Photon.PunBehaviour 
{		
	public static PhotonManager_Border Instance {
		get {
			if (instance == null) {
				instance = UnityEngine.Object.FindObjectOfType<PhotonManager_Border>();
			}
			if (instance == null) {
				var go = new GameObject("PhotonManager");
				instance = go.AddComponent<PhotonManager_Border>();
				go.transform.localPosition = Vector3.zero;
			}
			return instance;
		}
	}
	private static PhotonManager_Border instance = null;
	
	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}
	
	public void Connect()
	{
		PhotonNetwork.ConnectUsingSettings("0.5");
	}
	
	public override void OnJoinedLobby()
	{
		PhotonNetwork.JoinRandomRoom();
	}
	
	public void OnPhotonRandomJoinFailed()
	{
		PhotonNetwork.CreateRoom(null);
	}
	
	public override void OnJoinedRoom()
	{
		
		Player_Border.PlType type;
		type = Player_Border.PlType.Guard;

		foreach(var pl in PhotonNetwork.playerList)
		{
			if(!PhotonHashTable.ReferenceEquals(pl.customProperties["Type"],null) 
			   && (int)pl.customProperties["Type"] == 0)
			{
				type = Player_Border.PlType.Fugitive;
			}
		}

		PhotonHashTable props = new PhotonHashTable();		
		props.Add("Type", (int)type);		
		PhotonNetwork.player.SetCustomProperties( props );

		GameObject player = PhotonNetwork.Instantiate("PlayerBorder", transform.position, Quaternion.identity, 0);

		GameManager_Border.player = player.GetComponent<Player_Border>();
		GameManager_Border.player.Init(type);
	}
	
	public override void OnConnectedToMaster()
	{
		// when AutoJoinLobby is off, this method gets called when PUN finished the connection (instead of OnJoinedLobby())
		PhotonNetwork.JoinRandomRoom();
	}
	
	public PeerState ConnectionStateDetailed()
	{
		return PhotonNetwork.connectionStateDetailed;
	}
}
