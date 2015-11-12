using UnityEngine;
using System.Collections;

public class Player_IMGT : MonoBehaviour 
{
	PhotonView photonView;
	RPGMovement move;

	Vector3 init_Pos;
	Quaternion init_Rot;

	public void Start()
	{		
		init_Pos = transform.position;
		init_Rot = transform.rotation;

		photonView = GetComponent<PhotonView>();
		move = GetComponent<RPGMovement>();
	}

	void OnEnable() 
	{
		EventManager_IMGT.gameReset += Reset;
	}
	
	void OnDisable() 
	{
		EventManager_IMGT.gameReset -= Reset;
	}
	
	void Reset()
	{
		if(photonView.isMine)
		{
			transform.position = init_Pos;
			transform.rotation = init_Rot;
			move.Reset();
			RPGCamera.Instance.transform.eulerAngles = Vector3.up * 180;
		}
	}
}
