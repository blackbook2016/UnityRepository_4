using UnityEngine;
using System;
using System.Collections;

public class EventManager_IMGT : Singleton<EventManager_IMGT>  
{
	PhotonView photonView;
	
	public static event Action gameReset;
	
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
}
