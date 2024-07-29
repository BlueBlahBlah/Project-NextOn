using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class DonutCollider : MonoBehaviour
{
    public int radialSegments = 15;
    public int tubularSegments = 15;
    public float radius = 5.5f;
    public float tubeRadius = 0.5f;
    public Material transparentMaterial;

    void Start()
    {
        Mesh donutMesh = CreateDonutMesh(radius, tubeRadius, radialSegments, tubularSegments);
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        MeshCollider meshCollider = GetComponent<MeshCollider>();

        meshFilter.mesh = donutMesh;
        meshCollider.sharedMesh = donutMesh;

        // Set the material to a transparent material
        if (transparentMaterial != null)
        {
            meshRenderer.material = transparentMaterial;
        }
        else
        {
            // Optionally create a transparent material if one is not assigned
            Material mat = new Material(Shader.Find("Standard"));
            mat.color = new Color(1, 1, 1, 0.5f); // Semi-transparent white
            mat.SetFloat("_Mode", 3); // Transparent mode
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

            meshRenderer.material = mat;
        }
    }

    Mesh CreateDonutMesh(float radius, float tubeRadius, int radialSegments, int tubularSegments)
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[(radialSegments + 1) * (tubularSegments + 1)];
        int[] triangles = new int[radialSegments * tubularSegments * 6];
        Vector2[] uv = new Vector2[(radialSegments + 1) * (tubularSegments + 1)];

        for (int i = 0; i <= radialSegments; i++)
        {
            float theta = i * 2f * Mathf.PI / radialSegments;
            Vector3 center = new Vector3(Mathf.Cos(theta) * radius, 0, Mathf.Sin(theta) * radius);

            for (int j = 0; j <= tubularSegments; j++)
            {
                float phi = j * 2f * Mathf.PI / tubularSegments;
                Vector3 vertex = center + new Vector3(Mathf.Cos(phi) * tubeRadius * Mathf.Cos(theta),
                                                      Mathf.Sin(phi) * tubeRadius,
                                                      Mathf.Cos(phi) * tubeRadius * Mathf.Sin(theta));
                vertices[i * (tubularSegments + 1) + j] = vertex;
                uv[i * (tubularSegments + 1) + j] = new Vector2((float)i / radialSegments, (float)j / tubularSegments);
            }
        }

        for (int i = 0; i < radialSegments; i++)
        {
            for (int j = 0; j < tubularSegments; j++)
            {
                int a = i * (tubularSegments + 1) + j;
                int b = (i + 1) * (tubularSegments + 1) + j;
                int c = (i + 1) * (tubularSegments + 1) + j + 1;
                int d = i * (tubularSegments + 1) + j + 1;

                triangles[(i * tubularSegments + j) * 6 + 0] = a;
                triangles[(i * tubularSegments + j) * 6 + 1] = b;
                triangles[(i * tubularSegments + j) * 6 + 2] = d;
                triangles[(i * tubularSegments + j) * 6 + 3] = b;
                triangles[(i * tubularSegments + j) * 6 + 4] = c;
                triangles[(i * tubularSegments + j) * 6 + 5] = d;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateNormals();

        return mesh;
    }
}
