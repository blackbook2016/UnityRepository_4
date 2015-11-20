using UnityEngine;
using System.Collections;

public class Player_PC : PlayerEntity
{
	public static Player_PC Instance 
	{
		get {
			if (instance == null) {
				instance = UnityEngine.Object.FindObjectOfType<Player_PC>();
			}
			if (instance == null) {
				var go = new GameObject("Cardboard");
				instance = go.AddComponent<Player_PC>();
				go.transform.localPosition = Vector3.zero;
			}
			return instance;
		}
	}
	
	private static Player_PC instance = null;

	PhotonView photonView;
	RPGMovement move;
	
	Vector3 init_Pos;
	Quaternion init_Rot;

	void Start()
	{
		Init();
	}

	public override void Init()
	{		
		init_Pos = transform.position;
		init_Rot = transform.rotation;
		
		photonView = GetComponent<PhotonView>();
		move = GetComponent<RPGMovement>();
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
