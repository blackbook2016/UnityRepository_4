using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class InputManager_MT : MonoBehaviour 
{
	Pawn pawn = new Pawn();
	Triangle m;
	bool initialized = false;
	public bool moyenne = false;
	
	public struct Triangle
	{
		public int id;
		public Vector2[] points;
		public float a;
		public float b;
		public float c;
		public double[] angles;
		public int aire;
		public int airep;
		public int p; //perimeter
		public int m;

		public void Set(Triangle t)
		{
			id = t.id;
			a = t.a;
			b =  t.b;
			c =  t.c;
			
			angles = new double[3]{t.angles[0], t.angles[1] ,t.angles[2]};
			aire =  t.aire;
			airep =  t.airep;
			p =  t.p;
			m = 1;
		}

		public void Add(Triangle t)
		{
			m++;
			id = t.id;
			a += t.a;
			b += t.b;
			c += t.c;
			angles[0] += t.angles[0];
			angles[1] += t.angles[1];
			angles[2] += t.angles[2];
			aire += t.aire;
			airep += t.airep;
			p += t.p;
		}

//		public override bool Equals(Triangle t)
//		{
//			bool finished = false;
//
//			while(!finished)
//			{
//				for(int i = 0; i < 3; i++)
//				{
//					for(int y = 0; y < 3; y++)
//					{
//						if(t[i] == )
//					}
//				}
//			}
//		}
	}
	
	void OnGUI()
	{
		if(Input.touches.Length >= 3)
		{
			GUILayout.Label("ObjectRecognized");
			
			List<Triangle> list_trianglesTemp = new List<Triangle>();
			
			for(int i = 0; i + 2 < Input.touches.Length; i++)
			{
				Triangle tr = new Triangle();
				tr.points = new Vector2[3]{Input.touches[i].position, 
					Input.touches[i + 1].position,
					Input.touches[i + 2].position };
				
				tr.a = Vector2.Distance(tr.points[1], tr.points[2]); 
				tr.b = Vector2.Distance(tr.points[0], tr.points[2]); 
				tr.c = Vector2.Distance(tr.points[0], tr.points[1]); 

				float alpha = RadianToDegree(Mathf.Acos(((tr.b * tr.b) + (tr.c * tr.c) - (tr.a * tr.a)) / (2 * tr.b * tr.c)));
				float beta = RadianToDegree(Mathf.Acos(((tr.a * tr.a) + (tr.c * tr.c) - (tr.b * tr.b)) / (2 * tr.a * tr.c)));
				float gamma = RadianToDegree(Mathf.Acos(((tr.a * tr.a) + (tr.b * tr.b) - (tr.c * tr.c)) / (2 * tr.a * tr.b)));

				tr.angles = new double[3]{alpha, beta ,gamma};
				tr.p = (int)(tr.a + tr.b + tr.c);
				tr.aire = (int)(0.5f * tr.b * tr.c * Mathf.Sin(alpha));
				tr.airep = (int)(Mathf.Sqrt(tr.p * (tr.p - tr.a) * (tr.p - tr.b) * (tr.p - tr.c)));
				tr.id = i;
				list_trianglesTemp.Add (tr);
			}

			if(!initialized)
			{
					m.Set(list_trianglesTemp[0]);
				initialized = true;
			}
			else
				m.Add(list_trianglesTemp[0]);

				foreach(var t in list_trianglesTemp)
			{
				if(moyenne)
				{
				GUILayout.Label(m.id + "  " + 
				                (int)m.angles[0] / m.m + "  " + 
				                (int)m.angles[1] / m.m + "  " + 
				                (int)m.angles[2] / m.m + "  " + 
				                m.p / m.m+ "  " + 
				                m.aire / m.m + "  " + 
				                m.airep / m.m + "  " +
				                m.a / m.m + "  " +
				                m.b / m.m + "  " +
				                m.c / m.m);
				}
				else
				{
					GUILayout.Label(t.id + " : " + 
					               t.angles[0] + " / " + 
					               t.angles[1] + " / " + 
					               t.angles[2] + " : " + 
					                (int)(t.angles[2] + t.angles[1] + t.angles[0]) + " : " + 
					                t.p+ " : " + 
					                t.aire + " : " + 
					                t.airep + " : " +
					                t.a + " : " +
					                t.b + " : " +
					                t.c);
				}
				GUILayout.Space(5);
			}		
		}
	}
	
	private float RadianToDegree(float angle)
	{
		return (float)(angle * (180.0f / Math.PI));
	}	
}