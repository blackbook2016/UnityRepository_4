using UnityEngine;
using System.Collections;

public class Player_Base : Photon.MonoBehaviour 
{	
	[SerializeField]
	private float moveSpeed = 1;	
	private CardboardHead head;
	
	private Vector3 correctPlayerPos;
	private Quaternion correctPlayerRot;
	
	void Awake()
	{
		head = Camera.main.GetComponent<StereoController>().Head;
	}
	
	void Start () 
	{		
		transform.position = Camera.main.transform.position;
		transform.rotation = Camera.main.transform.rotation;
		transform.parent = Camera.main.transform.parent;
	}
	
	void Update () 
	{
		if(!photonView.isMine)
		{
			transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 5);
			transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 5);
		}
	}

	void LateUpdate()
	{		
		if(photonView.isMine && !Input.GetMouseButton(0))
			Cardboard.SDK.transform.position += head.transform.forward.normalized * Time.deltaTime * moveSpeed;
	}
	
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			// We own this player: send the others our data
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
			
		}
		else
		{
			// Network player, receive data
			this.correctPlayerPos = (Vector3)stream.ReceiveNext();
			this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
		}
	}
}
