using UnityEngine;
using System;
using System.Collections;

public class EventManager_GS : Singleton<EventManager_GS>  
{
	PhotonView photonView;
	
	public static event Action gameReset;
	public static event Action gameInit;
	public static event Action<bool> gameOver;
	
	public void Start()
	{		
		photonView = GetComponent<PhotonView>();
	}
	
	public void RPCReset()
	{
		photonView.RPC("GameReset", PhotonTargets.All);
	}
	
	[PunRPC]
	public void GameReset()
	{
		if(gameReset != null)
		{
			gameReset();			
		}
	}

	public void GameInit()
	{
		if(gameInit != null)
		{
			gameInit();			
		}
	}

	public void RPCGameOver(bool status)
	{
		photonView.RPC("GameOver", PhotonTargets.All, status);
	}
	
	[PunRPC]
	public void GameOver(bool status)
	{
		if(gameOver != null)
		{
			gameOver(status);			
		}
	}
}
