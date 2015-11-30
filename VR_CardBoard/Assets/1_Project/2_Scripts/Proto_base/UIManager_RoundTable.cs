using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager_RoundTable : Singleton<UIManager_RoundTable> 
{
	[SerializeField]
	private Transform panel_Floor;

	public void EnablePanelFloor()
	{
	}

	public void SetPanelFloor(Transform player)
	{
		Vector3 pos = player.position;
		pos.y = panel_Floor.position.y;
		panel_Floor.position = pos;
		panel_Floor.rotation = player.rotation;
		Vector3 eul = panel_Floor.eulerAngles;
		eul.x = 90;
		panel_Floor.eulerAngles = eul;
	}
}
