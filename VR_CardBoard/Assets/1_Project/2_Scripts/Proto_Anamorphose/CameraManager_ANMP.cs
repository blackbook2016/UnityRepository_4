using UnityEngine;
using System.Collections;

public class CameraManager_ANMP : Singleton<CameraManager_ANMP>
{
	private Transform dest_POV;
	
	[SerializeField]
	private float moveSpeed = 1;	
	private CardboardHead head;

	private ObjectDraggable_ANMP obj_Draggable;
	private float dist_Draggable;
	
	void Awake()
	{
		head = Camera.main.GetComponent<StereoController>().Head;
	}
	
	void Start () 
	{
		dest_POV = POVManager_ANMP.Instance.GetStartPOV();
		transform.position = dest_POV.position;
	}
	
	void Update () 
	{
		if(!Vector3.ReferenceEquals(dest_POV, null))
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
				UIManager_ANMP.Instance.SetCursorColor(Color.white);
			}
		}
	}

	private void moveToDestPov()
	{
		if(Vector3.Distance(dest_POV.position, transform.position) > 0.05f)
		{
			Vector3 dir = (dest_POV.position- transform.position).normalized;
			transform.position += dir * Time.deltaTime * moveSpeed;
		}
		else
			if(transform.position != dest_POV.position)
		{
			transform.position = dest_POV.position;
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
		UIManager_ANMP.Instance.SetCursorColor(Color.blue);
	}

	public bool isDragging()
	{
		return !ObjectDraggable_ANMP.ReferenceEquals(obj_Draggable, null);
	}
}

