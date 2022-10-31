namespace Assets.Scripts
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    static class JoystickInputCalibration
    {
        public static bool isNeutral(float value, float movementThreshold)
        {
            return (value >= (-1 * movementThreshold) && (value <= movementThreshold));
        }

        public static bool isPositive(float value, float movementThreshold)
        {            
            bool result = isNeutral(value, movementThreshold) ? false : (value >= movementThreshold);
            return result;
        }

        public static bool isNegative(float value, float movementThreshold)
        {
            bool result = isNeutral(value, movementThreshold) ? false : (value <= movementThreshold);
            return result;
        }

        public static Vector3 getNextInput(InputValue value, bool invertAxis, float movementThreshold, float movementThresholdDiagonal, float neutralSpeed, float rotationSpeed, float negativeRotationSpeed)
        {
            try
            {
                Debug.Log("In OnMove");
                Vector2 eventValue = (Vector2)value.Get();

                Debug.Log("movementThreshold: " + movementThreshold + " eventValue.x: " + eventValue.x + " eventValue.y: " + eventValue.y);

                // Rotate based on direction pressed.
                if (isNeutral(eventValue.x, movementThreshold) && isPositive(eventValue.y, movementThreshold))
                {
                    // Up
                    Debug.Log("In Up");
                    if(invertAxis)
                    {
                        return new Vector3(neutralSpeed, rotationSpeed);
                    } else
                    {
                        return new Vector3(neutralSpeed, negativeRotationSpeed);
                    }
                }

                if (isNeutral(eventValue.x, movementThreshold) && isNegative(eventValue.y, movementThreshold))
                {
                    // Down
                    Debug.Log("In Down");                    
                    if (invertAxis)
                    {
                        return new Vector3(neutralSpeed, negativeRotationSpeed);                        
                    }
                    else
                    {
                        return new Vector3(neutralSpeed, rotationSpeed);
                    }
                }

                if (isPositive(eventValue.x, movementThreshold) && isNeutral(eventValue.y, movementThreshold))
                {
                    // Right
                    Debug.Log("In Right");
                    return new Vector3(negativeRotationSpeed, neutralSpeed);
                }

                if (isNegative(eventValue.x, movementThreshold) && isNeutral(eventValue.y, movementThreshold))
                {
                    // Left
                    Debug.Log("In Left");
                    return new Vector3(rotationSpeed, neutralSpeed);
                }

                if (isPositive(eventValue.x, movementThresholdDiagonal) && isPositive(eventValue.y, movementThresholdDiagonal))
                {
                    // Up Right
                    Debug.Log("In Up Right");
                    if (invertAxis)
                    {
                        return new Vector3(negativeRotationSpeed, rotationSpeed);                        
                    }
                    else
                    {
                        return new Vector3(rotationSpeed, rotationSpeed);
                    }
                }

                if (isPositive(eventValue.x, movementThresholdDiagonal) && isNegative(eventValue.y, movementThresholdDiagonal))
                {
                    // Down Right
                    Debug.Log("In Down Right");
                    if (invertAxis)
                    {
                        return new Vector3(negativeRotationSpeed, negativeRotationSpeed);
                    }
                    else
                    {
                        return new Vector3(rotationSpeed, negativeRotationSpeed);
                    }
                }

                if (isNegative(eventValue.x, movementThresholdDiagonal) && isPositive(eventValue.y, movementThresholdDiagonal))
                {
                    // Up Left
                    Debug.Log("In Up Left");
                    if (invertAxis)
                    {
                        return new Vector3(rotationSpeed, rotationSpeed);
                    }
                    else
                    {
                        return new Vector3(negativeRotationSpeed, rotationSpeed);
                    }
                }

                if (isNegative(eventValue.x, movementThresholdDiagonal) && isNegative(eventValue.y, movementThresholdDiagonal))
                {
                    // Down Left
                    Debug.Log("In Down Left");
                    if (invertAxis)
                    {
                        return new Vector3(rotationSpeed, negativeRotationSpeed);                        
                    }
                    else
                    {
                        return new Vector3(negativeRotationSpeed, negativeRotationSpeed);                        
                    }                    
                }
            }
            catch
            {
                // Stop rotation.
                return new Vector3(neutralSpeed, neutralSpeed);
            }

            return new Vector3(neutralSpeed, neutralSpeed);
        }
    }
}
