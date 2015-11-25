using UnityEngine;
using System.Collections;

public class GameManager_Border : MonoBehaviour 
{
	public static Player_Border player = null;

	void Awake()
	{
		PhotonManager_Border.Instance.Connect();
	}
}
