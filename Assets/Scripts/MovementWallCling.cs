using UnityEngine;

public class MovementWallCling : MonoBehaviour
{
    public bool Trigger(bool lhsBlocked, bool rhsBlocked, bool grounded, short inputAxis, Rigidbody2D rigidbody)
    {
        var leftCondition = inputAxis > 0 && rhsBlocked;
        var rightCondition = inputAxis < 0 && lhsBlocked;
        var result = Mathf.Abs(inputAxis) > 0 && !grounded && rigidbody.velocity.y < 0;

        result &= leftCondition || rightCondition;

        Debug.LogFormat("zvp: result: {0}", result);

        return result;
    }

    public void Cling(Rigidbody2D rigidbody)
    {
        var newVelocity = rigidbody.velocity;

        newVelocity.y = 0;

        rigidbody.velocity = newVelocity;
        rigidbody.Sleep();
    }
}
