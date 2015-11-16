using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager_ANMP : Singleton<UIManager_ANMP> 
{
	[SerializeField]
	private Transform trs_Panel_FloorMenu;
	[SerializeField]
	private Transform trs_Panel_Overlay;

	private CardboardHead head;

	void Start()
	{		
		head = Camera.main.GetComponent<StereoController>().Head;
	}

	void Update () 
	{
		Vector3 pos = head.transform.position;
		pos.y -= 0.5f;
		trs_Panel_FloorMenu.position = pos;
	}

	public void UpdatePanelOverlay()
	{
//		Vector3 pos = head.transform.position;
//		pos += head.transform.forward * 3;
//		trs_Panel_Overlay.position = pos;
//		trs_Panel_Overlay.rotation = head.transform.rotation;
	}
}
