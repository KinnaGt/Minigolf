using UnityEngine;

public class TestingMesh : MonoBehaviour
{
    void Start()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices;
        Vector2[] uv = new Vector2[3];
        int[] triangles;

        vertices = new Vector3[] { new(0, 0), new(0, 100), new(100, 100) };
        uv= new Vector2[] { new(0, 0), new(0, 1), new(1, 1) };
        triangles = new int[] { 0, 1, 2 };

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        GetComponent<MeshFilter>().mesh = mesh;
    }

}
