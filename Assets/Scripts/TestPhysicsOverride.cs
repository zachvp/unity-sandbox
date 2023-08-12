using UnityEngine;

public class TestPhysicsOverride : MonoBehaviour
{
    public Collider2D attached;
    public Collider2D ignore;

    public VarWatch<bool> ignored;

    public void Awake()
    {
        if (attached && ignore)
        {
            ignored.onChanged += (oldVal, newVal) =>
            {
                Physics2D.IgnoreCollision(attached, ignore, newVal);
            };
        }
    }

    public void Update()
    {
        ignored.Update(ignored.value);
    }
}
