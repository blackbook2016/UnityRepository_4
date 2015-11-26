using UnityEngine;
using System.Collections;

public class Player_Office : Photon.MonoBehaviour 
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

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
			
		}
		else
		{
			this.correctPlayerPos = (Vector3)stream.ReceiveNext();
			this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
		}
	}
}
