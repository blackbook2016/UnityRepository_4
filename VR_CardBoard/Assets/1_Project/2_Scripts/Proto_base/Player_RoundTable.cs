using UnityEngine;
using System.Collections;

public class Player_RoundTable : Photon.MonoBehaviour 
{	
	private CardboardHead head;
	
	private ObjectDraggable obj_Draggable;
	private float dist_Draggable;
	
	private Vector3 correctPlayerPos;
	private Quaternion correctPlayerRot;
	
	void Awake()
	{
		head = Camera.main.GetComponent<StereoController>().Head;
	}
	
	void Start () 
	{
		if(photonView.isMine)
		{
			transform.position = Camera.main.transform.position;
			transform.rotation = Camera.main.transform.rotation;		
			transform.parent = Camera.main.transform.parent;
		}
	}
	
	void Update () 
	{
		if(photonView.isMine)
		{
			if(isDragging())
			{
				if(Input.GetMouseButton(0))
				{
					Vector3 vel = head.Gaze.GetPoint(dist_Draggable) - obj_Draggable.transform.position;
					obj_Draggable.ChangeVelocity(vel * vel.magnitude);
				}
				else if(Input.GetMouseButtonUp(0))
				{
					obj_Draggable = null;
				}
			}
		}
		else
		{
			transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 5);
			transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 5);
		}		
	}
	
	void LateUpdate()
	{
		if(photonView.isMine)
			CheckDraggable();
	}
	
	public bool isDragging()
	{
		return !ObjectDraggable.ReferenceEquals(obj_Draggable, null);
	}
	
	public void CheckDraggable ()
	{
		RaycastHit hit;
		if(Input.GetMouseButtonDown(0))
		{
			if(Physics.Raycast(head.Gaze, out hit, Mathf.Infinity) && 
			   hit.collider.GetComponent<ObjectDraggable>())
			{
				obj_Draggable = hit.collider.GetComponent<ObjectDraggable>();
				dist_Draggable = hit.distance;
			}
		}
	}
	
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (photonView.isMine && stream.isWriting)
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
