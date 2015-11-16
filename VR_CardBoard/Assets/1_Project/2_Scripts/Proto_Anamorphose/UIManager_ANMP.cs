using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager_ANMP : Singleton<UIManager_ANMP> 
{
	[SerializeField]
	private Transform trs_Panel_FloorMenu;

	private CardboardHead head;

	void Start()
	{		
		head = Camera.main.GetComponent<StereoController>().Head;
	}

	void Update () 
	{
		Vector3 pos = head.transform.position;
		pos.y -= 1f;
		trs_Panel_FloorMenu.position = pos;
	}
}
