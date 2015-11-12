using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager_IMGT : Singleton<UIManager_IMGT> 
{
	[SerializeField]
	private GameObject panel_EndGame;
	[SerializeField]
	private GameObject panel_FloorMenu;
	[SerializeField]
	private GameObject eventSystem_CB;
	[SerializeField]
	private GameObject eventSystem;
	[SerializeField]
	private Text text_GameOver;


	void Awake()
	{
		panel_EndGame.SetActive(false);
	}

	public void LoadUI()
	{
		#if UNITY_ANDROID
			eventSystem.SetActive(false);
			panel_FloorMenu.SetActive(true);
			eventSystem_CB.SetActive(true);
		#elif UNITY_STANDALONE
			panel_FloorMenu.SetActive(false);
			eventSystem_CB.SetActive(false);
			eventSystem.SetActive(true);
		#endif
	}
	
	void OnEnable()
	{
		EventManager_IMGT.gameOver += GameOver;
		EventManager_IMGT.gameReset += Reset;
	}
	
	void OnDisable()
	{
		EventManager_IMGT.gameOver -= GameOver;
		EventManager_IMGT.gameReset += Reset;
	}
	
	void GameOver(bool status)
	{
		text_GameOver.text = status ? "VICTORY!!!" : "DEFEAT!!!";
		panel_EndGame.SetActive(true);
	}

	void Reset()
	{
		panel_EndGame.SetActive(false);
	}
}
