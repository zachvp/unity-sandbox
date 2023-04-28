using UnityEngine;
using MyBox;

public class MovementFollowTransform : MonoBehaviour
{
    public Transform root;
    public bool usePhysics;

    [ConditionalField("usePhysics")]
    public Rigidbody2D body;

    public void Update()
    {
        transform.position = root.position;
    }
}
