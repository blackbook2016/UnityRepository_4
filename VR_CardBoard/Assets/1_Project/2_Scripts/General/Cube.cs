using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class Cube : MonoBehaviour 
{
	Rigidbody rb;
	Vector3 init_pos;
	Quaternion init_rot;
	
	void Start () 
	{
		rb = GetComponent<Rigidbody>();	
		init_pos = transform.position;
		init_rot = transform.rotation;
	}
	
	void OnEnable() 
	{
		EventManager.gameReset += Reset;
	}
	
	void OnDisable() 
	{
		EventManager.gameReset -= Reset;
	}
	
	void Reset()
	{
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		
		transform.position = init_pos;
		transform.rotation = init_rot;
	}
}
