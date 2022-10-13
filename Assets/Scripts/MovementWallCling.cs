using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class MovementWallCling : MonoBehaviour
{
    public bool Trigger(bool lhsBlocked, bool rhsBlocked, bool grounded, short inputAxis, Rigidbody2D rigidbody)
    {
        var rightCondition = inputAxis > 0 && rhsBlocked;
        var leftCondition = inputAxis < 0 && lhsBlocked;
        var result = Mathf.Abs(inputAxis) > 0 && !grounded && rigidbody.velocity.y < 1;

        result &= leftCondition || rightCondition;

        EventBus.Trigger<bool>(WallClingEventUnit.EventHook, gameObject, result);

        return result;
    }
}
