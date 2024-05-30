using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float floatAmplitude = 0.5f;
    public float floatFrequency = 1f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Rotate the cube
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Float up and down
        Vector3 tempPosition = startPosition;
        tempPosition.y += Mathf.Sin(Time.time * Mathf.PI * floatFrequency) * floatAmplitude;
        transform.position = tempPosition;
    }
}
