using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class ObjectDraggable_ANMP : MonoBehaviour 
{
	Rigidbody rb;
	Vector3 init_pos;
	Quaternion init_rot;
	Material mat;
	
	void Start () 
	{
		mat = GetComponent<MeshRenderer>().material;
		rb = GetComponent<Rigidbody>();	
		init_pos = transform.position;
		init_rot = transform.rotation;
	}
	
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Space))
			Change();
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
	
	public void Change()
	{
		Vector3 col = new Vector3(Random.Range(0.0f, 1.1f), Random.Range(0.0f, 1.1f), Random.Range(0.0f, 1.1f));
		mat.color = new Color( col.x, col.y, col.z );
	}
	
	public void ChangeVelocity(Vector3 vel)
	{
		rb.velocity = vel;
//		transform.position += vel;
	}
}
