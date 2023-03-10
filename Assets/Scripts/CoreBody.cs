using UnityEngine;

// split into separate 'PlatformBody' class?
public class CoreBody : MonoBehaviour
{
    public Rigidbody2D body;
    public float originalGravity;
    public RigidbodyType2D originalType;

    public Vector2 velocity { get { return body.velocity; } set { Trigger(value); } }
    public float rotation { get { return body.rotation; } set { body.rotation = value; } }

    public void Awake()
    {
        originalGravity = body.gravityScale;
    }

    public void Trigger(Vector2 value)
    {
        StartCoroutine(CoreUtilities.PostFixedUpdateTask(() =>
        {
            body.velocity = value;
        }));
    }

    public void TriggerX(float value)
    {
        StartCoroutine(CoreUtilities.PostFixedUpdateTask(() =>
        {
            var result = body.velocity;
            result.x = value;

            body.velocity = result;
        }));
    }

    public void TriggerY(float value)
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
            body.gravityScale = originalGravity;
        }));
    }

    public void SetRotation(float r)
    {
        StartCoroutine(CoreUtilities.PostFixedUpdateTask(() =>
        {
            rotation = r;
        }));
    }

    public void StopPhysics()
    {
        StartCoroutine(CoreUtilities.PostFixedUpdateTask(() =>
        {
            body.bodyType = RigidbodyType2D.Static;
        }));
    }

    public void ResetPhysics()
    {
        StartCoroutine(CoreUtilities.PostFixedUpdateTask(() =>
        {
            body.bodyType = originalType;
        }));
    }
}
