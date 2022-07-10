using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

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
    private float movementThreshold = 0.01f;
    public TextMeshProUGUI _textMesh;

    private Quaternion originalRotation;
    // private Vector3 originalRotation;
    
    // Start is called before the first frame update.
    void Start()
    {
        originalRotation = transform.localRotation;
        // originalRotation = new Vector3(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z);
        negativeRotationSpeed = (-1 * rotationSpeed);
    }

    // Update is called once per frame.
    void Update()
    {
        // _textMesh.text = "Hello World";

        // Debug.Log("movementThreshold: " + movementThreshold + " eventValue.x: " + eventValue.x + " eventValue.y: " + eventValue.y);
        // Debug.Log("transform.rotation.x: " + transform.rotation.x + " | transform.rotation.y: " + transform.rotation.y + " | transform.rotation.z: " + transform.rotation.z);

        /*
        Debug.Log("originalRotation.x: " + originalRotation.x + " | originalRotation.y: " + originalRotation.y + " | transform.localRotation.z: " + originalRotation.z);

        Debug.Log("transform.localRotation.x: " + transform.localRotation.x + " | transform.localRotation.y: " + transform.localRotation.y + " | transform.localRotation.z: " + transform.localRotation.z);

        Debug.Log("nextRotationTransformation.x: " + nextRotationTransformation.x + " | nextRotationTransformation.y: " + nextRotationTransformation.y + " | nextRotationTransformation.z: " + nextRotationTransformation.z);
        */

        // transform.Rotate(nextRotationTransformation);

        if (nextRotationTransformation.x == 0 && nextRotationTransformation.y == 0 && nextRotationTransformation.z == 0)
        {
            // transform.Rotate(originalRotation);
            // transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, Time.time * 1.0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, Time.deltaTime);
        } else
        {
            transform.Rotate(nextRotationTransformation);

            /*
            Vector3 cappedRotation = transform.rotation.eulerAngles + new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            cappedRotation.y = ClampAngle(cappedRotation.y, -90f, 90f);
            transform.eulerAngles = cappedRotation;
            */
            // transform.eulerAngles = Mathf.Clamp(transform.eulerAngles.y, -90, 90);

            /*
                Make a copy of the current transform object
                Use copy's transfrom.rotate
                Cap the copy's angles
                Set the main object's euler angles to match the copy's
            */

        }

        // transform.RotateAround(nextRotationTransformation);
        // transform.forward = nextMovementTransformation;   
        // transform.forward = new Vector3(transform.position.x, transform.position.y, (transform.position.z + 0.5f));

        transform.position += transform.right * movementSpeed * nextMovement * Time.deltaTime;

        applyYRotationLimit();
    }

    private float ClampAngle(float angle, float from, float to)
    {
        // accepts e.g. -80, 80
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + from);
        return Mathf.Min(angle, to);
    }


    public void OnMove(InputValue value)
    {
        try
        {
            Debug.Log("In OnMove");
            Vector2 eventValue = (Vector2)value.Get();

            // Debug.Log("eventValue.x: " + eventValue.x);
            // Debug.Log("eventValue.y: " + eventValue.y);
            Debug.Log("movementThreshold: " + movementThreshold + " eventValue.x: " + eventValue.x + " eventValue.y: " + eventValue.y);

            /*
            if ((eventValue.x >= (-1 * movementThreshold)) && (eventValue.x <= movementThreshold))
            {
                // neutral
                Debug.Log("In neutral x: " + eventValue.x);
            }
            else if (eventValue.x > movementThreshold)
            {
                // positive
                Debug.Log("In positive x: " + eventValue.x);
            }
            else if (eventValue.x < movementThreshold)
            {
                // neutral
                Debug.Log("In negative x: " + eventValue.x);
            }
            */

            /*
            if (isNeutral(eventValue.x))
            {
                // neutral
                Debug.Log("In neutral x: " + eventValue.x);  
            }
            else if (isPositive(eventValue.x))
            {
                // positive
                Debug.Log("In positive x: " + eventValue.x);
            }
            else if (isNegative(eventValue.x))
            {
                // neutral
                Debug.Log("In negative x: " + eventValue.x);
            }

            if (isNeutral(eventValue.y))
            {
                // neutral
                Debug.Log("In neutral y: " + eventValue.y);
            }
            else if (isPositive(eventValue.y))
            {
                // positive
                Debug.Log("In positive y: " + eventValue.y);
            }
            else if (isNegative(eventValue.y))
            {
                // neutral
                Debug.Log("In negative y: " + eventValue.y);
            }
            */


            /*
            // Rotate camera based on hat direction pressed.
            if (eventValue.x == movementThreshold && eventValue.y > movementThreshold)
            {
                // Up
                Debug.Log("In Up");
                nextRotationTransformation = new Vector3(negativeRotationSpeed, neutralSpeed);                
            }
            else if (eventValue.x == movementThreshold && eventValue.y < movementThreshold)
            {
                // Down
                Debug.Log("In Down");
                nextRotationTransformation = new Vector3(rotationSpeed, neutralSpeed);
            }
            else if (eventValue.x > movementThreshold && eventValue.y == movementThreshold)
            {
                // Right
                Debug.Log("In Right");
                nextRotationTransformation = new Vector3(neutralSpeed, rotationSpeed);
            }
            else if (eventValue.x < movementThreshold && eventValue.y == movementThreshold)
            {
                // Left
                Debug.Log("In Left");
                nextRotationTransformation = new Vector3(neutralSpeed, negativeRotationSpeed);
            }
            else if (eventValue.x > movementThreshold && eventValue.y > movementThreshold)
            {
                // Up Right
                Debug.Log("In Up Right");
                nextRotationTransformation = new Vector3(negativeRotationSpeed, rotationSpeed);
            }
            else if (eventValue.x > 0 && eventValue.y < 0)
            {
                // Down Right
                Debug.Log("In Down Right");
                nextRotationTransformation = new Vector3(rotationSpeed, rotationSpeed);
            }
            else if (eventValue.x < movementThreshold && eventValue.y > movementThreshold)
            {
                // Up Left
                Debug.Log("In Up Left");
                nextRotationTransformation = new Vector3(negativeRotationSpeed, negativeRotationSpeed);
            }
            else if (eventValue.x < movementThreshold && eventValue.y < movementThreshold)
            {
                // Down Left
                Debug.Log("In Down Left");
                nextRotationTransformation = new Vector3(rotationSpeed, negativeRotationSpeed);
            }
            */

            // Rotate camera based on hat direction pressed.
            if (isNeutral(eventValue.x) && isPositive(eventValue.y))
            {
                // Up
                Debug.Log("In Up");
                // nextRotationTransformation = new Vector3(negativeRotationSpeed, neutralSpeed);
                nextRotationTransformation = new Vector3(neutralSpeed, rotationSpeed);
            }
            else if (isNeutral(eventValue.x) && isNegative(eventValue.y))
            {
                // Down
                Debug.Log("In Down");
                // nextRotationTransformation = new Vector3(rotationSpeed, neutralSpeed);                
                nextRotationTransformation = new Vector3(neutralSpeed, negativeRotationSpeed);
            }
            else if (isPositive(eventValue.x) && isNeutral(eventValue.y))
            {
                // Right
                Debug.Log("In Right");
                // nextRotationTransformation = new Vector3(neutralSpeed, rotationSpeed);
                nextRotationTransformation = new Vector3(negativeRotationSpeed, neutralSpeed);
            }
            else if (isNegative(eventValue.x) && isNeutral(eventValue.y))
            {
                // Left
                Debug.Log("In Left");
                // nextRotationTransformation = new Vector3(neutralSpeed, negativeRotationSpeed);
                nextRotationTransformation = new Vector3(rotationSpeed, neutralSpeed);
            }
            else if (isPositive(eventValue.x) && isPositive(eventValue.y))
            {
                // Up Right
                Debug.Log("In Up Right");
                // nextRotationTransformation = new Vector3(negativeRotationSpeed, rotationSpeed);
            }
            else if (isPositive(eventValue.x) && isNegative(eventValue.y))
            {
                // Down Right
                Debug.Log("In Down Right");
                // nextRotationTransformation = new Vector3(rotationSpeed, rotationSpeed);
            }
            else if (isNegative(eventValue.x) && isPositive(eventValue.y))
            {
                // Up Left
                Debug.Log("In Up Left");
                // nextRotationTransformation = new Vector3(negativeRotationSpeed, negativeRotationSpeed);
            }
            else if (isNegative(eventValue.x) && isNegative(eventValue.y))
            {
                // Down Left
                Debug.Log("In Down Left");
                // nextRotationTransformation = new Vector3(rotationSpeed, negativeRotationSpeed);
            }
        }
        catch
        {
            // Stop camera rotation.
            nextRotationTransformation = new Vector3(neutralSpeed, neutralSpeed);
        }
    }

    public void OnStickTwist(InputValue value)
    {
        try
        {
            Debug.Log("In OnStickTwist");
            float eventValue = (float)value.Get();
            _textMesh.text = "OnStickTwist: eventValue: " + eventValue;
            
            // Rotate camera based on hat direction pressed.
            if (isPositive(eventValue))
            {
                // Right
                Debug.Log("In Up");
                nextRotationTransformation = new Vector3(neutralSpeed, neutralSpeed, rotationSpeed);
            }
            else if (isNegative(eventValue))
            {
                // Left
                Debug.Log("In Down");
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

            // Move camera based on thruster position.            
            nextMovement = eventValue;
        }
        catch
        {
            Debug.Log("Thruster movement stopped");

            // Stop camera movement.
            nextMovement = neutralSpeed;
        }
    }

    public bool isNeutral(float value)
    {
        return (value >= (-1 * movementThreshold) && (value <= movementThreshold));
    }

    public bool isPositive(float value)
    {
        return (value >= movementThreshold);
    }

    public bool isNegative(float value)
    {
        return (value <= movementThreshold);
    }

    public void applyYRotationLimit()
    {
        /*
        Vector3 currentPosition = transform.position;
        Debug.Log("currentPosition.y: " + currentPosition.y + " | Mathf.Clamp(currentPosition.y, 0, 45): " + Mathf.Clamp(currentPosition.y, 0, 45));

        currentPosition.y = Mathf.Clamp(currentPosition.y, 0, 45);

        transform.position = currentPosition;
        */

        // Quaternion currentRotation = transform.rotation;        
        // Debug.Log("currentRotation.y: " + currentRotation.y + " | Mathf.Clamp(currentRotation.y, 0, 45): " + Mathf.Clamp(currentRotation.y, 0, 45));

        // _textMesh.text = "currentRotation.y: " + currentRotation.y + " | Mathf.Clamp(currentRotation.y, 0, 45): " + Mathf.Clamp(currentRotation.y, 0, 45);

        // currentRotation.y = Mathf.Clamp(currentRotation.y, 0, 0.5f);

        // transform.rotation = currentRotation;
    }
}
