using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ModifyVelocity : MonoBehaviour
{
    public DataVector2 value;
    public Rigidbody2D body;

    public void Awake()
    {
        Debug.LogFormat("zvp: awake: value: {0}", value.data);
    }

    public void Trigger()
    {
        body.velocity = value.data;
    }
}
