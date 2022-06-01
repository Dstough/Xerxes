using UnityEngine;
using UnityEngine.InputSystem;

// [ExecuteInEditMode] // Be carefull with this one
[RequireComponent(typeof(MeshRenderer))]
public class TestCameraInputEvents : MonoBehaviour
{
    public float rotationSpeed;
    private float neutralRotationSpeed = 0.0f;
    private float negativeRotationSpeed;
    private Vector3 nextTransformation;


    // Start is called before the first frame update.
    void Start()
    {        
        negativeRotationSpeed = (-1 * rotationSpeed);                 
    }

    // Update is called once per frame.
    void Update()
    {        
        this.transform.Rotate(nextTransformation);
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
                this.nextTransformation = new Vector3(negativeRotationSpeed, neutralRotationSpeed);                
            }
            else if (eventValue.x == 0 && eventValue.y < 0)
            {
                // Down
                this.nextTransformation = new Vector3(rotationSpeed, neutralRotationSpeed);
            }
            else if (eventValue.x > 0 && eventValue.y == 0)
            {
                // Right                
                this.nextTransformation = new Vector3(neutralRotationSpeed, rotationSpeed);
            }
            else if (eventValue.x < 0 && eventValue.y == 0)
            {
                // Left
                this.nextTransformation = new Vector3(neutralRotationSpeed, negativeRotationSpeed);
            }
            else if (eventValue.x > 0 && eventValue.y > 0)
            {
                // Up Right                
                this.nextTransformation = new Vector3(negativeRotationSpeed, rotationSpeed);
            }
            else if (eventValue.x > 0 && eventValue.y < 0)
            {
                // Down Right                                
                this.nextTransformation = new Vector3(rotationSpeed, rotationSpeed);
            }
            else if (eventValue.x < 0 && eventValue.y > 0)
            {
                // Up Left
                this.nextTransformation = new Vector3(negativeRotationSpeed, negativeRotationSpeed);
            }
            else if (eventValue.x < 0 && eventValue.y < 0)
            {
                // Down Left
                this.nextTransformation = new Vector3(rotationSpeed, negativeRotationSpeed);
            }            
        } catch
        {
            // Stop camera rotation.
            this.nextTransformation = new Vector3(neutralRotationSpeed, neutralRotationSpeed);
        }
    }   
}
