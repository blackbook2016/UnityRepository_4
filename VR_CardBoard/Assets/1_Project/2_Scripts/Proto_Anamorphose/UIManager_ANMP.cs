using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager_ANMP : Singleton<UIManager_ANMP> 
{
	[Header("UI ")]
	[SerializeField]
	private Transform trs_Panel_FloorMenu;
	[SerializeField]
	private Transform trs_Panel_HUD_R;
	[SerializeField]
	private Transform trs_Cam_Eye_R;
	[SerializeField]
	private Transform trs_Panel_HUD_L;
	[SerializeField]
	private Transform trs_Cam_Eye_L;
	
	private CardboardHead head;
	
	
	[Header("Cursor")]
	[SerializeField]
	private float distHUD = 1f;
	[SerializeField]
	private Image img_Cursor_R;
	[SerializeField]
	private Image img_Cursor_L;
	[SerializeField]
	private Image img_Fill_R;
	[SerializeField]
	private Image img_Fill_L;
	[SerializeField]
	private float fill_Speed = 1f;
	
	[SerializeField]
	private Texture img;	
	
	void OnGUI()
	{
		//		GUI.DrawTexture(new Rect(new Vector2((Screen.width/4) - (img.width / 2),(Screen.height / 2)- (img.height / 2)), new Vector2(img.width, img.height)),img);
		//		GUI.DrawTexture(new Rect(new Vector2((Screen.width * 3 / 4) - (img.width / 2),(Screen.height / 2)- (img.height / 2)), new Vector2(img.width, img.height)),img);
		//		GUI.DrawTexture(new Rect(trs_Cam_Eye_R.GetComponent<Camera>().ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0)), new Vector2(img.width, img.height)),img);
		//		GUI.DrawTexture(new Rect(trs_Cam_Eye_L.GetComponent<Camera>().ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0)), new Vector2(img.width, img.height)),img);
		//		GUI.DrawTexture(new Rect(trs_Cam_Eye_R.GetComponent<Camera>().rect.center, new Vector2(img.width * 2, img.height * 2)),img);
		//		GUI.DrawTexture(new Rect(trs_Cam_Eye_L.GetComponent<Camera>().rect.center, new Vector2(img.width * 2, img.height * 2)),img);
	}
	
	void Start()
	{		
		head = Camera.main.GetComponent<StereoController>().Head;
	}
	
	void LateUpdate () 
	{
		Vector3 pos = head.transform.position;
		pos.y -= 0.5f;
		trs_Panel_FloorMenu.position = pos;
		
		UpdatePanelHUD();
	}
	
	private void UpdatePanelHUD()
	{
		Vector3 pos = trs_Cam_Eye_R.position;
		pos += 100 * trs_Cam_Eye_R.forward.normalized;
		
		trs_Panel_HUD_R.position = pos;
		trs_Panel_HUD_R.rotation = trs_Cam_Eye_R.rotation;
		
		pos = trs_Cam_Eye_L.position;
		pos += 100 * trs_Cam_Eye_L.forward.normalized;
		trs_Panel_HUD_L.position = pos;
		trs_Panel_HUD_L.rotation = trs_Cam_Eye_L.rotation;
	}
	
	public bool FillCursor()
	{
		
		float sizeMax = img_Cursor_R.transform.localScale.x;
		float size = img_Fill_R.transform.localScale.x;
		
		size += Time.deltaTime * 0.1f; 
		
		if(size < sizeMax)
		{
			img_Fill_R.transform.localScale = img_Fill_L.transform.localScale = Vector3.one * size;
			return true;
		}
		else
		{
			return false;
			img_Fill_R.transform.localScale = img_Fill_L.transform.localScale = Vector3.one * sizeMax;
		}
	}
	
	public void ClearCursor()
	{
		img_Fill_R.transform.localScale = img_Fill_L.transform.localScale = Vector3.zero;		
	}

	public void SetCursorColor(Color col)
	{
		img_Cursor_R.color = img_Cursor_L.color = col;
	}
	
}
