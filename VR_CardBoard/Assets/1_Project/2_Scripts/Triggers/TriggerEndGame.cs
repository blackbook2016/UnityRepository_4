using UnityEngine;
using System.Collections;

public class TriggerEndGame : MonoBehaviour 
{
	private bool isActive = true;

	void OnTriggerEnter(Collider other)
	{
		if(isActive && other.tag == "Player")
		{
			GameManager_IMGT.Instance.StartCoroutine("ChangeGameState", GameState.Victory);
			isActive = false;
		}
	}

	void OnEnable()
	{
		EventManager_IMGT.gameReset += HandlegameReset;
	}

	void HandlegameReset ()
	{
		
	}

	void OnDisable()
	{
		EventManager_IMGT.gameReset -= HandlegameReset;
	}
}
