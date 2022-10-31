using UnityEngine;
using UnityEngine.InputSystem;
using Assets.Scripts;

// [ExecuteInEditMode] // Be carefull with this one
[RequireComponent(typeof(MeshRenderer))]
public class TestCameraInputEvents : MonoBehaviour
{
    public float rotationSpeed;
    public float movementSpeed;
    private float neutralSpeed = 0.0f;
    private float negativeRotationSpeed;
    private Vector3 nextRotationTransformation;    
    private float movementThreshold = 0.01f;
    private float movementThresholdDiagonal = 0.1f;
    
    // Start is called before the first frame update.
    void Start()
    {        
        negativeRotationSpeed = (-1 * rotationSpeed);
    }

    // Update is called once per frame.
    void Update()
    {        
        transform.Rotate(nextRotationTransformation);
    }

    public void OnLook(InputValue value)
    {
        Vector3 tempNextRotationTransformation = JoystickInputCalibration.getNextInput(value, true, movementThreshold, movementThresholdDiagonal, neutralSpeed, rotationSpeed, negativeRotationSpeed);
        Debug.Log("tempNextRotationTransformation: " + tempNextRotationTransformation);

        nextRotationTransformation = new Vector3((-1 * tempNextRotationTransformation.y), (-1 * tempNextRotationTransformation.x));
    }
}
