using UnityEngine;

public class ModifyVelocity : MonoBehaviour
{
    public DataVector2 value;
    public Rigidbody2D body;

    public void Trigger()
    {
        body.velocity = value.data;
    }
}
