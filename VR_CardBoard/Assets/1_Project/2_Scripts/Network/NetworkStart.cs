using UnityEngine;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.VR;
public class NetworkStart : MonoBehaviour 
{
	public GameObject cardboardMain;
	public string hostIP = "10.0.1.14";

	void Awake() {
//		VRSettings.enabled = false;
//		#if (UNITY_ANDROID || UNITY_IPHONE)
//		cardboardMain.SetActive (true);
//		NetworkManager net = GetComponent<NetworkManager> ();
//		net.networkAddress = hostIP;
//		net.networkPort = 10002;
//		net.StartClient ();
//		#else
//		cardboardMain.SetActive (false);
//		#endif
		Network.natFacilitatorIP = "172.31.23.97";
		Network.natFacilitatorPort = 50005;
		MasterServer.ipAddress = "52.24.84.224";
		MasterServer.port = 23466;
		MasterServer.RequestHostList("Dat game");
	}
	void OnFailedToConnectToMasterServer(NetworkConnectionError info) {
		Debug.Log("Could not connect to master server: " + info);
	}
	void OnGUI() {
		if (GUILayout.Button("Start Server")) 
		{
			bool useNat = !Network.HavePublicAddress();
			Network.InitializeServer(32, 50005, useNat);
			MasterServer.RegisterHost("multi", "Dat game");
		}
		
		HostData[] data = MasterServer.PollHostList();
		// Go through all the hosts in the host list
		foreach (var element in data)
		{
			GUILayout.BeginHorizontal();    
			var name = element.gameName + " " + element.connectedPlayers + " / " + element.playerLimit;
			GUILayout.Label(name);  
			GUILayout.Space(5);
			string hostInfo;
			hostInfo = "[";
			foreach (var host in element.ip)
				hostInfo = hostInfo + host + ":" + element.port + " ";
			hostInfo = hostInfo + "]";
			GUILayout.Label(hostInfo);  
			GUILayout.Space(5);
			GUILayout.Label(element.comment);
			GUILayout.Space(5);
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Connect"))
			{
				// Connect to HostData struct, internally the correct method is used (GUID when using NAT).
				Network.Connect(element);           
			}
			GUILayout.EndHorizontal();  
		}
	}
}