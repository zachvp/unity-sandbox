using UnityEngine;
using MyBox;

public class MovementFollowTransform : MonoBehaviour
{
    public Transform root;
    public bool usePhysics;

    [ConditionalField("usePhysics")]
    public CoreBody body;

    public void Update()
    {
        if (usePhysics)
        {
            body.position = root.position;
        }
        else
        {
            transform.position = root.position;
        }
    }
}
