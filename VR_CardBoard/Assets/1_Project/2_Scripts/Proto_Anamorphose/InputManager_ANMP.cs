using UnityEngine;
using System.Collections;

public enum Target
{
	None,
	POV,
	Draggable
}

public class InputManager_ANMP : Singleton<InputManager_ANMP> 
{
	private CardboardHead head;

	public Target targetInSight;
	private RaycastHit hit;

	#region Unity

	void Awake()
	{
		head = Camera.main.GetComponent<StereoController>().Head;
	}

	void Start()
	{
		targetInSight = Target.None;
	}

	void LateUpdate () 
	{
		CheckTargetInSight();
		UpdateTargetInSight();
	}
	#endregion

	#region TargeStateMachine

	private void CheckTargetInSight()
	{
		if(Physics.SphereCast(head.Gaze, 0.1f, out hit, Mathf.Infinity))
		{
			if(hit.collider.tag == "POV" && hit.collider.GetComponent<POV_ANMP>().IsReachable())
			{
				if(targetInSight != Target.POV)
					ChangeTargetInSight(Target.POV);
			}
			else if(targetInSight == Target.POV)
			{
				ChangeTargetInSight(Target.None);
			}

			if(hit.collider.tag == "Drag")
			{
				if(targetInSight != Target.Draggable)
					ChangeTargetInSight(Target.Draggable);
			}
			else if(targetInSight == Target.Draggable)
			{
				ChangeTargetInSight(Target.None);
			}

		}
		else if(targetInSight != Target.None)
		{
			ChangeTargetInSight(Target.None);
		}
	}

	private void UpdateTargetInSight()
	{
		switch(targetInSight)
		{
		case Target.None:
			break;

		case Target.POV:
			if(!UIManager_ANMP.Instance.FillCursor())
				CameraManager_ANMP.Instance.SetDestPov(hit.collider.GetComponent<POV_ANMP>());
			break;

		case Target.Draggable:
			CameraManager_ANMP.Instance.CheckDraggable();
			break;
		}
	}

	private void ChangeTargetInSight(Target tg)
	{
		if(targetInSight != tg)
		{
			switch(targetInSight)
			{
			case Target.None:
				break;

			case Target.POV:
				UIManager_ANMP.Instance.ClearCursor();
				break;

			case Target.Draggable:
				if(!CameraManager_ANMP.Instance.isDragging())
					UIManager_ANMP.Instance.SetCursorColor(Color.white);
				break;
			}
		}
		targetInSight = tg;
	}
	#endregion
}
