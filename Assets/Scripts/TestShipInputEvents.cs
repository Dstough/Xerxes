using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Assets.Scripts;

// [ExecuteInEditMode] // Be carefull with this one
[RequireComponent(typeof(MeshRenderer))]
public class TestShipInputEvents : MonoBehaviour
{
    public float rotationSpeed;
    public float movementSpeed;
    private float neutralSpeed = 0.0f;
    private float negativeRotationSpeed;
    private Vector3 nextRotationTransformation;
    private float nextMovement = 0.0f;
    private float nextVerticalMovement = 0.0f;
    private float nextHorizontalMovement = 0.0f;
    private float movementThreshold = 0.01f;
    private float movementThresholdDiagonal = 0.1f;
    public TextMeshProUGUI _textMesh;

    private Quaternion originalRotation;
    
    // Start is called before the first frame update.
    void Start()
    {
        originalRotation = transform.localRotation;
        negativeRotationSpeed = (-1 * rotationSpeed);
    }

    // Update is called once per frame.
    void Update()
    {
        if (nextRotationTransformation.x == 0 && nextRotationTransformation.y == 0 && nextRotationTransformation.z == 0)
        {
            // Do nothing.
        } 
        else
        {
            transform.Rotate(nextRotationTransformation);
        }

        // Forward / Backward movement.
        transform.position += transform.right * movementSpeed * nextMovement * Time.deltaTime;

        // Horizontal movement.
        transform.position += transform.up * movementSpeed * nextHorizontalMovement * Time.deltaTime;

        // Vertical movement.
        transform.position += transform.forward * movementSpeed * nextVerticalMovement * Time.deltaTime;

        _textMesh.text = "Rotation: " + transform.rotation;
    }
        
    public void OnMove(InputValue value)
    {
        nextRotationTransformation = JoystickInputCalibration.getNextInput(value, true,movementThreshold, movementThresholdDiagonal, neutralSpeed, rotationSpeed, negativeRotationSpeed);
        Debug.Log("nextRotationTransformation: " + nextRotationTransformation);
    }

    public void OnStickTwist(InputValue value)
    {
        try
        {
            Debug.Log("In OnStickTwist");
            float eventValue = (float)value.Get();
            _textMesh.text = "OnStickTwist: eventValue: " + eventValue;
            
            // Rotate based on direction pressed.
            if (JoystickInputCalibration.isPositive(eventValue, movementThreshold))
            {
                // Right
                Debug.Log("In Right");
                nextRotationTransformation = new Vector3(neutralSpeed, neutralSpeed, rotationSpeed);
            }
            else if (JoystickInputCalibration.isNegative(eventValue, movementThreshold))
            {
                // Left
                Debug.Log("In Left");
                nextRotationTransformation = new Vector3(neutralSpeed, neutralSpeed, negativeRotationSpeed);
            }            
        }
        catch
        {
            nextRotationTransformation = new Vector3(neutralSpeed, neutralSpeed);

            Debug.Log("OnStickTwist: error");
            _textMesh.text = "OnStickTwist: eventValue: " + 0;
        }
    }

    public void OnThruster(InputValue value)
    {
        try
        {            
            float eventValue = (-1 * (float)value.Get());
            Debug.Log("Thruster movement: " + eventValue);

            // Move based on thruster position.            
            nextMovement = eventValue;
        }
        catch
        {
            Debug.Log("Thruster movement stopped");

            // Stop movement.
            nextMovement = neutralSpeed;
        }
    }

    public void OnThrusterTwist(InputValue value)
    {
        try
        {
            float eventValue = (float)value.Get();
            Debug.Log("In OnThrusterTwist: " + eventValue);

            // Move based on thruster position.            
            nextHorizontalMovement = eventValue;
        }
        catch
        {
            Debug.Log("In OnThrusterTwist stopped");

            // Stop movement.
            nextHorizontalMovement = neutralSpeed;
        }
    }

    public void OnWeapon7(InputValue value)
    {
        try
        {
            float eventValue = (float)value.Get();
            Debug.Log("In OnWeapon7: " + eventValue);

            // Move based on thruster position.            
            nextVerticalMovement = eventValue;
        }
        catch
        {
            Debug.Log("In OnWeapon7 stopped");

            // Stop movement.
            nextVerticalMovement = neutralSpeed;
        }
    }

    public void OnWeapon8(InputValue value)
    {
        try
        {
            float eventValue = (-1 * (float)value.Get());
            Debug.Log("In OnWeapon8: " + eventValue);

            // Move based on thruster position.            
            nextVerticalMovement = eventValue;
        }
        catch
        {
            Debug.Log("In OnWeapon8 stopped");

            // Stop movement.
            nextVerticalMovement = neutralSpeed;
        }
    }
}
