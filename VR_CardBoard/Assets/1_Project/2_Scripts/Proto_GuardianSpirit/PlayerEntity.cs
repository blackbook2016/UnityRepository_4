using UnityEngine;
using System.Collections;

public enum PlayerType
{
	PC,
	VR
}

public abstract class PlayerEntity<T> : MonoBehaviour where T : MonoBehaviour, new()
{	
	public static T Instance
	{
		get
		{
			lock(_lock)
			{
				if (_instance == null)
				{
					_instance = (T) FindObjectOfType(typeof(T));
					
					if ( FindObjectsOfType(typeof(T)).Length > 1 )
					{
						Debug.LogError("[Singleton] Something went really wrong " +
						               " - there should never be more than 1 singleton!" +
						               " Reopening the scene might fix it.");
						return _instance;
					}
					
					if (_instance == null)
					{
						GameObject singleton = new GameObject();
						_instance = singleton.AddComponent<T>();
						singleton.name = "(singleton) "+ typeof(T).ToString();
						
						DontDestroyOnLoad(singleton);
						
						Debug.Log("[Singleton] An instance of " + typeof(T) + 
						          " is needed in the scene, so '" + singleton +
						          "' was created with DontDestroyOnLoad.");
					} 
				}			
				return _instance;	
			}
		}
	}
	
	private static object _lock = new object();
	
	private static T _instance = new T();
	
	public bool isReady;
	public PlayerType type;

	void OnEnable() 
	{
		EventManager_GS.gameInit += Init;
	}
	
	void OnDisable() 
	{
		EventManager_GS.gameInit -= Init;
	}
	
	public abstract void Init ();
}
