using UnityEngine;
using System.Collections;

public class POV_ANMP : MonoBehaviour 
{
	private MeshRenderer mr;
	[SerializeField]
	private float fadeSpeed = 1f;

	private bool isReachable = true;
	
	void Start () 
	{
		mr = GetComponent<MeshRenderer>();
	}

	public bool IsReachable()
	{
		return isReachable;
	}
	
	public IEnumerator FadeIn()
	{
		isReachable = true;

		Color c = mr.material.color;
		
		while(c.a < 1)
		{
			mr.material.color = c;
			
			yield return null;
			
			c.a += Time.deltaTime * fadeSpeed;
		}
		
		c.a = 1;
		mr.material.color = c;
	}
	
	public IEnumerator FadeOut()
	{
		isReachable = false;

		Color c = mr.material.color;
		
		while(c.a > 0)
		{
			mr.material.color = c;
			yield return null;
			
			c.a -= Time.deltaTime * fadeSpeed;
		}
		
		c.a = 0;
		mr.material.color = c;
	}
}
