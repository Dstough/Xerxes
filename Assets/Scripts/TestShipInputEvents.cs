using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Assets.Scripts;

// [ExecuteInEditMode] // Be carefull with this one
[RequireComponent(typeof(MeshRenderer))]
public class TestShipInputEvents : MonoBehaviour
{    
    private float neutralSpeed = 0.0f;
    private float negativeRotationSpeed;
    private Vector3 nextRotationTransformation;
    private float nextMovement = 0.0f;
    private float nextVerticalMovement = 0.0f;
    private float nextHorizontalMovement = 0.0f;
    private float movementThreshold = 0.01f;
    private float movementThresholdDiagonal = 0.1f;

    public TextMeshProUGUI _textMesh;


    public float rotationSpeed;        
    public float accelerationRate;    
    public float resistance;

    public float topDepthSpeed;
    private float depthSpeed;
    private float currentDepthControlValue = 0.0f;

    public float topVerticalSpeed;
    private float verticalSpeed;
    private float currentVerticalControlValue = 0.0f;

    public float topHorizontalSpeed;
    private float horizontalSpeed;
    private float currentHorizontalControlValue = 0.0f;
    

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

        UpdateMovementSpeed(currentDepthControlValue, ref depthSpeed, ref topDepthSpeed);
        UpdateMovementSpeed(currentVerticalControlValue, ref verticalSpeed, ref topVerticalSpeed);
        UpdateMovementSpeed(currentHorizontalControlValue, ref horizontalSpeed, ref topHorizontalSpeed);

        // Forward / Backward movement.
        // transform.position += transform.right * movementSpeed * nextMovement * Time.deltaTime;
        transform.position += transform.right * depthSpeed * Time.deltaTime;

        // Horizontal movement.
        // transform.position += transform.up * movementSpeed * nextHorizontalMovement * Time.deltaTime;
        transform.position += transform.up * horizontalSpeed * Time.deltaTime;

        // Vertical movement.
        // transform.position += transform.forward * movementSpeed * nextVerticalMovement * Time.deltaTime;
        transform.position += transform.forward * verticalSpeed * Time.deltaTime;

        // Update HUD
        _textMesh.text = "Rotation: " + transform.rotation + "\n" 
            + "nextMovement: " + nextMovement + "\n"
            + "currentThrusterValue: " + currentDepthControlValue + "\n"
            + "depthSpeed: " + depthSpeed + "\n"
            + "verticalSpeed: " + verticalSpeed + "\n"
            + "horizontalSpeed: " + horizontalSpeed;
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
            // nextMovement = eventValue;

            //
            // UpdateMovementSpeed(eventValue);
            currentDepthControlValue = eventValue;
        }
        catch
        {
            Debug.Log("Thruster movement stopped");

            // Stop movement.
            // nextMovement = neutralSpeed;

            //
            // UpdateMovementSpeed(neutralSpeed);
            currentDepthControlValue = neutralSpeed;
        }
    }

    public void UpdateMovementSpeed(float adjustment, ref float speed, ref float topSpeed)
    {
        if (adjustment > 0.0f)
        {
            Accelerate((adjustment * accelerationRate), ref speed, ref topSpeed);
        } else if (adjustment == 0.0f)
        {
            Decelerate(ref speed);
        } else
        {
            Reverse((adjustment * accelerationRate * -1), ref speed, ref topSpeed);
        }
    }

    public void Accelerate(float adjustment, ref float speed, ref float topSpeed)
    {
        if (speed != topSpeed)
        {
            if ((speed + adjustment) > topSpeed)
            {
                speed = topSpeed;
            } else
            {
                speed += adjustment;
            }
        }
    }

    public void Decelerate(ref float speed)
    {
        if (speed != neutralSpeed)
        {
            if (speed > neutralSpeed)
            {
                if ((speed - resistance) < neutralSpeed)
                {
                    speed = neutralSpeed;
                }
                else
                {
                    speed -= resistance;
                }
            } else
            {
                if ((speed + resistance) > neutralSpeed)
                {
                    speed = neutralSpeed;
                }
                else
                {
                    speed += resistance;
                }
            }
        }
    }

    public void Reverse(float adjustment, ref float speed, ref float topSpeed)
    {
        float topReverseMovementSpeed = (-1 * topSpeed);
        if (speed != topReverseMovementSpeed)
        {
            if ((speed - adjustment) < topReverseMovementSpeed)
            {
                speed = topReverseMovementSpeed;
            }
            else
            {
                speed -= adjustment;
            }
        }
    }

    public void OnThrusterTwist(InputValue value)
    {
        try
        {
            float eventValue = (float)value.Get();
            Debug.Log("In OnThrusterTwist: " + eventValue);

            // Move based on thruster position.            
            // nextHorizontalMovement = eventValue;

            //
            currentHorizontalControlValue = eventValue;
        }
        catch
        {
            Debug.Log("In OnThrusterTwist stopped");

            // Stop movement.
            // nextHorizontalMovement = neutralSpeed;

            //
            currentHorizontalControlValue = neutralSpeed;
        }
    }

    public void OnWeapon7(InputValue value)
    {
        try
        {
            float eventValue = (float)value.Get();
            Debug.Log("In OnWeapon7: " + eventValue);

            // Move based on thruster position.            
            // nextVerticalMovement = eventValue;

            //
            currentVerticalControlValue = eventValue;
        }
        catch
        {
            Debug.Log("In OnWeapon7 stopped");

            // Stop movement.
            // nextVerticalMovement = neutralSpeed;

            //
            currentVerticalControlValue = neutralSpeed;
        }
    }

    public void OnWeapon8(InputValue value)
    {
        try
        {
            float eventValue = (-1 * (float)value.Get());
            Debug.Log("In OnWeapon8: " + eventValue);

            // Move based on thruster position.            
            // nextVerticalMovement = eventValue;

            //
            currentVerticalControlValue = eventValue;            
        }
        catch
        {
            Debug.Log("In OnWeapon8 stopped");

            // Stop movement.
            // nextVerticalMovement = neutralSpeed;

            //
            currentVerticalControlValue = neutralSpeed;
        }
    }
}
