using UnityEngine;
using System.Collections;

public class PhotonManager_IMGT : Photon.PunBehaviour 
{		
	public static PhotonManager_IMGT Instance {
		get {
			if (instance == null) {
				instance = UnityEngine.Object.FindObjectOfType<PhotonManager_IMGT>();
			}
			if (instance == null) {
				var go = new GameObject("PhotonManager");
				instance = go.AddComponent<PhotonManager_IMGT>();
				go.transform.localPosition = Vector3.zero;
			}
			return instance;
		}
	}
	private static PhotonManager_IMGT instance = null;
	
	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}
	
	public void Connect()
	{
		PhotonNetwork.ConnectUsingSettings("0.3");
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
