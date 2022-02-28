using UnityEngine;

[ExecuteAlways]
public class RegularPolygon : MonoBehaviour {
  [SerializeField] private Material material;
  [Min(0)]
  [SerializeField] private float radius = 5;
  [Range(3, 30)]
  [SerializeField] private int numVertices = 5;
  [Range(0, 360)]
  [SerializeField] private int startingAngle = 0;

  private MeshFilter meshFilter;
  private MeshRenderer meshRenderer;
  private Mesh mesh;

  private void Awake() {
    // Add required components to display a mesh, if they aren't already there.
    // As we have ExecuteInEditMode on they'll get added if you're viewing this in the editor.
    meshFilter = gameObject.GetComponent<MeshFilter>();
    if (meshFilter == null) {
      meshFilter = gameObject.AddComponent<MeshFilter>();
    }
    meshRenderer = gameObject.GetComponent<MeshRenderer>();
    if (meshRenderer == null) {
      meshRenderer = gameObject.AddComponent<MeshRenderer>();
    }
    mesh = new Mesh();

    meshFilter.mesh = mesh;
  }

  // Called whenever values in the InspectorGUI are changed.
  private void OnValidate() {
    // Note this actually gets called before Awake, so trap if our objects aren't initialised.
    if (mesh != null) {
      Start();
    }
  }

  void Start() {
    // Remove the current assignments.
    mesh.Clear();
    // Update the material
    meshRenderer.material = material;

    // Angle of each segment in radians.
    float angle = 2 * Mathf.PI / numVertices;

    // Create the vertices around the polygon.
    Vector3[] vertices = new Vector3[numVertices];
    Vector2[] uv = new Vector2[numVertices];

    // Covert degrees to radians.
    float startingRadians = Mathf.Deg2Rad * startingAngle;

    for (int i = 0; i < numVertices; i++) {
      vertices[i] = new Vector3(Mathf.Sin(startingRadians + (i * angle)), 0, Mathf.Cos(startingRadians + (i * angle))) * radius;
      uv[i] = new Vector2(1 + Mathf.Sin(startingRadians + (i * angle)), 1 + Mathf.Cos(startingRadians + (i * angle))) * 0.5f;
    }
    mesh.vertices = vertices;
    mesh.uv = uv;

    // The triangle vertices must be done in clockwise order.
    int[] triangles = new int[3 * (numVertices - 2)];
    for (int i = 0; i < numVertices - 2; i++) {
      triangles[3 * i] = 0;
      triangles[(3 * i) + 1] = i + 1;
      triangles[(3 * i) + 2] = i + 2;
    }
    mesh.triangles = triangles;
  }
}
