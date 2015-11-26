using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using System;
using System.Collections;
using System.Collections.Generic;

public class UIManager_Office : MonoBehaviour 
{
	public enum KeyStatus
	{
		Normal,
		Pressed,
		Clicked
	}
	
	[SerializeField]
	private Text text_Status;
	[SerializeField]
	private GameObject[] buttons;
	
	PhotonView photonView;

	List<int> keys_pressed = new List<int>();
	
	void Start () 
	{
		photonView = PhotonView.Get(this);
	}
	
	void Update () 
	{
		if(Input.anyKeyDown)
		{
			int key = (int)detectPressedKeyOrButton().Value;
			if(key != null)
			{
				photonView.RPC("ShowStatus", PhotonTargets.All, key, (int)KeyStatus.Pressed);

				if(!keys_pressed.Contains(key))
					keys_pressed.Add(key);
			}
		}
		if(keys_pressed.Count !=0)
		{
			foreach(var key in keys_pressed)
			{
				if(Input.GetKeyUp((KeyCode)key))
				{
					photonView.RPC("ShowStatus", PhotonTargets.All, key, (int)KeyStatus.Normal);
//					keys_pressed.Remove(key);
				}
			}
		}
	}
	
	
	
	public void UIButtonDown(int key)
	{
		photonView.RPC("ShowStatus", PhotonTargets.All, key, (int)KeyStatus.Clicked);
	}
	
	[PunRPC]
	public void ShowStatus(int key, int key_Status)
	{
		if((KeyStatus)key_Status != KeyStatus.Normal)
			text_Status.text  = "The Last Key Pressed is " + (KeyCode)key;
		
		if(key == 32)			
		{
			SetButtonStatus(0, (KeyStatus)key_Status);
		}
		else if(key == 13)
		{
			SetButtonStatus(1, (KeyStatus)key_Status);
		}
	}

	public void SetButtonStatus(int index, KeyStatus key_Status)
	{		
		var pointer = new PointerEventData(EventSystem.current);

		switch(key_Status)
		{
		case KeyStatus.Normal:
			ExecuteEvents.Execute(buttons[index].gameObject, pointer, ExecuteEvents.pointerUpHandler);
			break;
		case KeyStatus.Clicked:
			ExecuteEvents.Execute(buttons[index].gameObject, pointer, ExecuteEvents.pointerEnterHandler);
			ExecuteEvents.Execute(buttons[index].gameObject, pointer, ExecuteEvents.pointerDownHandler);
			ExecuteEvents.Execute(buttons[index].gameObject, pointer, ExecuteEvents.pointerUpHandler);
			break;
		case KeyStatus.Pressed:
			ExecuteEvents.Execute(buttons[index].gameObject, pointer, ExecuteEvents.pointerEnterHandler);
			ExecuteEvents.Execute(buttons[index].gameObject, pointer, ExecuteEvents.pointerDownHandler);
			break;
		}
	}
	
	public KeyCode? detectPressedKeyOrButton()
	{
		foreach(KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
		{
			if (Input.GetKeyDown(kcode))
			{
//				Debug.Log("KeyCode down: " + kcode + "/" + (int)kcode);
				return kcode;
			}
		}
		return null;
	}
}
