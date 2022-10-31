using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Range(2, 256)]
    public int resolution = 10;
    public bool autoUpdate = true;
    public enum FaceRenderMask { all, top, bottom, left, right, front, back }
    public FaceRenderMask faceRenderMask;

    public ShapeSettings shapeSettings;
    public ColorSettings colorSettings;

    [HideInInspector]
    public bool shapeSettingsFoldout, colorSettingsFoldout;

    ShapeGenerator shapeGenerator = new ShapeGenerator();
    ColorGenerator colorGenerator = new ColorGenerator();

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;

    void Initialize()
    {
        shapeGenerator.UpdateSettings(shapeSettings);
        colorGenerator.UpdateSettings(colorSettings);

        if (meshFilters == null || meshFilters.Length == 0)
            meshFilters = new MeshFilter[6];

        terrainFaces = new TerrainFace[6];

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (var index = 0; index < 6; index++)
        {
            if (meshFilters[index] == null)
            {
                var meshObject = new GameObject("Mesh");
                meshObject.transform.parent = transform;
                meshObject.AddComponent<MeshRenderer>();

                meshFilters[index] = meshObject.AddComponent<MeshFilter>();
                meshFilters[index].sharedMesh = new Mesh();
            }
            meshFilters[index].GetComponent<MeshRenderer>().sharedMaterial = colorSettings.material;

            terrainFaces[index] = new TerrainFace(shapeGenerator, meshFilters[index].sharedMesh, resolution, directions[index]);
            var renderFace = faceRenderMask == FaceRenderMask.all || (int)faceRenderMask - 1 == index;
            meshFilters[index].gameObject.SetActive(renderFace);
        }
    }

    void GenerateMesh()
    {
        for (var index = 0; index < 6; index++)
            if (meshFilters[index].gameObject.activeSelf)
                terrainFaces[index].ConstructMesh();

        colorGenerator.UpdateElevation(shapeGenerator.minMax);
    }

    void GenerateColors()
    {
        colorGenerator.UpdateColors();

        for (var index = 0; index < 6; index++)
            if (meshFilters[index].gameObject.activeSelf)
                terrainFaces[index].UpdateUVs(colorGenerator);
    }

    #region Handlers

    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColors();
    }

    public void OnShapeSettingsUpdated()
    {
        if (!autoUpdate)
            return;

        Initialize();
        GenerateMesh();
    }

    public void OnColorSettingsUpdated()
    {
        if (!autoUpdate)
            return;

        Initialize();
        GenerateColors();
    }

    #endregion
}
