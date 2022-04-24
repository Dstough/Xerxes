using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
public class Asteroid : MonoBehaviour
{
    public int seed;

    private int oldResolution;
    [Range(4, 14)] public int resolution = 9;
    private int oldScale;
    [Range(1, 10)] public int scale = 5;
    private float oldTerrainLevel;
    [Range(0f, .999f)] public float terrainLevel = .5f;

    MeshFilter meshFilter;
    List<Vector3> vertices;
    List<int> triangles;
    float[,,] terrainMap;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        vertices = new List<Vector3>();
        triangles = new List<int>();
        
        ClearMesh();
        PopulateTerrainMap();
        CreateMeshData();
        BuildMesh();
    }

    private void Update()
    {
        if (oldResolution!=resolution || oldTerrainLevel != terrainLevel || oldScale != scale)
        {
            ClearMesh();
            PopulateTerrainMap();
            CreateMeshData();
            BuildMesh();
        }

        oldResolution = resolution;
        oldScale = scale;
        oldTerrainLevel = terrainLevel;
    }

    void CreateMeshData()
    {
        for (int x = 0; x < resolution; x++)
            for (int y = 0; y < resolution; y++)
                for (int z = 0; z < resolution; z++)
                {
                    float[] cube = new float[8];
                    for (int i = 0; i < 8; i++)
                    {
                        var corner = new Vector3Int(x, y, z) + MarchingCubes.CornerTable[i];
                        cube[i] = terrainMap[corner.x, corner.y, corner.z];
                    }

                    MarchCube(new Vector3Int(x, y, z), cube);
                }

    }

    void PopulateTerrainMap()
    {
        terrainMap = new float[resolution + 1, resolution + 1, resolution + 1];
        
        for (int x = 0; x < resolution + 1; x++)
            for (int y = 0; y < resolution + 1; y++)
                for (int z = 0; z < resolution + 1; z++)
                    terrainMap[x, y, z] = GetRandomNumber(x, y, z, resolution);
    }

    int GetCubeConfiguration(float[] cube)
    {
        int configurationIndex = 0;

        for (int i = 0; i < 8; i++)
            if (cube[i] > terrainLevel)
                configurationIndex |= 1 << i;
        
        return configurationIndex;
    }

    void MarchCube(Vector3 position, float[] cube)
    {
        var configIndex = GetCubeConfiguration(cube);

        if (configIndex == 0 || configIndex == 255)
            return;

        var edgeIndex = 0;

        for (var i = 0; i < 5; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                var index = MarchingCubes.TriangleTable[configIndex, edgeIndex];

                if (index == -1)
                    return;

                var vertex1 = (position + MarchingCubes.EdgeTable[index, 0]) * scale;
                var vertex2 = (position + MarchingCubes.EdgeTable[index, 1]) * scale;
                var vertexPosition = (vertex1 + vertex2) / 2f;

                vertices.Add(vertexPosition);
                triangles.Add(vertices.Count - 1);

                edgeIndex++;
            }
        }
    }

    void ClearMesh()
    {
        vertices.Clear();
        triangles.Clear();
    }

    void BuildMesh()
    {
        var mesh = new Mesh
        {
            vertices = vertices.ToArray(),
            triangles = triangles.ToArray()
        };

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;
    }

    float GetRandomNumber(int x, int y, int z, int resolution)
    {
        if (x == 0 || y == 0 || z == 0 || x == resolution || y == resolution || z == resolution)
            return 1;

        Random.InitState(seed + x + (y * resolution) + (z * resolution * resolution));
        return Random.Range(0f, 1f);
    }
}