using UnityEngine;

public class MovementWallJump : MonoBehaviour
{
    public Vector2 jumpVelocity;

    public bool Trigger(bool grounded, bool lhsBlocked, bool rhsBlocked)
    {
        var result = !grounded && (lhsBlocked || rhsBlocked);

        return result;
    }

    public Vector2 ComputeVelocity(short input)
    {
        var result = jumpVelocity;

        jumpVelocity.x *= input;

        return result;
    }
}
