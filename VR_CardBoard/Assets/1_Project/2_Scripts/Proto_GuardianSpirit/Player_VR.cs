using UnityEngine;
using System.Collections;

public class Player_VR : PlayerEntity
{	
	public static Player_VR Instance 
	{
		get {
			if (instance == null) {
				instance = UnityEngine.Object.FindObjectOfType<Player_VR>();
			}
			if (instance == null) {
				var go = new GameObject("Cardboard");
				instance = go.AddComponent<Player_VR>();
				go.transform.localPosition = Vector3.zero;
			}
			return instance;
		}
	}
	
	private static Player_VR instance = null;

	public override void Init () 
	{
		type = PlayerType.VR;
	}
}
