using UnityEngine;
using UnityEngine.Networking;

public class AvatarMultiplayer : NetworkBehaviour 
{
	public override void OnStartLocalPlayer () 
	{
		GameObject camera = GameObject.FindWithTag ("MainCamera");
		camera.transform.position = transform.position + 0.5f * Vector3.up;
		transform.parent = camera.transform;
		transform.localPosition = Vector3.zero;
	}
}