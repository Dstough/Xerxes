using UnityEngine;
using UnityEngine.InputSystem;

// [ExecuteInEditMode] // Be carefull with this one
[RequireComponent(typeof(MeshRenderer))]
public class TestCameraInputEvents : MonoBehaviour
{
    public float rotationSpeed;
    public float movementSpeed;
    private float neutralSpeed = 0.0f;
    private float negativeRotationSpeed;
    private Vector3 nextRotationTransformation;
    private float nextMovement = 0.0f;
    

    // Start is called before the first frame update.
    void Start()
    {        
        negativeRotationSpeed = (-1 * rotationSpeed);
    }

    // Update is called once per frame.
    void Update()
    {        
        this.transform.Rotate(nextRotationTransformation);
        // this.transform.forward = nextMovementTransformation;   
        // this.transform.forward = new Vector3(this.transform.position.x, this.transform.position.y, (this.transform.position.z + 0.5f));
        this.transform.position += this.transform.forward * movementSpeed * nextMovement * Time.deltaTime;
    }

    public void OnLook(InputValue value)
    {   
        try
        {
            Vector2 eventValue = (Vector2)value.Get();

            // Rotate camera based on hat direction pressed.
            if (eventValue.x == 0 && eventValue.y > 0)
            {
                // Up
                this.nextRotationTransformation = new Vector3(negativeRotationSpeed, neutralSpeed);                
            }
            else if (eventValue.x == 0 && eventValue.y < 0)
            {
                // Down
                this.nextRotationTransformation = new Vector3(rotationSpeed, neutralSpeed);
            }
            else if (eventValue.x > 0 && eventValue.y == 0)
            {
                // Right                
                this.nextRotationTransformation = new Vector3(neutralSpeed, rotationSpeed);
            }
            else if (eventValue.x < 0 && eventValue.y == 0)
            {
                // Left
                this.nextRotationTransformation = new Vector3(neutralSpeed, negativeRotationSpeed);
            }
            else if (eventValue.x > 0 && eventValue.y > 0)
            {
                // Up Right                
                this.nextRotationTransformation = new Vector3(negativeRotationSpeed, rotationSpeed);
            }
            else if (eventValue.x > 0 && eventValue.y < 0)
            {
                // Down Right                                
                this.nextRotationTransformation = new Vector3(rotationSpeed, rotationSpeed);
            }
            else if (eventValue.x < 0 && eventValue.y > 0)
            {
                // Up Left
                this.nextRotationTransformation = new Vector3(negativeRotationSpeed, negativeRotationSpeed);
            }
            else if (eventValue.x < 0 && eventValue.y < 0)
            {
                // Down Left
                this.nextRotationTransformation = new Vector3(rotationSpeed, negativeRotationSpeed);
            }            
        } catch
        {
            // Stop camera rotation.
            this.nextRotationTransformation = new Vector3(neutralSpeed, neutralSpeed);
        }
    }

    public void OnThruster(InputValue value)
    {
        try
        {            
            float eventValue = (-1 * (float)value.Get());
            Debug.Log("Thruster movement: " + eventValue);

            // Move camera based on thruster position.            
            this.nextMovement = eventValue;
        }
        catch
        {
            Debug.Log("Thruster movement stopped");

            // Stop camera movement.
            this.nextMovement = neutralSpeed;
        }
    }   
}
