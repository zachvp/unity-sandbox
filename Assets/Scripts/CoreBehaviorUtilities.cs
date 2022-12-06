using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UI;

public class CoreBehaviorUtilities : MonoBehaviour
{
    public static bool MultiAND(bool[] propositions)
    {
        Debug.Assert(propositions.Length > 0, "propositions are empty, result is trivially false");

        var result = propositions[0];

        for (var i = 1; i < propositions.Length; i++)
        {
            result &= propositions[i];
        }

        return result;
    }

    public static void Swap(GameObject toActivate, GameObject toDeactivate, UnityProperties properties)
    {
        if (EnumHelper.ContainsFlags(properties, UnityProperties.ACTIVATION))
        {
            toActivate.SetActive(true);
        }

        // check to modify position
        if ((properties | UnityProperties.POSITION) == properties)
        {
            var activatePosition = toActivate.transform.position;

            toActivate.transform.position = toDeactivate.transform.position;
        }

        if (EnumHelper.ContainsFlags(properties, UnityProperties.ACTIVATION))
        {
            toDeactivate.SetActive(false);
        }
    }
}