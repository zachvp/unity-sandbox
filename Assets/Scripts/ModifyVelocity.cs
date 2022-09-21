using UnityEngine;

public class ModifyVelocity : MonoBehaviour
{
    public DataVector2 value; // todo: refactor to 2 floats?
    public Rigidbody2D body;

    // todo: set X & Y velocity independently
    public void Trigger()
    {
        var result = body.velocity;
        result.x = value.data.x;

        // todo: set in FixedUpdate()
        body.velocity = value.data;
        Debug.LogFormat("zvp: trigger x");
    }

    public void TriggerY()
    {
        var result = body.velocity;
        result.y = value.data.y;

        body.velocity = value.data;
        Debug.LogFormat("zvp: trigger y");
    }
}
