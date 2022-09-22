using UnityEngine;

public class ModifyVelocity : MonoBehaviour
{
    public Rigidbody2D body;

    private float velocityX;
    private float velocityY;

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
}
