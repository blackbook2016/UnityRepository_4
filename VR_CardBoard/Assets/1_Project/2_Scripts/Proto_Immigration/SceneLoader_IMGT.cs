using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneLoader_IMGT : Singleton<SceneLoader_IMGT> 
{
	[SerializeField]
	private GameObject Cardboard_prefab;
	[SerializeField]
	private RPGCamera cam;
	[SerializeField]
	private GameObject ui;
	[SerializeField]
	private List<Transform> list_trs_Spawn;

	public void LoadScene()
	{		
		#if UNITY_ANDROID
		Cardboard_prefab.SetActive(true);
		UIManager_IMGT.Instance.LoadUI();
		#elif UNITY_STANDALONE
		GameObject player = PhotonNetwork.Instantiate("Robot Kyle RP", list_trs_Spawn[Random.Range(0, list_trs_Spawn.Count)].position, Quaternion.identity, 0);
		player.transform.eulerAngles = Vector3.up * 180;

		cam.gameObject.SetActive(true);
		cam.Target = player.transform;
		#endif
	}
}
