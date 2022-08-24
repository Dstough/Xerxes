using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

[ExecuteInEditMode]
[RequireComponent(typeof(ChunkResizeable))]
public class FloodFillable : MonoBehaviour
{
    public List<GameObject> objectsToFloodFill = new();

    [Range(0, 10000)]
    public int seed = 0;

    [Range(0, 1000)]
    public int ammount = 0;

    [Range(0f, 1f)]
    public float rotationLikelyhood = 0f;

    int
        oldSeed = 0,
        oldAmmount = 0;

    float oldRotationLikelyhood = 0f;
    bool dirty = true;
    ChunkResizeable chunkResizeable;

    private void Start()
    {
        chunkResizeable = GetComponent<ChunkResizeable>();
    }

    void Update()
    {
        dirty = dirty ||
            oldSeed != seed ||
            oldAmmount != ammount ||
            oldRotationLikelyhood != rotationLikelyhood;

        if (!dirty || objectsToFloodFill.Count == 0)
            return;

        Reinitialize();

        oldSeed = seed;
        oldAmmount = ammount;
        oldRotationLikelyhood = rotationLikelyhood;
        dirty = false;
    }

    private void Reinitialize()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
            DestroyImmediate(transform.GetChild(i).gameObject);
        
        Random.InitState(seed);

        for (var count = 0; count < ammount; count++)
        {
            var index = Random.Range(0, objectsToFloodFill.Count - 1);

            if (objectsToFloodFill[index] == null)
                continue;

            var placementErrorCount = 0;
            var placementFound = false;
            var newGameObject = Instantiate(objectsToFloodFill[index], Vector3.zero, Quaternion.identity, transform);
            var size = Random.Range(1, chunkResizeable.GetMinimumDimention() / 10f);

            newGameObject.transform.localScale = new Vector3(size, size, size);
            newGameObject.transform.rotation = Quaternion.Euler(Random.Range(0f, 90f), Random.Range(0f, 90f), Random.Range(0f, 90f));
            newGameObject.TryGetComponent<MeshSwappable>(out var meshSwappable);
            newGameObject.TryGetComponent<MeshCollider>(out var meshCollider);
            newGameObject.TryGetComponent<Rotatable>(out var rotatable);

            if (meshSwappable != null)
                meshSwappable.index = Random.Range(0, Mathf.Max(0, meshSwappable.meshes.Count));

            if (rotatable != null && Random.Range(0f, 1f) < rotationLikelyhood)
            {
                rotatable.xSpeed = Random.Range(-2f, 2f);
                rotatable.ySpeed = Random.Range(-2f, 2f);
                rotatable.zSpeed = Random.Range(-2f, 2f);
                rotatable.gloabalSpeed = Random.Range(1f, 5f);
            }

            Physics.SyncTransforms();

            do
            {
                var location = new Vector3
                {
                    x = Random.Range(transform.position.x, transform.position.x + chunkResizeable.width),
                    y = Random.Range(transform.position.y, transform.position.y + chunkResizeable.height),
                    z = Random.Range(transform.position.z, transform.position.z + chunkResizeable.depth)
                };

                if (meshCollider == null || Physics.CheckBox(location, meshCollider.bounds.size) == false)
                {
                    newGameObject.transform.position = location;
                    placementFound = true;
                }
                else
                    placementErrorCount++;
                
            } while (!placementFound && placementErrorCount < 5);

            if (!placementFound)
                DestroyImmediate(newGameObject);
        }
    }
}
