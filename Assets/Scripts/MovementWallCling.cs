using UnityEngine;

public class MovementWallCling : MonoBehaviour
{
    public bool Trigger(bool lhsBlocked, bool rhsBlocked, bool grounded, short inputAxis)
    {
        var result = Mathf.Abs(inputAxis) > 0 && !grounded && (lhsBlocked || rhsBlocked);

        Debug.LogFormat("zvp: result: {0}", result);

        return result;
    }
}
