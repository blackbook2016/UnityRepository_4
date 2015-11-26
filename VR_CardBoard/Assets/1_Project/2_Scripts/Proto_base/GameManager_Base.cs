using UnityEngine;
using System.Collections;

public enum NetworkType
{
	LAN,
	Photon
}

public class GameManager_Base : MonoBehaviour 
{
	public NetworkType Networktype;

	[SerializeField]
	public static NetworkType networktype;

	void Awake()
	{
		networktype = Networktype;
	}
}
