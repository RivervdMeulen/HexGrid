using UnityEngine;
using System.Collections;
using System.Linq;

public class Hexagon : MonoBehaviour {

	public float x = 0f;
	public float y = 0f;
	public float size = 0.5f;
	public int corners = 6;

	public void CreateHex (float X, float Y, float size, int corners) {
		//First I create a new int of the amount of corners, to be used in the calculations
		//Then I create arrays for the vertices and the triangles, to be filled by for loops
		int calcCorners = corners - 2;
		Vector3[] verts = new Vector3[corners];
		int[] tris = new int[calcCorners*3];

		//Here the vertices array gets filled with the verts of all the corners
		for (int i = 0; i < corners; i++) {
			//360 degrees for the whole circle, divided by the amount of corners, tells me how much degrees every corner is, which is needed for the calculation of the point
			verts [i] = GetPoint (X, Y, size, (360 / corners) * i);
		}

		//Here I fill the triangles array, by making triangles between the first vert and the other verts
		//I make all triangles include the first vert and 2 others because this way I use less verts and tris than if I were to add a vert in the middle to connect all tris to
		//This is for optimalization purposes, as the models will create a little less lag this way, especially on shapes with more corners and in large amounts of shapes
		for (int i = 0; i < calcCorners; i++) {
			tris [i * 3] = 0;
			tris [(i * 3) + 1] = i + 1;
			tris [(i * 3) + 2] = i + 2;
		}

		//Creating a new mesh
		MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
		Mesh mesh = meshFilter.mesh;

		//Adding the calculated verts to the new mesh
		mesh.vertices = verts;

		MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer> ();

		//Adding the tris to the new mesh, so it actually gets rendered
		mesh.triangles = tris;

		//Create an array for the UV mapping coordinates
		Vector2[] UV = new Vector2[mesh.vertices.Length];

		//For loop to add the UV map coordinates
		for (int i=0; i < UV.Length; i++) {
			//I add the size then divide by 2 to make sure the texture doesn't get tiled but instead fits exactly
			UV[i] = new Vector2((mesh.vertices[i].x+size)/2, (mesh.vertices[i].z+size)/2);
		}

		//Add the new UV mapping to the new mesh
		mesh.uv = UV;

		//Recalculating the normals to make sure the lighting works
		mesh.RecalculateNormals ();

		//Adding a standard white material to the newly created object
		Material mat = new Material (Shader.Find ("Standard"));
		mat.color = new Color (1f, 1f, 1f);
		meshRenderer.material = mat;
	}

	//Get a point on a radius at a certain angle
	//This is used to get the corners of the hexagon (or octagon, pentagon, etc.)
	//Give it an X and Y value to offset the result (to move the hexagon origin to a different position)
	//The radius is used to determine how far from the origin the point is placed, thus making the final shape larger or smaller
	//The angle is used to determine at what angle the point I'm looking for is placed
	Vector3 GetPoint (float X, float Y, float rad, float angle) {
		//The X coordinates get flipped, otherwise the faces are inverted, it's not the best solution but it works
		float pointX = -(rad * Mathf.Cos (angle * Mathf.PI / 180f)) + X;
		float pointZ = rad * Mathf.Sin (angle * Mathf.PI / 180f) + Y;
		Vector3 result = new Vector3 (pointX, 0, pointZ);
		return result;
	}
}
