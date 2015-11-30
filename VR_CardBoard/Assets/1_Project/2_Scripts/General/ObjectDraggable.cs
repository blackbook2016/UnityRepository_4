using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class ObjectDraggable : MonoBehaviour 
{
	Rigidbody rb;
	Vector3 init_pos;
	Quaternion init_rot;
	Material mat;
	PhotonView photonView;

	void Start () 
	{
		mat = GetComponent<MeshRenderer>().material;
		rb = GetComponent<Rigidbody>();	
		init_pos = transform.position;
		init_rot = transform.rotation;
		
		photonView = PhotonView.Get(this);
	}
	
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Space))
			Change();
	}
	
	void OnEnable() 
	{
		EventManager.gameReset += Reset;
	}
	
	void OnDisable() 
	{
		EventManager.gameReset -= Reset;
	}

	void Reset()
	{
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;

		transform.position = init_pos;
		transform.rotation = init_rot;
	}
	
	public void Change()
	{
		Vector3 col = new Vector3(Random.Range(0.0f, 1.1f), Random.Range(0.0f, 1.1f), Random.Range(0.0f, 1.1f));
		mat.color = new Color( col.x, col.y, col.z );
		photonView.RPC("SetColor", PhotonTargets.All, col);
	}

	[PunRPC]
	void SetColor(Vector3 col)
	{
		Color color = new Color( col.x, col.y, col.z );
		GetComponent<MeshRenderer>().material.color = color;
	}

	public void RequestOwnership()
	{
		if(photonView.ownerId != PhotonNetwork.player.ID)
			photonView.RequestOwnership();
	}

	public void ChangeVelocity(Vector3 vel)
	{
		if(photonView.isMine)
		{
			rb.velocity = vel;
			photonView.RPC("UpdateObject", PhotonTargets.All, transform.position, transform.eulerAngles, vel);
		}
	}

	[PunRPC]
	public void UpdateObject(Vector3 pos, Vector3 rot, Vector3 vel)
	{
//		transform.position = pos;
//		transform.eulerAngles = rot;
		rb.velocity = vel;
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			// We own this player: send the others our data
			if(photonView.isMine)
			{
				stream.SendNext(transform.position);
			}
			else
				print (gameObject);
		}
		else
		{
			transform.position = (Vector3) stream.ReceiveNext();
		}
	}

//	public void OnOwnershipRequest(object[] viewAndPlayer)
//	{
//		PhotonView view = viewAndPlayer[0] as PhotonView;
//		PhotonPlayer requestingPlayer = viewAndPlayer[1] as PhotonPlayer;
//		
//		Debug.Log("OnOwnershipRequest(): Player " + requestingPlayer + " requests ownership of: " + view + ".");
//		view.TransferOwnership(requestingPlayer.ID);
//	}
}
