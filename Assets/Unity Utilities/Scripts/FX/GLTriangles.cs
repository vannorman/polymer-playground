// ********** //
// This script is for drawing CIRLCE BASED PROPORTIONS FOR FRACTIONS.
// it is NOT a generic triangle drawer.
//

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GLTriangles : MonoBehaviour
{
	static Material lineMaterial;

	static void CreateLineMaterial ()
	{
		if (!lineMaterial)
		{
			// Unity has a built-in shader that is useful for drawing
			// simple colored things.
			//			var shader = Shader.Find ("Hidden/Internal-Colored");
			var shader = Shader.Find ("Catlike Coding/Text Box/Alpha Blend Z for digits");
			lineMaterial = new Material (shader);
			lineMaterial.hideFlags = HideFlags.HideAndDontSave;
			// Turn on alpha blending
			lineMaterial.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			lineMaterial.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			// Turn backface culling off
			lineMaterial.SetInt ("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
			// Turn off depth writes
			lineMaterial.SetInt ("_ZWrite", 0);
		}
	}
	
//	// Will be called after all regular rendering is done
//	public void OnRenderObject ()
//	{
//
////		CreateLineMaterial ();
////		DrawProportionalCircles();
////		DrawLines();
////
//	}
	

	
	public static void DrawTriangle(Vector3[] pos, Color col){
		CreateLineMaterial ();
		
		lineMaterial.SetPass (0);
		GL.PushMatrix ();
		GL.Begin (GL.TRIANGLES);
		GL.Color(col);
		GL.Vertex3(pos[0].x,pos[0].y,pos[0].z);
		GL.Vertex3(pos[1].x,pos[1].y,pos[1].z);
		GL.Vertex3(pos[2].x,pos[2].y,pos[2].z);
		GL.End();
		GL.PopMatrix();
		
	
	}

}