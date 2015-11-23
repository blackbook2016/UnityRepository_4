using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager_GS : Singleton<UIManager_GS> 
{
	[SerializeField]
	private GameObject Canvas;
	
	[SerializeField]
	private GameObject panel_MainMenu;
	[SerializeField]
	private Text text_Connection;
	[SerializeField]
	private GameObject  button_Play;
	[SerializeField]
	private Text text_Wait;

	void Awake()
	{
		EnablePanelMainMenu();
	}

	void Update()
	{
		UpdateConnectionText();
	}

	private void EnablePanelMainMenu()
	{	
		Camera.main.cullingMask = 1 << 5;
		Camera.main.clearFlags = CameraClearFlags.SolidColor;

		text_Connection.enabled = true;
		button_Play.SetActive(false);
		text_Wait.enabled = false;
		panel_MainMenu.SetActive(true);
	}

	public void DisablePanelMainMenu()
	{	
		panel_MainMenu.SetActive(false);
		text_Connection.enabled = true;
		button_Play.SetActive(false);
		text_Wait.enabled = false;

		Camera.main.cullingMask = -1;
		Camera.main.clearFlags = CameraClearFlags.Skybox;
	}

	private void UpdateConnectionText()
	{
		text_Connection.text = "Connection Status: " + PhotonNetwork.connectionStateDetailed.ToString();
	}

	public void ButtonReadyEnable()
	{
		button_Play.SetActive(true);
		text_Connection.enabled = false;
	}

	public void ButtonReadyCliked()
	{
		button_Play.GetComponent<Button>().enabled = false;
		text_Wait.enabled = true;

		PhotonManager_GS.Instance.PlayerReady();
	}
}
