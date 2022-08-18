using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(ChunkResizeable))]
public class FloodFillable : MonoBehaviour
{
    public List<GameObject> objectsToFloodFill = new();

    [Range(0, 10000)]
    public int seed = 0;

    [Range(0, 100)]
    public int ammount = 0;

    [Range(0f, 1f)]
    public float rotationLikelyhood = 0f;

    int
        oldSeed = 0,
        oldAmmount = 0;

    float oldRotationLikelyhood = 0f;
    bool dirty = true;
    List<GameObject> objectsGenerated = new();
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

        if (!dirty)
            return;

        if (objectsToFloodFill.Count == 0)
            return;

        Random.InitState(seed);

        foreach (var gameObject in objectsGenerated)
            DestroyImmediate(gameObject);

        objectsGenerated.Clear();

        for (var count = 0; count < ammount; count++)
        {
            var index = Random.Range(0, objectsToFloodFill.Count - 1);

            if (objectsToFloodFill[index] == null)
                continue;

            objectsToFloodFill[index].TryGetComponent<MeshCollider>(out var meshColider);
            
            var placementErrorCount = 0;
            var placementFound = false;

            //TODO: Fix this by generating the object first then moving it into place.
            //      The size also needs to be evaluated again. For some reason the mesh
            //      collider always returns 0 for the size, I don't know why.
            //      > Stoats.
            do
            {
                var size = meshColider != null ? meshColider.bounds.size : Vector3.zero;
                var location = new Vector3
                {
                    x = Random.Range(transform.position.x, transform.position.x + chunkResizeable.width),
                    y = Random.Range(transform.position.y, transform.position.y + chunkResizeable.height),
                    z = Random.Range(transform.position.z, transform.position.z + chunkResizeable.depth)
                };

                if (Physics.CheckBox(location, size / 2, transform.rotation) == true)
                    placementErrorCount++;
                else
                {
                    var newGameObject = Instantiate(objectsToFloodFill[index], location, Quaternion.identity, transform);
                    
                    newGameObject.transform.localScale = size;

                    if (Random.Range(0f, 1f) < rotationLikelyhood)
                    {
                        newGameObject.TryGetComponent<Rotatable>(out var rotatable);
                        //TODO: Randomize the rotation speeds of the object via the rotateable 
                    }

                    objectsGenerated.Add(newGameObject);
                    placementFound = true;
                };

            } while (!placementFound && placementErrorCount < 5);
        }

        oldSeed = seed;
        oldAmmount = ammount;
        oldRotationLikelyhood = rotationLikelyhood;

        dirty = false;
    }
}
