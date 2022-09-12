using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

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

    // Vector3 offset;
    // Quaternion offsetRotation;

    // Old version.    
    // Start is called before the first frame update.
    void Start()
    {
        transform.position = GetNextPosition();
        // offset = GetNextPosition();

        // offsetRotation = transform.rotation * Quaternion.Inverse(trackingTarget.rotation);
    }

    // Update is called once per frame.
    void Update()
    {
        // transform.position = GetNextPosition();
        transform.position = GetInitialPosition();



        // transform.rotation = trackingTarget.rotation;
        // transform.rotation = Quaternion.Slerp(transform.rotation, trackingTarget.rotation * offsetRotation, 0.8f);
        // transform.RotateAround(trackingTarget.transform.position, Vector3.up, 20 * Time.deltaTime);        
        // transform.RotateAround(transform.position, Vector3.up, 20 * Time.deltaTime);
    }
    private void LateUpdate()
    {
        // transform.position = trackingTarget.position + offset;
    }

    private Vector3 GetNextPosition()
    {
        return new Vector3(trackingTarget.position.x + xOffset, trackingTarget.position.y + yOffset, trackingTarget.position.z + zOffset);
        // return new Vector3(trackingTarget.transform.position.x + xOffset, trackingTarget.transform.position.y + yOffset, trackingTarget.transform.position.z + zOffset);
    }

    private Vector3 GetInitialPosition()
    {
        return new Vector3(trackingTarget.position.x + xOffset, trackingTarget.position.y + yOffset, trackingTarget.position.z + zOffset);
        // return new Vector3(trackingTarget.transform.position.x + xOffset, trackingTarget.transform.position.y + yOffset, trackingTarget.transform.position.z + zOffset);
    }
    

    // New version
    /*
    private Transform targetTransform = null;

    [Range(1, 10)]

    [SerializeField]
    private float smoothSpeed = 5f;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private Vector3 minValue, maxValue;

    void Start()
    {
        targetTransform = trackingTarget;
    }

    private void LateUpdate()
    {
        updateCamPos();
    }

    private void updateCamPos()
    {
        // Vector3 desiredPos = playerTransform.position + offset;
        Vector3 desiredPos = targetTransform.TransformPoint(offset);

        Vector3 clampPosition = new Vector3(
            Mathf.Clamp(desiredPos.x, minValue.x, maxValue.x),
            Mathf.Clamp(desiredPos.y, minValue.y, maxValue.y),
            Mathf.Clamp(desiredPos.z, minValue.z, maxValue.z));

        Vector3 smoothPos = Vector3.Lerp(
            transform.position,
            clampPosition,
            smoothSpeed * Time.deltaTime);

        transform.position = smoothPos;

        transform.LookAt(targetTransform);
    }
    */
}
