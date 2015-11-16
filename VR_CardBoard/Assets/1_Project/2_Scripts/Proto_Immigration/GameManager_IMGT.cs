using UnityEngine;
using System.Collections;

public enum GameState
{
	Connect,
	LoadScene,
	Play,
	Pause,
	Victory,
	Defeat
}

public class GameManager_IMGT : Singleton<GameManager_IMGT> 
{	
	GameState game_State;

	void Start()
	{
		StartCoroutine("StartGame");
	}

	private IEnumerator StartGame()
	{
		game_State = GameState.Connect;
		PhotonManager_IMGT.Instance.Connect();

		while(PhotonManager_IMGT.Instance.ConnectionStateDetailed() != PeerState.Joined)
			yield return null;

		game_State = GameState.LoadScene;
		SceneLoader_IMGT.Instance.LoadScene();
	}

	public IEnumerator ChangeGameState(GameState state)
	{
		switch(state)
		{
		case GameState.Play:
			break;
		case GameState.Pause:
			break;
		case GameState.Victory:			
			EventManager_IMGT.Instance.RPCGameOver(true);
			yield return new WaitForSeconds(1.0f);
			EventManager_IMGT.Instance.RPCReset();
			break;
		case GameState.Defeat:
			EventManager_IMGT.Instance.RPCGameOver(false);
			yield return new WaitForSeconds(1.0f);
			EventManager_IMGT.Instance.RPCReset();
			break;
		}
		yield return null;
	}
}
