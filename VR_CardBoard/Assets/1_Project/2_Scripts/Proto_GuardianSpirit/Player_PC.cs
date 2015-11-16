using UnityEngine;
using System.Collections;

public class Player_PC : PlayerEntity<Player_PC>
{
	PhotonView photonView;
	RPGMovement move;
	
	Vector3 init_Pos;
	Quaternion init_Rot;
	
	public override void Init()
	{		
		init_Pos = transform.position;
		init_Rot = transform.rotation;
		
		photonView = GetComponent<PhotonView>();
		move = GetComponent<RPGMovement>();

		type = PlayerType.PC;
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
