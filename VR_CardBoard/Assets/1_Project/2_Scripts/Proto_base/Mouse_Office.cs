using UnityEngine;
using System.Collections;

public class Mouse_Office : MonoBehaviour 
{
	private Vector3 mousePos_init;
	private Vector3 mousePos_current;
	private Vector3 mousePos_lastFrame;
	private Vector3 delta;

	PhotonView photonView;

	void Start () 
	{
		mousePos_init = transform.position;

		photonView = PhotonView.Get(this);
	}

	void Update () 
	{
		if(Input.mousePresent && PhotonNetwork.inRoom)
		{
			delta = Input.mousePosition - mousePos_lastFrame;
			mousePos_current = new Vector3(Input.mousePosition.x / 600, 0, Input.mousePosition.y / 200);
			mousePos_current += mousePos_init;
			photonView.RPC("UpdateMousePosition", PhotonTargets.All, mousePos_current);
		}
	}

	[PunRPC]
	public void UpdateMousePosition(Vector3 mousePos_current)
	{
		transform.position = mousePos_current;
	}

}
