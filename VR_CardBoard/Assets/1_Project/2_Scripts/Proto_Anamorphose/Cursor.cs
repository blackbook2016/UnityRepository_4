using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour 
{
	CardboardHead head;
	public float size;

	void Start () 
	{
		head = Camera.main.GetComponent<StereoController>().Head;
	}

	void LateUpdate () 
	{
		RaycastHit hit;
		float dist;

		if(Physics.Raycast(head.Gaze, out hit, Mathf.Infinity))
		{
			dist = hit.distance;
		}
		else
		{
			dist = Camera.main.farClipPlane * 0.95f;
		}

		transform.position = head.transform.position + head.transform.forward * dist;
		transform.LookAt(head.transform.position);
		transform.localScale = Vector3.one * dist * size;
	}
}
