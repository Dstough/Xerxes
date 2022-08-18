using UnityEngine;

[ExecuteInEditMode]
public class ChunkResizeable : MonoBehaviour
{
    [Range(0f, 100f)]
    public float
        height = 10f,
        width = 10f,
        depth = 10f;

    private float
        oldHeight = 10f,
        oldWidth = 10f,
        oldDepth = 10f;

    private Vector3 
        oldPosition,
        center,
        size;

    bool dirty = true;

    void Update()
    {
        dirty = dirty ||
            oldHeight != height ||
            oldWidth != width ||
            oldDepth != depth ||
            oldPosition != transform.position;

        if (!dirty)
            return;

        center = new Vector3(transform.position.x + width / 2f, transform.position.y + height / 2f, transform.position.z + depth / 2f);
        size = new Vector3(width, height, depth);

        oldPosition = transform.position;
        oldHeight = height;
        oldWidth = width;
        oldDepth = depth;
        
        dirty = false;
    }

    public float GetMinimumDimention()
    {
        var minimum = width;

        if (height < minimum)
            minimum = height;

        if (depth < minimum)
            minimum = depth;

        return minimum;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(center, 1f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }
}
