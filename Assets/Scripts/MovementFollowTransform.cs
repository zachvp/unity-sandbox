using UnityEngine;

public class MovementFollowTransform : MonoBehaviour
{
    public Transform root;
    public bool usePhysics;
    
    public Rigidbody2D body;

    public void Update()
    {
        transform.position = root.position;
    }
}
