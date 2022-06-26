using UnityEngine;
using UnityEngine.InputSystem;

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
    
    // Start is called before the first frame update.
    void Start()
    {        
        negativeRotationSpeed = (-1 * rotationSpeed);
    }

    // Update is called once per frame.
    void Update()
    {
        

        transform.Rotate(nextRotationTransformation);
        // transform.forward = nextMovementTransformation;   
        // transform.forward = new Vector3(transform.position.x, transform.position.y, (transform.position.z + 0.5f));

        transform.position += transform.right * movementSpeed * nextMovement * Time.deltaTime;
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
                nextRotationTransformation = new Vector3(negativeRotationSpeed, neutralSpeed);
            }
            else if (isNeutral(eventValue.x) && isNegative(eventValue.y))
            {
                // Down
                Debug.Log("In Down");
                nextRotationTransformation = new Vector3(rotationSpeed, neutralSpeed);
            }
            else if (isPositive(eventValue.x) && isNeutral(eventValue.y))
            {
                // Right
                Debug.Log("In Right");
                nextRotationTransformation = new Vector3(neutralSpeed, rotationSpeed);
            }
            else if (isNegative(eventValue.x) && isNeutral(eventValue.y))
            {
                // Left
                Debug.Log("In Left");
                nextRotationTransformation = new Vector3(neutralSpeed, negativeRotationSpeed);
            }
            else if (isPositive(eventValue.x) && isPositive(eventValue.y))
            {
                // Up Right
                Debug.Log("In Up Right");
                nextRotationTransformation = new Vector3(negativeRotationSpeed, rotationSpeed);
            }
            else if (isPositive(eventValue.x) && isNegative(eventValue.y))
            {
                // Down Right
                Debug.Log("In Down Right");
                nextRotationTransformation = new Vector3(rotationSpeed, rotationSpeed);
            }
            else if (isNegative(eventValue.x) && isPositive(eventValue.y))
            {
                // Up Left
                Debug.Log("In Up Left");
                nextRotationTransformation = new Vector3(negativeRotationSpeed, negativeRotationSpeed);
            }
            else if (isNegative(eventValue.x) && isNegative(eventValue.y))
            {
                // Down Left
                Debug.Log("In Down Left");
                nextRotationTransformation = new Vector3(rotationSpeed, negativeRotationSpeed);
            }
        }
        catch
        {
            // Stop camera rotation.
            nextRotationTransformation = new Vector3(neutralSpeed, neutralSpeed);
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
}
