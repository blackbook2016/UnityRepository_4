using System;
using System.Collections;

public class EventManager : Singleton<EventManager> 
{		
	PhotonView photonView;

	public static event Action gameReset;

	public void Start()
	{		
		photonView = PhotonView.Get(this);
	}

	public void GameReset()
	{
		if(gameReset != null)
		{	
			photonView.RPC("RPCReset", PhotonTargets.All);
		}
	}

	[PunRPC]
	public void RPCReset()
	{
		if(gameReset != null)
		{
			gameReset();			
		}
	}
}
