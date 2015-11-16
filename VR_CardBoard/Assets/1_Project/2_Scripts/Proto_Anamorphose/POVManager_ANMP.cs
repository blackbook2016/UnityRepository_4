using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class POVManager_ANMP : Singleton<POVManager_ANMP> 
{
	private List<Transform> list_POV = new List<Transform>();
	
	[SerializeField]
	private Transform pov_Start;
	
	void Awake()
	{
		foreach(Transform child in transform)
		{
			list_POV.Add(child);
		}
	}
	
	public Transform GetStartPOV()
	{
		return pov_Start;
	}
}
