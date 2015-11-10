using UnityEngine;
using System.Collections;

public class SceneLoader_IMGT : Singleton<SceneLoader_IMGT> 
{
	[SerializeField]
	private GameObject Cardboard_prefab;
	[SerializeField]
	private RPGCamera cam;
	[SerializeField]
	private GameObject ui;

	public void LoadScene()
	{		
		#if UNITY_ANDROID
		Cardboard_prefab.SetActive(true);
		ui.SetActive(true);

		#elif UNITY_STANDALONE
		GameObject player = PhotonNetwork.Instantiate("Robot Kyle RP", Vector3.forward * 15, Quaternion.identity, 0);
		player.transform.eulerAngles = Vector3.up * 180;

		cam.gameObject.SetActive(true);
		cam.Target = player.transform;
		#endif
	}
}
