using UnityEngine;
using System.Collections;


public class Player_Border : Photon.MonoBehaviour
{
	public enum PlType
	{
		Guard = 0,
		Fugitive = 1
	}

	public PlType type;

	private Transform dest_POV;
	
	[SerializeField]
	private float moveSpeed = 1;	
	private CardboardHead head;
	
	private ObjectDraggable_ANMP obj_Draggable;
	private float dist_Draggable;

	private Vector3 correctPlayerPos;
	private Quaternion correctPlayerRot;
	
	void Awake()
	{
		head = Camera.main.GetComponent<StereoController>().Head;
	}
	
	public void Init (PlType type) 
	{
		this.type = type;

		transform.position = Camera.main.transform.position;
		transform.rotation = Camera.main.transform.rotation;
		transform.parent = Camera.main.transform.parent;

		if(type == PlType.Fugitive)
		{
			dest_POV = POVManager_ANMP.Instance.GetStartPOV();
			Cardboard.SDK.transform.position = dest_POV.position;		
		}
	}
	
	void Update () 
	{
		if(photonView.isMine)
		{
			if( !Vector3.ReferenceEquals(dest_POV, null))//type == PlType.Fugitive &&
			{
				moveToDestPov();
			}

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
					UIManager_Border.Instance.SetCursorColor(Color.white);
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
		{
			CheckDraggable();
		}
	}

	private void moveToDestPov()
	{
		if(Vector3.Distance(dest_POV.position, Cardboard.SDK.transform.position) > 0.05f)
		{
			Vector3 dir = (dest_POV.position- Cardboard.SDK.transform.position).normalized;
			Cardboard.SDK.transform.position += dir * Time.deltaTime * moveSpeed;
		}
		else
			if(Cardboard.SDK.transform.position != dest_POV.position)
		{
			Cardboard.SDK.transform.position = dest_POV.position;
		}
	}
	
	public void SetDestPov(POV_ANMP pov)
	{
		if(!Vector3.ReferenceEquals(dest_POV, null))
			dest_POV.GetComponent<POV_ANMP>().StartCoroutine("FadeIn");
		
		pov.StartCoroutine("FadeOut");
		
		dest_POV = pov.transform;
	}
	
	public void CheckDraggable ()
	{
		RaycastHit hit;
		if(Input.GetMouseButtonDown(0))
		{
			if(Physics.Raycast(head.Gaze, out hit, Mathf.Infinity) && 
			   hit.collider.GetComponent<ObjectDraggable_ANMP>())
			{
				obj_Draggable = hit.collider.GetComponent<ObjectDraggable_ANMP>();
				dist_Draggable = hit.distance;
			}
		}
	}
	
	public bool isDragging()
	{
		return !ObjectDraggable_ANMP.ReferenceEquals(obj_Draggable, null);
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

