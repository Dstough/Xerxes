using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class MeshSwappable : MonoBehaviour
{
    public List<Mesh> meshes = new();

    [Range(0,5)]
    public int index = 0;
    private int previousIndex = 0;

    private void Start()
    {
    }

    void Update()
    {
        if(index > meshes.Count)
            index = meshes.Count;
        else if(index < 0)
            index = 0;

        if (index == previousIndex)
            return;

        var myMeshFilter = gameObject.GetComponent<MeshFilter>();
        var myMeshCollider = gameObject.GetComponent<MeshCollider>();

        myMeshFilter.mesh = meshes[index];
        myMeshCollider.sharedMesh = meshes[index];

        previousIndex = index;
    }
}
