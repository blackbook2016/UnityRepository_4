using UnityEngine;
using System.Collections;

public class Player_CB_IMGT : MonoBehaviour 
{
	public static Player_CB_IMGT Instance {
		get {
			if (instance == null) {
				instance = UnityEngine.Object.FindObjectOfType<Player_CB_IMGT>();
			}
			if (instance == null) {
				var go = new GameObject("Cardboard");
				instance = go.AddComponent<Player_CB_IMGT>();
				go.transform.localPosition = Vector3.zero;
			}
			return instance;
		}
	}

	private static Player_CB_IMGT instance = null;

	private PhotonView photonView;
	
	public void Start()
	{				
		photonView = PhotonView.Get(this);
	}

	public void RPCUpdateTrs(Quaternion rot)
	{
		photonView.RPC("UpdateTrs", PhotonTargets.All, rot);
	}
	
	[PunRPC]
	public void UpdateTrs(Quaternion rot)
	{
		transform.rotation = rot;
	}
}
