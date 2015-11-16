using UnityEngine;
using System.Collections;

public class GameManager_GS : Singleton<GameManager_GS> 
{	
	[SerializeField]
	private RPGCamera cam;

	private bool isReady = false;

	void Awake()
	{
		EventManager_GS.Instance.GameInit();
		PhotonManager_GS.Instance.Connect();
	}

	public void PlayerReady()
	{
		isReady = true;
		StartGame();
	}

	public void StartGame()
	{
		if(!isReady || PhotonNetwork.playerList.Length != 2)
			return;
		
		#if UNITY_STANDALONE		
		UIManager_GS.Instance.DisablePanelMainMenu();
		CameraManager_GS.Instance.SetCamera();
		#endif		
	}

}