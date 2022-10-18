using UnityEngine;

public class MovementFollowTransform : MonoBehaviour
{
    public Transform root;

    public void Update()
    {
        transform.position = root.position;
    }
}
