using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUI : MonoBehaviour {

	[SerializeField]
	private Slider width;

	[SerializeField]
	private Slider height;

	[SerializeField]
	private Slider size;

	[SerializeField]
	private GameObject gridSpawner;

	public void changeGrid () {
		HexGrid hexGrid = gridSpawner.GetComponent<HexGrid> ();
		hexGrid.width = (int)width.value;
		hexGrid.height = (int)height.value;
		hexGrid.size = (float)size.value;

		hexGrid.DestroyGrid();
		hexGrid.CreateGrid(hexGrid.width, hexGrid.height);
	}
}
