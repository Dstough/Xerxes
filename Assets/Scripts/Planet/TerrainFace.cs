using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFace
{
    Mesh mesh;
    ShapeGenerator shapeGenerator;
    int resolution;
    Vector3 localUp;
    Vector3 axisA;
    Vector3 axisB;

    public TerrainFace(ShapeGenerator shapeGenerator, Mesh mesh, int resolution, Vector3 localUp)
    {
        this.mesh = mesh;
        this.shapeGenerator = shapeGenerator;
        this.resolution = resolution;
        this.localUp = localUp;

        axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        axisB = Vector3.Cross(localUp, axisA);
    }

    public void ConstructMesh()
    {
        var vertices = new Vector3[resolution * resolution];
        var triangles = new int[(resolution - 1) * (resolution - 1) * 6];
        var triangleIndex = 0;
        var uv = mesh.uv;

        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                var index = x + y * resolution;
                var percent = new Vector2(x, y) / (resolution - 1);
                var pointOnUnitCube = localUp +
                    (percent.x - .5f) * 2 * axisA +
                    (percent.y - .5f) * 2 * axisB;
                var pointOnUnitSphere = pointOnUnitCube.normalized;

                vertices[index] = shapeGenerator.CalculatePointOnPlanet(pointOnUnitSphere);

                if (x != resolution - 1 && y != resolution - 1)
                {
                    triangles[triangleIndex] = index;
                    triangles[triangleIndex + 1] = index + resolution + 1;
                    triangles[triangleIndex + 2] = index + resolution;

                    triangles[triangleIndex + 3] = index;
                    triangles[triangleIndex + 4] = index + 1;
                    triangles[triangleIndex + 5] = index + resolution + 1;

                    triangleIndex += 6;
                }
            }
        }

        if (mesh == null)
            return;

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.uv = uv;
    }

    public void UpdateUVs(ColorGenerator colorGenerator)
    {
        var uv = new Vector2[resolution * resolution];
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                var index = x + y * resolution;
                var percent = new Vector2(x, y) / (resolution - 1);
                var pointOnUnitCube = localUp +
                    (percent.x - .5f) * 2 * axisA +
                    (percent.y - .5f) * 2 * axisB;
                var pointOnUnitSphere = pointOnUnitCube.normalized;

                uv[index] = new Vector2(colorGenerator.BiomePercentFromPoint(pointOnUnitSphere), 0);
            }
        }
        mesh.uv = uv;
    }
}
