using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MasterServerUtils : MonoBehaviour
{
	public static string uniqueGameType = "_Unity_Test_Multi";
	public static string uniqueGameName = "VRCardBoard";
	
	public static bool useCustomConfiguration = true;
	public static string customConfigIP = "52.24.84.224";
	public static int customConfigPort = 23466;

	void Awake() 
	{
		RegisterWithMasterServer();
	}

	
	void OnGUI() {
//		print (ListHost().Count + "   " + Network.HavePublicAddress() + "   " + Network.TestConnection() + "  " + Network.connectionTesterIP + "  " + Network.connectionTesterPort);
		//		if (GUILayout.Button ("Start Server"))
		////		if(data.Length ==0)
		//		{
		//			// Use NAT punchthrough if no public IP present
		//			Network.InitializeServer(32, customConfigPort, !Network.HavePublicAddress());
		//			MasterServer.RegisterHost(uniqueGameType, uniqueGameName, "l33t game for all");
		//		}

		
		HostData[] data = MasterServer.PollHostList();
		
		GUILayout.BeginHorizontal();   
		GUILayout.Label ("Network.isClient:   " + Network.isClient);
		GUILayout.Space(5);
		GUILayout.Label ("Network.isServer:   " + Network.isServer);
		GUILayout.Space(5);
		GUILayout.Label ("ListHost:   " + ListHost().Count);
		GUILayout.Space(5);
		GUILayout.Label ("Network.HavePublicAddress:   " + Network.HavePublicAddress());
		GUILayout.Space(5);
		GUILayout.EndHorizontal();  
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

	static void ConfigureMasterServer()
	{
		if (useCustomConfiguration)
		{
			MasterServer.ipAddress = customConfigIP;
			MasterServer.port = customConfigPort;
		}
	}
	
	public static void RequestHostList()
	{
		ConfigureMasterServer();
		MasterServer.RequestHostList(uniqueGameType);
	}
	
	public static List<HostData> ListHost()
	{
		ConfigureMasterServer();
		HostData[] hostData = MasterServer.PollHostList();
		MasterServer.ClearHostList();
		return hostData.ToList();
	}
	
	public static void RegisterWithMasterServer()
	{
		ConfigureMasterServer();

//		bool useNat = !Network.HavePublicAddress();
//		Network.InitializeServer(32, customConfigPort, useNat);

//		MasterServer.RegisterHost(uniqueGameType, uniqueGameName, "l33t game for all");
	}
	
	public static void OnFailedToConnectToMasterServer(NetworkConnectionError info)
	{
		Debug.Log("Unable to connect to master server! - " + info);
	}

	public static void OnFailedToConnect(NetworkConnectionError info)		
	{
		
		Debug.Log(info);
	}
	#region Test2
//
//	string gameName = "You must change this";
//	
//	string serverName = "Joe Blow's Game";
//	
//	string serverTagline = "l33t game for all!";
//	
//	int serverPort = 25002;
//	
//	int numberOfPlayers  = 32;
//	
//	private int playerCount = 0;
//	
//	private float timeoutHostList = 0.0f;
//	
//	private float lastHostListRequest = -1000.0f;
//	
//	private float hostListRefreshTimeout = 10.0f;
//	
//	private ConnectionTesterStatus natCapable = ConnectionTesterStatus.Undetermined;
//	
//	private bool filterNATHosts = false;
//	
//	private bool probingPublicIP = false;
//	
//	private bool doneTesting = false;
//	
//	private float timer = 0.0;
//	
//	private Rect windowRect = Rect (Screen.width-300,0,300,100);
//	
//	private bool hideTest = false;
//	
//	private string testMessage = "Detetcting NAT capabilities...";
//	
//	// Enable this if not running a client on the server machine
//	
//	//MasterServer.dedicatedServer = true;
//	
//	void OnFailedToConnectToMasterServer(NetworkConnectionError info)
//		
//	{
//		
//		Debug.Log(info);
//	}
//	
//	void OnFailedToConnect(NetworkConnectionError info)
//		
//	{
//		
//		Debug.Log(info);
//	}
//	
//	void OnGUI ()
//		
//	{
//		
//		windowRect = GUILayout.Window (0, windowRect, MakeWindow, "Server Controls");
//	}
//	
//	void Awake ()
//		
//	{
//		
//		// Start connection test
//		
//		
//		natCapable = Network.TestConnection();
//		
//		
//		// What kind of IP does this machine have? TestConnection also indicates this in the
//		
//		
//		// test results
//		
//		
//		if (Network.HavePublicAddress())
//			
//			
//			Debug.Log("This machine has a public IP address");
//		
//		else
//			
//			
//			Debug.Log("This machine has a private IP address");
//		
//	}
//	
//	void MakeWindow (int id)
//		
//	{
//		
//		var hideNumberOfPlayers = !Network.isServer;
//		
//		
//		if (!hideNumberOfPlayers) 
//			
//			
//		{
//			
//			
//			GUILayout.Label("Number of player's connected: " + playerCount);
//			
//		}
//		
//		
//		hideTest = GUILayout.Toggle(hideTest, "Hide test info");
//		
//		
//		if (!hideTest) 
//			
//			
//		{
//			
//			
//			GUILayout.Label(testMessage);
//			if (GUILayout.Button ("Retest connection"))
//			{
//				Debug.Log("Redoing connection test");
//				probingPublicIP = false;
//				doneTesting = false;
//				natCapable = Network.TestConnection(true);
//			}
//			
//		}
//		
//		
//		if (Network.peerType == NetworkPeerType.Disconnected)
//			
//			
//		{
//			
//			
//			GUILayout.BeginHorizontal();
//			GUILayout.Space(10);
//			// Start a new server
//			if (GUILayout.Button ("Start Server"))
//			{
//				// Use NAT punchthrough if no public IP present
//				var useNat = !Network.HavePublicAddress();
//				Network.InitializeServer(numberOfPlayers, serverPort, useNat);
//				MasterServer.RegisterHost(gameName, serverName, serverTagline);
//			}
//			// Refresh hosts
//			if (GUILayout.Button ("Refresh available Servers") || Time.realtimeSinceStartup )
//			{
//				MasterServer.RequestHostList (gameName);
//				lastHostListRequest = Time.realtimeSinceStartup;
//			}
//			GUILayout.FlexibleSpace();
//			GUILayout.EndHorizontal();
//			GUILayout.Space(5);
//			HostData[] data = MasterServer.PollHostList();
//			for (var element in data)
//			{
//				GUILayout.BeginHorizontal();
//				// Do not display NAT enabled games if we cannot do NAT punchthrough
//				if ( !(filterNATHosts))//&amp;&amp; element.useNat) )
//				{
//					var name = element.gameName + " " + element.connectedPlayers + " / " + element.playerLimit;
//					GUILayout.Label(name);  
//					GUILayout.Space(5);
//					var hostInfo = "[";
//					// Here we display all IP addresses, there can be multiple in cases where
//					// internal LAN connections are being attempted. In the GUI we could just display
//					// the first one in order not confuse the end user, but internally Unity will
//					// do a connection check on all IP addresses in the element.ip list, and connect to the
//					// first valid one.
//					for (var host in element.ip)
//					{
//						hostInfo = hostInfo + host + ":" + element.port + " ";
//					}
//					hostInfo = hostInfo + "]";
//					//GUILayout.Label("[" + element.ip + ":" + element.port + "]"); 
//					GUILayout.Label(hostInfo);  
//					GUILayout.Space(5);
//					GUILayout.Label(element.comment);
//					GUILayout.Space(5);
//					GUILayout.FlexibleSpace();
//					if (GUILayout.Button("Connect"))
//					{
//						Network.Connect(element.ip, element.port);          
//					}
//				}
//				GUILayout.EndHorizontal();  
//			}
//			
//		}
//		
//		
//		else
//			
//			
//		{
//			
//			
//			if (GUILayout.Button ("Disconnect"))
//			{
//				Network.Disconnect();
//				MasterServer.UnregisterHost();
//			}
//			GUILayout.FlexibleSpace();
//			
//		}
//		
//		
//		GUI.DragWindow (Rect (0,0,1000,1000));
//		
//	}
//	
//	function Update()
//		
//	{
//		
//		// If test is undetermined, keep running
//		
//		
//		if (!doneTesting) 
//			
//			
//		{
//			
//			
//			TestConnection();
//			
//		} 
//		
//	}
//	
//	function TestConnection()
//		
//	{
//		
//		// Start/Poll the connection test, report the results in a label and 
//		
//		
//		// react to the results accordingly
//		
//		
//		connectionTestResult = Network.TestConnection();
//		
//		
//		switch (connectionTestResult) {
//			
//			
//		case ConnectionTesterStatus.Error: 
//			testMessage = "Problem determining NAT capabilities";
//			doneTesting = true;
//			break;
//		case ConnectionTesterStatus.Undetermined: 
//			testMessage = "Undetermined NAT capabilities";
//			doneTesting = false;
//			break;
//		case ConnectionTesterStatus.PublicIPIsConnectable:
//			testMessage = "Directly connectable public IP address.";
//			useNat = false;
//			doneTesting = true;
//			break;
//			// This case is a bit special as we now need to check if we can 
//			// circumvent the blocking by using NAT punchthrough
//		case ConnectionTesterStatus.PublicIPPortBlocked:
//			testMessage = "Non-connectble public IP address (port " + 
//				serverPort +" blocked), running a server is impossible.";
//			useNat = false;
//			// If no NAT punchthrough test has been performed on this public 
//			// IP, force a test
//			if (!probingPublicIP) {
//				connectionTestResult = Network.TestConnectionNAT();
//				probingPublicIP = true;
//				testStatus = "Testing if blocked public IP can be circumvented";
//				timer = Time.time + 10;
//			}
//			// NAT punchthrough test was performed but we still get blocked
//			else if (Time.time &gt; timer) {
//				probingPublicIP = false;         // reset
//				useNat = true;
//				doneTesting = true;
//			}
//			break;
//		case ConnectionTesterStatus.PublicIPNoServerStarted:
//			testMessage = "Public IP address but server not initialized, "+
//				"it must be started to check server accessibility. Restart "+
//					"connection test when ready.";
//			break;
//		case ConnectionTesterStatus.LimitedNATPunchthroughPortRestricted:
//			testMessage = "Limited NAT punchthrough capabilities. Cannot "+
//				"connect to all types of NAT servers.";
//			useNat = true;
//			doneTesting = true;
//			break;
//		case ConnectionTesterStatus.LimitedNATPunchthroughPortRestricted:
//			testMessage = "Limited NAT punchthrough capabilities. Cannot "+
//				"connect to all types of NAT servers. Running a server "+
//					"is ill advised as not everyone can connect.";
//			useNat = true;
//			doneTesting = true;
//			break;
//		case ConnectionTesterStatus.LimitedNATPunchthroughSymmetric:
//			testMessage = "Limited NAT punchthrough capabilities. Cannot "+
//				"connect to all types of NAT servers. Running a server "+
//					"is ill advised as not everyone can connect.";
//			useNat = true;
//			doneTesting = true;
//			break;
//		case ConnectionTesterStatus.NATpunchthroughAddressRestrictedCone:
//		case ConnectionTesterStatus.NATpunchthroughFullCone:
//			testMessage = "NAT punchthrough capable. Can connect to all "+
//				"servers and receive connections from all clients. Enabling "+
//					"NAT punchthrough functionality.";
//			useNat = true;
//			doneTesting = true;
//			break;
//		default: 
//			testMessage = "Error in test routine, got " + connectionTestResult;
//			
//		}
//		
//		
//		if (doneTesting) 
//			
//			
//		{
//			
//			
//			if (useNat)
//				shouldEnableNatMessage = "When starting a server the NAT "+
//					"punchthrough feature should be enabled (useNat parameter)";
//			else
//				shouldEnableNatMessage = "NAT punchthrough not needed";
//			testStatus = "Done testing";
//			
//		}
//		
//	}
//	
//	function OnPlayerConnected(player: NetworkPlayer)
//		
//	{
//		
//		playerCount += 1;
//		
//		
//		Debug.Log("Player " + playerCount + 
//		          
//		          
//		          " connected from " + player.ipAddress + 
//		          ":" + player.port);
//		
//		// Populate a data structure with player information ...
//		
//	}
//	
//	function OnPlayerDisconnected(player: NetworkPlayer)
//		
//	{
//		
//		playerCount -= 1;
//		
//		
//		Debug.Log("Clean up after player " +  player);
//		
//		
//		Network.RemoveRPCs(player);
//		
//		
//		Network.DestroyPlayerObjects(player);
//		
//	}
	#endregion
}

#region Test
//string testStatus = "Testing network connection capabilities.";
//string testMessage = "Test in progress";
//string shouldEnableNatMessage  = "";
//bool doneTesting = false;
//bool probingPublicIP = false;
//ConnectionTesterStatus connectionTestResult = ConnectionTesterStatus.Undetermined;
//float timer =0;
//
//// Indicates if the useNat parameter be enabled when starting a server
//bool useNat = false;
//
////	void OnGUI() {
////		GUILayout.Label("Current Status: " + testStatus);
////		GUILayout.Label("Test result : " + testMessage);
////		GUILayout.Label(shouldEnableNatMessage);
////		if (!doneTesting)
////			TestConnection();
////	}
//
//void TestConnection() {
//	// Start/Poll the connection test, report the results in a label and 
//	// react to the results accordingly
//	connectionTestResult = Network.TestConnection();
//	switch (connectionTestResult) 
//	{
//	case ConnectionTesterStatus.Error: 
//		testMessage = "Problem determining NAT capabilities";
//		doneTesting = true;
//		break;
//		
//	case ConnectionTesterStatus.Undetermined: 
//		testMessage = "Undetermined NAT capabilities";
//		doneTesting = false;
//		break;
//		
//	case ConnectionTesterStatus.PublicIPIsConnectable:
//		testMessage = "Directly connectable public IP address.";
//		useNat = false;
//		doneTesting = true;
//		break;
//		
//		// This case is a bit special as we now need to check if we can 
//		// circumvent the blocking by using NAT punchthrough
//	case ConnectionTesterStatus.PublicIPPortBlocked:
//		testMessage = "Non-connectable public IP address (port " +
//			customConfigPort +" blocked), running a server is impossible.";
//		useNat = false;
//		// If no NAT punchthrough test has been performed on this public 
//		// IP, force a test
//		if (!probingPublicIP) {
//			connectionTestResult = Network.TestConnectionNAT();
//			probingPublicIP = true;
//			testStatus = "Testing if blocked public IP can be circumvented";
//			timer = Time.time + 10;
//		}
//		// NAT punchthrough test was performed but we still get blocked
//		else if (Time.time > timer) {
//			probingPublicIP = false; 		// reset
//			useNat = true;
//			doneTesting = true;
//		}
//		break;
//	case ConnectionTesterStatus.PublicIPNoServerStarted:
//		testMessage = "Public IP address but server not initialized, "+
//			"it must be started to check server accessibility. Restart "+
//				"connection test when ready.";
//		break;
//		
//	case ConnectionTesterStatus.LimitedNATPunchthroughPortRestricted:
//		testMessage = "Limited NAT punchthrough capabilities. Cannot "+
//			"connect to all types of NAT servers. Running a server "+
//				"is ill advised as not everyone can connect.";
//		useNat = true;
//		doneTesting = true;
//		break;
//		
//	case ConnectionTesterStatus.LimitedNATPunchthroughSymmetric:
//		testMessage = "Limited NAT punchthrough capabilities. Cannot "+
//			"connect to all types of NAT servers. Running a server "+
//				"is ill advised as not everyone can connect.";
//		useNat = true;
//		doneTesting = true;
//		break;
//		
//	case ConnectionTesterStatus.NATpunchthroughAddressRestrictedCone:
//		break;
//	case ConnectionTesterStatus.NATpunchthroughFullCone:
//		testMessage = "NAT punchthrough capable. Can connect to all "+
//			"servers and receive connections from all clients. Enabling "+
//				"NAT punchthrough functionality.";
//		useNat = true;
//		doneTesting = true;
//		break;
//		
//	default: 
//		testMessage = "Error in test routine, got " + connectionTestResult;
//		break;
//	}
//	if (doneTesting) {
//		if (useNat)
//			shouldEnableNatMessage = "When starting a server the NAT "+
//				"punchthrough feature should be enabled (useNat parameter)";
//		else
//			shouldEnableNatMessage = "NAT punchthrough not needed";
//		testStatus = "Done testing";
//	}
//}
#endregion