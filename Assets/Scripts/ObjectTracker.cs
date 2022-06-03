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
        
    }

    // Update is called once per frame.
    void Update()
    {
        this.transform.position = new Vector3(trackingTarget.position.x + xOffset, trackingTarget.position.y + yOffset, transform.position.z + zOffset);
    }
}
