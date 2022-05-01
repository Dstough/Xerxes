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

    public MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        currentColor = color.red;
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {        
        var joystick = Joystick.current;

        if (joystick == null)
            return; // No gamepad connected.

        /*
        if (joystick.trigger.IsPressed())
        {
            // 'Use' code here
            Debug.Log("Trigger Pressed");
        }
        */

        var joystickTwist = joystick.twist.ReadValue();
        if (joystickTwist != 0)
        {
            Debug.Log("Joystick Twist: " + joystickTwist);
        }

        var joystickStick = joystick.stick.ReadValue();
        Vector2 neutralPosition = new Vector2();
        if (joystickStick != neutralPosition)
        {
            Debug.Log("Joystick Stick (x,y): " + joystickStick.ToString());
        }

        if (joystick.hatswitch != null)
        {
            var joystickHatSwitch = joystick.hatswitch.ReadValue();
            if (joystickHatSwitch != neutralPosition)
            {
                Debug.Log("Joystick Hat Switch (x,y): " + joystickStick.ToString());
            }
        }
        

        /*
        var joystickControls_X = joystick.allControls[1];
        if (joystickControls_X.IsPressed())
        {
            Debug.Log("joystickControls_X Pressed");
        }
        */

        // 01: Trigger
        var joystickControls_Trigger = joystick.allControls[1];
        if (joystickControls_Trigger.IsPressed())
        {
            Debug.Log("joystickControls_Trigger Pressed");
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
        }

        // 03: 
        var joystickControls_R3 = joystick.allControls[3];
        if (joystickControls_R3.IsPressed())
        {
            Debug.Log("joystickControls_R3 Pressed");
        }


        // 04: 
        var joystickControls_L3 = joystick.allControls[4];
        if (joystickControls_L3.IsPressed())
        {
            Debug.Log("joystickControls_L3 Pressed");
        }

        // 05: 
        var joystickControls_Square = joystick.allControls[5];
        if (joystickControls_Square.IsPressed())
        {
            Debug.Log("joystickControls_Square Pressed");
        }

        // 06: 
        var joystickControls_X = joystick.allControls[6];
        if (joystickControls_X.IsPressed())
        {
            Debug.Log("joystickControls_X Pressed");
        }

        // 07: 
        var joystickControls_O = joystick.allControls[7];
        if (joystickControls_O.IsPressed())
        {
            Debug.Log("joystickControls_O Pressed");
        }

        // 08: 
        var joystickControls_Triangle = joystick.allControls[8];
        if (joystickControls_Triangle.IsPressed())
        {
            Debug.Log("joystickControls_Triangle Pressed");
        }

        // 09: 
        var joystickControls_R2 = joystick.allControls[9];
        if (joystickControls_R2.IsPressed())
        {
            Debug.Log("joystickControls_R2 Pressed");
        }

        // 10: 
        var joystickControls_L2 = joystick.allControls[10];
        if (joystickControls_L2.IsPressed())
        {
            Debug.Log("joystickControls_L2 Pressed");
        }

        // 11: 
        var joystickControls_Share = joystick.allControls[11];
        if (joystickControls_Share.IsPressed())
        {
            Debug.Log("joystickControls_Share Pressed");
        }

        // 12: 
        var joystickControls_Options = joystick.allControls[12];
        if (joystickControls_Options.IsPressed())
        {
            Debug.Log("joystickControls_Options Pressed");
        }


        /*
        for (int x = 0; x < joystick.allControls.Count; x++)
        {
            if (joystick.allControls[x].IsPressed())
            {
                Debug.Log("Joystick Input Pressed: " + x);
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
            17: 
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
