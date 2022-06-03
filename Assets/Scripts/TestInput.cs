using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// [ExecuteInEditMode] // Be carefull with this one
[RequireComponent(typeof(MeshRenderer))]
public class TestInput : MonoBehaviour
{
    public enum color
    {
        red = 0,
        green = 1,
        blue = 2,
    }

    public color currentColor;

    private MeshRenderer meshRenderer;

    public Material material;
    public Material material2;
    
    // Start is called before the first frame update
    void Start()
    {
        currentColor = color.blue;
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var joystick = Joystick.current;

        if (joystick == null)
            return; // No gamepad connected.
                
        // 00: Stick
        var joystickControls_Stick = joystick.allControls[0];
        if (joystickControls_Stick.IsPressed())
        {
            Debug.Log("Joystick Stick (x,y): " + joystickControls_Stick.ReadValueAsObject());
        }

        // 01: Trigger
        var joystickControls_Trigger = joystick.allControls[1];
        if (joystickControls_Trigger.IsPressed())
        {
            // Debug.Log("joystickControls_Trigger Pressed");
            // Debug.Log("Thruster Trigger: " + joystickControls_Trigger);
            Debug.Log("Thruster Trigger: " + joystickControls_Trigger.ReadValueAsObject().GetType().ToString());
            // Debug.Log("Thruster Trigger: " + joystickControls_Trigger.ToString());
            // Debug.Log("Thruster Trigger: " + joystickControls_Trigger.ReadValueAsObject().GetType().ToString());
            // this.transform.position.x;
            // this.gameObject.transform.position.Set();
            // Vector3.MoveTowards(transform.position, new  + 10);
            // this.gameObject.transform.position.Set()

        }

        // 02: L1
        var joystickControls_L1 = joystick.allControls[2];
        if (joystickControls_L1.IsPressed())
        {
            Debug.Log("joystickControls_L1 Pressed");
            this.meshRenderer.material = material;
        }

        // 03: R3
        var joystickControls_R3 = joystick.allControls[3];
        if (joystickControls_R3.IsPressed())
        {
            Debug.Log("joystickControls_R3 Pressed");
        }

        // 04: L3
        var joystickControls_L3 = joystick.allControls[4];
        if (joystickControls_L3.IsPressed())
        {
            Debug.Log("joystickControls_L3 Pressed");
            this.meshRenderer.material = material2;
        }

        // 05: Square
        var joystickControls_Square = joystick.allControls[5];
        if (joystickControls_Square.IsPressed())
        {
            Debug.Log("joystickControls_Square Pressed");
        }

        // 06: X
        var joystickControls_X = joystick.allControls[6];
        if (joystickControls_X.IsPressed())
        {
            Debug.Log("joystickControls_X Pressed");
        }

        // 07: O
        var joystickControls_O = joystick.allControls[7];
        if (joystickControls_O.IsPressed())
        {
            Debug.Log("joystickControls_O Pressed");
        }

        // 08: Triangle
        var joystickControls_Triangle = joystick.allControls[8];
        if (joystickControls_Triangle.IsPressed())
        {
            Debug.Log("joystickControls_Triangle Pressed");
        }

        // 09: R2
        var joystickControls_R2 = joystick.allControls[9];
        if (joystickControls_R2.IsPressed())
        {
            Debug.Log("joystickControls_R2 Pressed");
        }

        // 10: L2
        var joystickControls_L2 = joystick.allControls[10];
        if (joystickControls_L2.IsPressed())
        {
            Debug.Log("joystickControls_L2 Pressed");
        }

        // 11: Share
        var joystickControls_Share = joystick.allControls[11];
        if (joystickControls_Share.IsPressed())
        {
            Debug.Log("joystickControls_Share Pressed");
        }

        // 12: Options
        var joystickControls_Options = joystick.allControls[12];
        if (joystickControls_Options.IsPressed())
        {
            Debug.Log("joystickControls_Options Pressed");
        }

        // 13: Thruster Tilt
        var joystickControls_Thruster_Tilt = joystick.allControls[13];
        if (joystickControls_Thruster_Tilt.IsPressed())
        {   
            // Debug.Log("Thruster Tilt: " + joystickControls_Thruster_Tilt.ReadValueAsObject().ToString());
            Debug.Log("Thruster Tilt: " + joystickControls_Thruster_Tilt.ToString());
        }

        // 17: Twist
        var joystickControls_Twist = joystick.allControls[17];
        if (joystickControls_Twist.IsPressed())
        {
            // Debug.Log("Joystick Twist: " + joystickControls_Twist.ReadValueAsObject().ToString());
            Debug.Log("Joystick Twist: " + joystickControls_Twist.ToString());
        }
        
        // 18: Thruster
        var joystickControls_Thruster = joystick.allControls[18];
        if (joystickControls_Thruster.IsPressed())
        {
            // Debug.Log("Thruster: " + joystickControls_Thruster.ReadValueAsObject().ToString());
            Debug.Log("Thruster: " + joystickControls_Thruster.ReadValueAsObject().GetType().ToString());
            // Debug.Log("Thruster: " + joystickControls_Thruster.ToString());
        }

        // 19: Hat Switch 
        var joystickControls_Hat_Switch = joystick.allControls[19];
        if (joystickControls_Hat_Switch.IsPressed())
        {
            // Debug.Log("joystickControls_19 Pressed");
            Debug.Log("joystickControls_Hat_Switch (x,y): " + joystickControls_Hat_Switch.ReadValueAsObject().ToString());
            // Debug.Log("Thruster Hat: " + joystickControls_Hat_Switch.ReadValueAsObject().GetType().ToString());
        }


        /*
        // Test method to detect control inputs.
        for (int x = 0; x < joystick.allControls.Count; x++)
        {
            if (joystick.allControls[x].IsPressed())
            {
                // Debug.Log("Joystick Input Pressed: " + x);
                Debug.Log("Joystick Input [" + x + "] Pressed: " + joystick.allControls[x].ReadValueAsObject().ToString());
            }
        }
        */

        /*
            [joystick.allControls[*] Mappings]:
            00: Joystick position
            01: Trigger
            02: L1
            03: R3
            04: L3
            05: Square
            06: X
            07: O
            08: Triangle
            09: R2
            10: L2
            11: Share
            12: Options
            13: Tilt
            14: 
            15: Thruster
            16: 
            17: Twist
            18: 
            19:
            20: 
            21: 
            22: 
            23: 
            24: 
            25: 
            26: 
            27: 
            28: 
            29: 
            30: 
            31: 
            32: 
            33: 
            34: 
            35: 
            36: 
            37: 
            38: 
            39: 
        */
    }
}
