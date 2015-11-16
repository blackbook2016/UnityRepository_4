using UnityEngine;
using System.Collections;

public class Player_VR : PlayerEntity<Player_VR>
{	
	public override void Init () 
	{
		type = PlayerType.VR;
	}
}
