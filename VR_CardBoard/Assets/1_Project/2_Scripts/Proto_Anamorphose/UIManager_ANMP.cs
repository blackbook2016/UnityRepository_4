using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager_ANMP : Singleton<UIManager_ANMP> 
{
	[SerializeField]
	private Transform trs_Panel_FloorMenu;
	[SerializeField]
	private CardboardHead head;

	void Update () 
	{
		Vector3 pos = head.transform.position;
		pos.y -= 1f;
		trs_Panel_FloorMenu.position = pos;
	}
}
