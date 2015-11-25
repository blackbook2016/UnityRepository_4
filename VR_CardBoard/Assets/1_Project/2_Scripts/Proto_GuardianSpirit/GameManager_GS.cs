using UnityEngine;
using System.Collections;

public class GameManager_GS : Singleton<GameManager_GS> 
{
	[SerializeField]
	private RPGCamera cam;

	void Awake()
	{
		PhotonManager_GS.Instance.Connect();		
//		EventManager_GS.Instance.GameInit();
	}
	
	public void StartGame()
	{		
		#if UNITY_ANDROID
		#elif UNITY_STANDALONE		
		CameraManager_GS.Instance.SetPCPlayer();		
		UIManager_GS.Instance.DisablePanelMainMenu();
		#endif		
	}
}