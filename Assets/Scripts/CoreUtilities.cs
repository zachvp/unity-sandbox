using UnityEngine;

public class CoreUtilities : MonoBehaviour
{
    public static bool MultiAND(bool[] propositions)
    {
        if (propositions.Length < 1)
        {
            return false;
        }

        var result = propositions[0];

        for (var i = 1; i < propositions.Length; i++)
        {
            result &= propositions[i];
        }

        return result;
    }
}
