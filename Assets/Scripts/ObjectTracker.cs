using UnityEngine;
using UnityEngine.InputSystem;

// [ExecuteInEditMode] // Be carefull with this one
[RequireComponent(typeof(MeshRenderer))]
public class ObjectTracker : MonoBehaviour
{
    [SerializeField]
    protected Transform trackingTarget;

    [SerializeField]
    float xOffset;

    [SerializeField]
    float yOffset;

    [SerializeField]
    float zOffset;

    // Start is called before the first frame update.
    void Start()
    {
        transform.position = GetNextPosition();
    }

    // Update is called once per frame.
    void Update()
    {        
        transform.position = GetNextPosition();
    }

    private Vector3 GetNextPosition()
    {
        return new Vector3(trackingTarget.position.x + xOffset, trackingTarget.position.y + yOffset, trackingTarget.position.z + zOffset);
    }
}
