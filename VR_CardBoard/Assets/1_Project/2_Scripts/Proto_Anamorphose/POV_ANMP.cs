using UnityEngine;
using System.Collections;

public class POV_ANMP : MonoBehaviour 
{
	private MeshRenderer mr;
	private float fadeSpeed = 1f;
	
	void Start () 
	{
		mr = GetComponent<MeshRenderer>();
	}
	
	public IEnumerator FadeIn()
	{
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
