using UnityEngine;
using System.Collections;

public class ObjectDraggable : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Change();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Space))
			Change();
	}

	public void Change()
	{
		GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0.0f, 1.1f), Random.Range(0.0f, 1.1f), Random.Range(0.0f, 1.1f));
	}
}
