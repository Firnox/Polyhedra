using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RegularPolygon))]
public class RegularPolygonEditor : Editor {

	[MenuItem("GameObject/Custom Objects/Regular Polygon")]
	static void Create() {
		// Create an empty game object with the name RegularPolygon.
		GameObject gameObject = new GameObject("RegularPolygon");

		// Attach our RegularPolygon script to it.
		gameObject.AddComponent<RegularPolygon>();
	}
}
