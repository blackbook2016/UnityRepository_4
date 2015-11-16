using UnityEngine;
using System.Collections;

public class CameraManager_GS : Singleton<CameraManager_GS> 
{
	[SerializeField]
	private GameObject Cardboard_prefab;
	[SerializeField]
	private RPGCamera cam;

	void Awake()
	{
		#if UNITY_ANDROID
		Cardboard_prefab.SetActive(true);
		#elif UNITY_STANDALONE
		Cardboard_prefab.SetActive(false);
		#endif	
	}

	public void SetCamera () 
	{	
		GameObject player = PhotonNetwork.Instantiate("Robot Kyle RP", Vector3.zero, Quaternion.identity, 0);
		player.transform.eulerAngles = Vector3.up * 180;
		
		cam.gameObject.SetActive(true);
		cam.Target = player.transform;
	}
}
