using UnityEngine;

public class ModifyVelocity : MonoBehaviour
{
    public Rigidbody2D body;

    // todo: set X & Y velocity independently
    public void Trigger(int value)
    {
        var result = body.velocity;
        result.x = value;

        // todo: set in FixedUpdate()
        body.velocity = result;
    }

    public void TriggerY(int value)
    {
        var result = body.velocity;
        result.y = value;

        body.velocity = result;
        Debug.LogFormat("zvp: trigger y; value: {0}", value);
    }
}
