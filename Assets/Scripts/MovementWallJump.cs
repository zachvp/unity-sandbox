using UnityEngine;

public class MovementWallJump : MonoBehaviour
{
    public bool Trigger(bool grounded, bool lhsBlocked)
    {
        var result = !grounded && lhsBlocked;

        // todo: trigger event
        //Debug.LogFormat("zvp: state: {0}\tresult: {1}", state, result);

        return result;
    }
}
