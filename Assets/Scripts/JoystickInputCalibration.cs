namespace Assets.Scripts
{
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

            // return (value >= movementThreshold);
        }

        public static bool isNegative(float value, float movementThreshold)
        {
            bool result = isNeutral(value, movementThreshold) ? false : (value <= movementThreshold);
            return result;

            // return (value <= movementThreshold);
        }
    }
}
