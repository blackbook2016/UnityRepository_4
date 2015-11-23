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
		Color c = new Color();		
		if(mr.material.shader.name != "Particles/Additive")
		{
			c = mr.material.color;
		}
		else
		{
			c = mr.material.GetColor("_TintColor");
		}
		
		while(c.a < 1)
		{
			if(mr.material.shader.name != "Particles/Additive")
			{
				mr.material.color = c;
			}
			else
			{
				mr.material.SetColor("_TintColor", c);
			}
			
			yield return null;
			
			c.a += Time.deltaTime * fadeSpeed;
		}
		
		c.a = 1;			

		if(mr.material.shader.name != "Particles/Additive")
		{
			mr.material.color = c;
		}
		else
		{
			mr.material.SetColor("_TintColor", c);
		}
	}
	
	public IEnumerator FadeOut()
	{
		isReachable = false;

		Color c;

		if(mr.material.shader.name != "Particles/Additive")
		{
			c = mr.material.color;
		}
		else
		{
			c = mr.material.GetColor("_TintColor");
		}
		
		while(c.a > 0)
		{
			if(mr.material.shader.name != "Particles/Additive")
			{
				mr.material.color = c;
			}
			else
			{
				mr.material.SetColor("_TintColor", c);
			}
			yield return null;
			
			c.a -= Time.deltaTime * fadeSpeed;
		}
		
		c.a = 0;			
		if(mr.material.shader.name != "Particles/Additive")
		{
			mr.material.color = c;
		}
		else
		{
			mr.material.SetColor("_TintColor", c);
		}
	}
}
