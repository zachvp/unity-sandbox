using UnityEngine;
using MyBox;

public class MovementFollowTransform : MonoBehaviour
{
    public Transform root;
    public bool followX = true;
    public bool followY = true;
    public bool followZ = true;
    public bool usePhysics;

    [ConditionalField("usePhysics")]
    public CoreBody body;

    public void Update()
    {
        var modPosition = transform.position;

        if (followX)
        {
            modPosition.x = root.position.x;
        }
        if (followY)
        {
            modPosition.y = root.position.y;
        }
        if (followZ)
        {
            modPosition.z = root.position.z;
        }

        if (usePhysics)
        {
            body.position = modPosition;
        }
        else
        {
            transform.position = modPosition;
        }
    }
}
