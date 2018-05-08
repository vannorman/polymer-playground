
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class MeshCombiner{

	//takes an array of meshes and combines them into one mesh
	//submeshes within the same mesh will never merge,
	//but submeshes from different meshes with the same submesh-index can be merged by setting merge to true
	static public Mesh combinedMeshes(Mesh[] meshes, bool merge)
	{
		if (merge)
		{
			return mergedMeshes(meshes);
		}
		else
		{
			return concatenatedMeshes(meshes);
		}
	}

	//takes an array of meshes and combines them into one mesh
	//the given array transforms will be used to transform the first transforms.Length meshes
	//submeshes within the same mesh will never merge,
	//but submeshes from different meshes with the same submesh-index can be merged by setting merge to true
	static public Mesh combinedMeshes(Mesh[] meshes, Matrix4x4[] transforms, bool merge)
	{
		Mesh[] transmeshes = new Mesh[meshes.Length];
		for (int i = 0; i < meshes.Length && i < transforms.Length; i++)
		{
			transmeshes[i] = transformedMesh(meshes[i], transforms[i]);
		}
		return combinedMeshes(transmeshes, merge);
	}

	//returns a mesh with all meshes combined, while keeping all submeshes seperate
	//the order of the meshes is determined by the order in which they are put in the array
	static public Mesh concatenatedMeshes(Mesh[] meshes)
	{
		Mesh merged = new Mesh();
		CombineInstance[] ci = new CombineInstance[meshes.Length];
		int submeshcount = 0;
		for (int i = 0; i < meshes.Length; i++)
		{
			submeshcount = Mathf.Max(meshes[i].subMeshCount, submeshcount);
			ci[i].mesh = meshes[i];
			ci[i].subMeshIndex = 0;
		}
		merged.CombineMeshes(ci, true, false); //let's assume that the vertices are in order
		merged.triangles = null;
		merged.subMeshCount = 0;
		int offset = 0;
		for(int i = 0; i < meshes.Length; i++)
		{
			for (int j = 0; j < meshes[i].subMeshCount; j++)
			{
				int[] t = meshes[i].GetTriangles(j);
				for (int k = 0; k < t.Length; k++)
				{
					t[k] += offset; //make sure that every triangle points to the right points
				}
				merged.subMeshCount++;
				merged.SetTriangles(t, merged.subMeshCount - 1);
			}
			offset += meshes[i].vertexCount; //assuming that the vertices are in order, this should tell where they start for each mesh
		}
		return merged;
	}

	//returns a mesh with all meshes combined by merging submeshes with the same submesh index
	//the index of all submeshes will remain the same
	static public Mesh mergedMeshes(Mesh[] meshes)
	{
		Mesh merged = new Mesh();
		CombineInstance[] ci = new CombineInstance[meshes.Length];
		int submeshcount = 0;
		for (int i = 0; i < meshes.Length; i++)
		{
			submeshcount = Mathf.Max(meshes[i].subMeshCount, submeshcount);
			ci[i].mesh = meshes[i];
			ci[i].subMeshIndex = 0;
		}
		merged.CombineMeshes(ci, true, false); //let's assume that the vertices are in order
		merged.subMeshCount = submeshcount;
		for (int i = 1; i < submeshcount; i++) //submesh index 0 is already merged
		{
			List<int> submesh = new List<int>();
			int offset = 0;
			for (int j = 0; j < meshes.Length; j++)
			{
				if (i < meshes[j].subMeshCount)
				{
					int[] t = meshes[j].GetTriangles(i);
					for (int k = 0; k < t.Length; k++)
					{
						t[k] += offset; //make sure that every triangle points to the righ points
					}
					submesh.AddRange(t);
				}
				offset += meshes[j].vertexCount; //assuming that the vertices are in order, this should tell where they start for each mesh
			}
			merged.SetTriangles(submesh, i);
		}
		return merged;
	}

	//returns the same mesh with all submeshes merged into one submesh
	static public Mesh mergedMesh(Mesh mesh)
	{
		Mesh merged = Object.Instantiate<Mesh>(mesh);
		int[] t = merged.triangles;
		merged.triangles = null;
		merged.subMeshCount = 1;
		merged.triangles = t;
		return merged;
	}

	//returns the same mesh with all vertex data transformed
	static public Mesh transformedMesh(Mesh mesh, Matrix4x4 M)
	{
		CombineInstance[] ci = new CombineInstance[1];
		ci[0].mesh = mesh;
		ci[0].subMeshIndex = 0;
		ci[0].transform = M;

		Mesh transformed = new Mesh();
		transformed.CombineMeshes(ci, false, true);
		transformed.subMeshCount = mesh.subMeshCount;
		for (int i = 1; i < mesh.subMeshCount; i++)
		{
			transformed.SetTriangles(mesh.GetTriangles(i),i);
		}
		return transformed;
	}

}