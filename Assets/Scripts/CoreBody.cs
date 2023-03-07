using UnityEngine;

public class CoreBody : MonoBehaviour
{
    public Rigidbody2D body;
    public float gravityScaleOriginal;
    public Vector2 velocity { get { return body.velocity; } set { Trigger(value); } }

    public void Awake()
    {
        gravityScaleOriginal = body.gravityScale;
    }

    public void Trigger(Vector2 value)
    {
        StartCoroutine(CoreUtilities.PostFixedUpdateTask(() =>
        {
            body.velocity = value;
        }));
    }

    public void TriggerX(short value)
    {
        StartCoroutine(CoreUtilities.PostFixedUpdateTask(() =>
        {
            var result = body.velocity;
            result.x = value;

            body.velocity = result;
        }));
    }

    public void TriggerY(short value)
    {
        StartCoroutine(CoreUtilities.PostFixedUpdateTask(() =>
        {
            var result = body.velocity;
            result.y = value;

            body.velocity = result;
        }));
    }

    public void StopVertical()
    {
        StartCoroutine(CoreUtilities.PostFixedUpdateTask(() =>
        {
            var newVelocity = body.velocity;

            newVelocity.y = 0;
            body.gravityScale = 0;

            body.velocity = newVelocity;
        }));
    }

    public void Reset()
    {
        StartCoroutine(CoreUtilities.PostFixedUpdateTask(() =>
        {
            body.gravityScale = gravityScaleOriginal;
        }));
    }

    public Vector2 GetVelocity()
    {
        return body.velocity;
    }
}
