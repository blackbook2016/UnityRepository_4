using System;
using System.Collections;

public class EventManager : Singleton<EventManager> 
{	
	public static event Action gameReset;

	public void GameReset()
	{
		if(gameReset != null)
			gameReset();
	}
}
