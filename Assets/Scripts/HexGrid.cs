using UnityEngine;
using System.Collections;

public class HexGrid : MonoBehaviour {

	[SerializeField]
	public int width = 1;

	[SerializeField]
	public int height = 1;

	[SerializeField]
	public float size = 1f;

	private float innerSize;

	private GameObject[] hexagons = new GameObject[1*1];
	private int hexAmount = 0;

	void Start () {
		CreateGrid (width, height);
	}

	//Start with forloop for every row, nested in which is a forloop for all columns
	public void CreateGrid (int width, int height){
		//Create array to store hexagons
		hexagons = new GameObject[width*height];

		for (int w = 0; w < width; w++) {
			for (int h = 0; h < height; h++) {

				//Create a new hexagon gameobject, and rename it
				GameObject hexagon = new GameObject ();
				hexagon.name = "Hexagon";

				//Put hexagon in array
				hexagons[hexAmount] = hexagon;
				hexAmount++;

				//Add hexagon generation script to the newly created hexagon object
				Hexagon hex = hexagon.AddComponent<Hexagon> ();

				//Set the size and coordinates of the hexagon
				hex.size = size;
				innerSize = size * (Mathf.Sqrt(3)/2);
				hex.x = w * (size * 1.5f);
				hex.y = (h + w * 0.5f - w / 2) * (innerSize * 2f);

				//Make sure the hexgrid is centered
				hex.x -= (width * size) / 2;
				hex.y -= (height * innerSize);

				//Finally, instantiate
				hex.CreateHex (hex.x, hex.y, hex.size, hex.corners);
			}
		}
	}

	public void DestroyGrid () {
		//Debug.Log (hexagons.Length);
		for (int i = 0; i < hexagons.Length; i++) {
			Debug.Log ("Deleting " + i + " aka " + hexagons[i]);
			Destroy (hexagons[i]);
		}
		hexAmount = 0;
	}
}
