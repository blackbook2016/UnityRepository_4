using UnityEngine;
using System.Collections;

public class GameManager_IMGT : Singleton<GameManager_IMGT> 
{	
	void Awake()
	{
		SceneLoader_IMGT.Instance.LoadScene();
		PhotonManager.Instance.Connect();
	}
}
