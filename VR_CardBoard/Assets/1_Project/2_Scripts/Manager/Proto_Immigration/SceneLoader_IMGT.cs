using UnityEngine;
using System.Collections;

public class SceneLoader_IMGT : Singleton<SceneLoader_IMGT> 
{
	[SerializeField]
	private GameObject player_prefab;
	[SerializeField]
	private GameObject Cardboard_prefab;

	public void LoadScene()
	{		
		#if UNITY_ANDROID
		#elif UNITY_EDITOR
		#endif
	}
}
