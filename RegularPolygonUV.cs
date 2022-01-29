using UnityEngine;

public class RegularPolygonUV : MonoBehaviour {
  [SerializeField] private Material material;
  [SerializeField] private float radius = 5;
  [SerializeField] private int numVertices = 5;

  void Start() {
    // Add required components to display a mesh.
    MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
    MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
    Mesh mesh = new Mesh();

    meshRenderer.material = material;
    meshFilter.mesh = mesh;

    // Angle of each segment in radians.
    float angle = 2 * Mathf.PI / numVertices;

    // Create the vertices around the polygon.
    Vector3[] vertices = new Vector3[numVertices];
    Vector2[] uv = new Vector2[numVertices];
    for (int i = 0; i < numVertices; i++) {
      vertices[i] = new Vector3(Mathf.Sin(i * angle), 0, Mathf.Cos(i * angle)) * radius;
      uv[i] = new Vector2(1 + Mathf.Sin(i * angle), 1 + Mathf.Cos(i * angle)) * 0.5f;
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
