using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(MeshRenderer))]
public class Main : MonoBehaviour
{
    private List<Vector3> _vertices = new();
    private List<int> _triangles = new();

    private void Start()
    {
        CreateCylinder();

        var mesh = new Mesh();

        mesh.SetVertices(_vertices);
        mesh.SetTriangles(_triangles, 0);

        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void CreateCylinder()
    {
        const float twoPi = Mathf.PI * 2;

        const float h = 15;
        const float r = 0.25f;
        const int angleDiv = 45;

        for (int y = 0; y < h; y++)
        {
            for (float i = 0; i < angleDiv; i++)
            {
                float f = i / angleDiv;

                float x = Mathf.Cos(f * twoPi) * r;
                float z = Mathf.Sin(f * twoPi) * r;

                _vertices.Add(new Vector3(x, y * 0.15f, z));
            }
        }

        for (int y = 0; y < h - 1; y++)
        {
            int yOffset = angleDiv * y;

            for (int i = 0; i < angleDiv; i++)
            {
                _triangles.Add(yOffset + i);
                _triangles.Add(yOffset + i + angleDiv);
                _triangles.Add(yOffset + (i + 1) % angleDiv);

                _triangles.Add(yOffset + i + angleDiv);
                _triangles.Add(yOffset + (i + 1) % angleDiv + angleDiv);
                _triangles.Add(yOffset + (i + 1) % angleDiv);
            }
        }
    }

    private void CreatePlane()
    {
        const int w = 5;
        const int h = 2;


        for (var y = 0; y < h; y++)
        for (int x = 0; x < w; x++)
            _vertices.Add(new Vector3(x, y));

        for (var i = 0; i < w * (h - 1) - 1; i++)
        {
            _triangles.Add(i + 0);
            _triangles.Add(i + w);
            _triangles.Add(i + 1);

            _triangles.Add(i + w);
            _triangles.Add(i + w + 1);
            _triangles.Add(i + 1);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        for (var index = 0; index < _vertices.Count; index++)
            Handles.Label(_vertices[index], index.ToString());
    }
}