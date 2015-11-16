using UnityEngine;
using System.Collections;

public class CameraManager_ANMP : Singleton<CameraManager_ANMP>
{
	private Transform dest_POV;

	[SerializeField]
	private float moveSpeed = 2;	
	private CardboardHead head;
	private bool reachedDest = true;

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
		if(Input.GetMouseButtonDown(0) && reachedDest)
			CheckPOV();
		if(!Vector3.ReferenceEquals(dest_POV, null))
		{
			if(Vector3.Distance(dest_POV.position, transform.position) > 0.05f)
			{
				Vector3 dir = (dest_POV.position- transform.position).normalized;
				transform.position += dir * Time.deltaTime * moveSpeed;
			}
			else
			{				
				reachedDest = true;
				transform.position = dest_POV.position;
			}
		}
	}
	
	private void CheckPOV()
	{
		RaycastHit hit;
		if(Physics.Raycast(head.Gaze, out hit, Mathf.Infinity) && 
		   hit.collider.tag == "POV")			
		{
			if(!Vector3.ReferenceEquals(dest_POV, null))
				dest_POV.GetComponent<POV_ANMP>().StartCoroutine("FadeIn");
			
			hit.collider.GetComponent<POV_ANMP>().StartCoroutine("FadeOut");

			dest_POV = hit.collider.transform;
//			reachedDest = false;
		}		
	}
}
