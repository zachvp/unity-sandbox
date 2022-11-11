using UnityEngine;
using Unity.VisualScripting;

public class TriggerPickup : MonoBehaviour
{
    public ConfigTraits.Traits traits;

    // todo: refactor to use same logic as volume trigger
    // or extend volume trigger to store the object it's overlapping....
    public bool isTriggered;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var traitConfig = collision.gameObject.GetComponent<ConfigTraits>();

        if (null != traitConfig && traitConfig.traits == traits)
        {
            EventBus.Trigger(PickupEventUnit.EventHook, gameObject, collision.gameObject);
            isTriggered = true;
        }
    }
}
