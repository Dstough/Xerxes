public class MinMax
{
    public float Minimum { get; private set; }
    public float Maximum { get; private set; }

    public MinMax()
    {
        Minimum = float.MaxValue;
        Maximum = float.MinValue;
    }

    public void AddValue(float value)
    {
        if (value > Maximum)
            Maximum = value;

        if (value < Minimum)
            Minimum = value;


    }
}
