using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ModifyVelocity : MonoBehaviour
{
    public ScriptableObjectVector2 value;
    public Rigidbody2D body;

    public void Awake()
    {
        Debug.LogFormat("zvp: awake: value: {0}", value.data);
    }

    public void Trigger()
    {
        value.data = Vector2.zero;
        Debug.LogFormat("zvp: SO vec2 value after change: {0}", value.data);

        body.velocity = value.data;
    }
}
