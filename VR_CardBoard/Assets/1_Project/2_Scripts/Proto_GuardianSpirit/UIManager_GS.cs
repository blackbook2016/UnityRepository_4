using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager_GS : Singleton<UIManager_GS> 
{
	[Header("Canvas")]
	[SerializeField]
	private GameObject Canvas_Overlay;
	[SerializeField]
	private GameObject Canvas_World;

	private GameObject canvas_current;

	[Header("Panel_MainMenu")]
	[SerializeField]
	private GameObject panel_MainMenu;
	[SerializeField]
	private Text text_Connection;
	[SerializeField]
	private GameObject button_Play;
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
		#if UNITY_ANDROID
		Canvas_Overlay.SetActive(false);
		Canvas_World.SetActive(true);
		canvas_current = Canvas_World;

		panel_MainMenu = canvas_current.transform.GetChild(0).gameObject;
		text_Connection = panel_MainMenu.transform.FindChild("Text_Connection").GetComponent<Text>();
		button_Play = panel_MainMenu.transform.FindChild("Button_Ready").gameObject;
		text_Wait = panel_MainMenu.transform.FindChild("Text_Wait").GetComponent<Text>();

		Camera.main.cullingMask = 1 << 5;
		Camera.main.clearFlags = CameraClearFlags.SolidColor;
		
		text_Connection.enabled = true;
		button_Play.SetActive(false);
		text_Wait.enabled = false;
		panel_MainMenu.SetActive(true);

		#elif UNITY_STANDALONE
		Canvas_World.SetActive(false);
		Canvas_Overlay.SetActive(true);
		canvas_current = Canvas_Overlay;

		Camera.main.cullingMask = 1 << 5;
		Camera.main.clearFlags = CameraClearFlags.SolidColor;
		
		text_Connection.enabled = true;
		button_Play.SetActive(false);
		text_Wait.enabled = false;
		panel_MainMenu.SetActive(true);
		#endif
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
		print ("Clicked");
	}
}
