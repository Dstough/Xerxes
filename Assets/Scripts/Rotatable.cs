using UnityEngine;

public class Rotatable : MonoBehaviour
{
    [Range(0f, 1f)]
    public float xSpeed = 0;
    [Range(0f, 1f)]
    public float ySpeed = 0;
    [Range(0f, 1f)]
    public float zSpeed = 0;
    [Range(0f, 100f)]
    public float gloabalSpeed = 0;

    private Vector3 rotationDirection;
    private float smooth;

    void Update()
    {
        rotationDirection = new Vector3(xSpeed, ySpeed, zSpeed);
        smooth = Time.deltaTime * gloabalSpeed;
        transform.Rotate(rotationDirection * smooth);
    }
}
