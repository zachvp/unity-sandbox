using UnityEngine;

public class MovementSine : MonoBehaviour
{
    public Transform origin;
    public short scale = 2;
    public float increment = 0.05f;

    private float current = 0;

    public void FixedUpdate()
    {
        var position = origin.position;

        position.x = origin.position.x + Mathf.Sin(current) * scale;
        transform.position = position;

        // Update current.
        current += increment % (2*Mathf.PI);
    }

    public void Reset()
    {
        current = 0;
        transform.position = origin.position;
    }
}
