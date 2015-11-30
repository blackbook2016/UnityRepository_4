using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PhotonHashTable = ExitGames.Client.Photon.Hashtable;

public class PhotonManager_Base : Photon.PunBehaviour 
{		
	public static PhotonManager_Base Instance {
		get {
			if (instance == null) {
				instance = UnityEngine.Object.FindObjectOfType<PhotonManager_Base>();
			}
			if (instance == null) {
				var go = new GameObject("PhotonManager");
				instance = go.AddComponent<PhotonManager_Base>();
				go.transform.localPosition = Vector3.zero;
			}
			return instance;
		}
	}
	private static PhotonManager_Base instance = null;
	
	[SerializeField]
	private List<string> list_InstantiateObj = new List<string>();
	
	[SerializeField]
	private List<Transform> list_SpawnPoints = new List<Transform>();
	
	void Awake()
	{
		Connect();
	}
	
	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}
	
	public void Connect()
	{
		PhotonNetwork.ConnectUsingSettings("0.6");
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
		LoadViewers();
	}
	
	public override void OnConnectedToMaster()
	{
		PhotonNetwork.JoinRandomRoom();
	}
	
	public PeerState ConnectionStateDetailed()
	{
		return PhotonNetwork.connectionStateDetailed;
	}
	
	private void LoadViewers()
	{
		foreach(var name in list_InstantiateObj)
		{			
			GameObject player = PhotonNetwork.Instantiate(name, transform.position, Quaternion.identity, 0);
		}		
		
		if(list_SpawnPoints.Count != 0)
		{
			List<int> EmptyChairs = new List<int>();
			EmptyChairs.Add(0);
			EmptyChairs.Add(1);
			EmptyChairs.Add(2);
			EmptyChairs.Add(3);
			
			if(PhotonNetwork.playerList.Length != 1)
			{
				foreach(var pl in PhotonNetwork.playerList)
				{
					if(pl.customProperties["TC"] != null)
					{
						int takenChair = (int)pl.customProperties["TC"];
						if(EmptyChairs.Contains(takenChair))
							EmptyChairs.Remove(takenChair);
					}
				}
			}
			
			int rand = EmptyChairs[Random.Range(0, EmptyChairs.Count)];
			Cardboard.SDK.transform.position = list_SpawnPoints[rand].position;
			Cardboard.SDK.transform.rotation = list_SpawnPoints[rand].rotation;
			
			PhotonHashTable props = new PhotonHashTable();
			props.Add( "TC", rand);
			
			PhotonNetwork.player.SetCustomProperties(props);
		}
	}
}
