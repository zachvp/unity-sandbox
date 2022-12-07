using UnityEngine;

// todo: rename to CoreBody
public class CoreBody : MonoBehaviour
{
    public Rigidbody2D body;
    public float gravityScaleOriginal;

    public void Awake()
    {
        gravityScaleOriginal = body.gravityScale;
    }

    public void Trigger(Vector2 value)
    {
        body.velocity = value;
    }

    public void TriggerX(short value)
    {
        var result = body.velocity;
        result.x = value;

        // todo: set in FixedUpdate()
        body.velocity = result;
    }

    public void TriggerY(short value)
    {
        var result = body.velocity;
        result.y = value;

        body.velocity = result;
    }

    public void StopVertical()
    {
        var newVelocity = body.velocity;

        newVelocity.y = 0;
        body.gravityScale = 0;

        body.velocity = newVelocity;
    }

    public void Reset()
    {
        body.gravityScale = gravityScaleOriginal;
    }

    public Vector2 GetVelocity()
    {
        return body.velocity;
    }
}
