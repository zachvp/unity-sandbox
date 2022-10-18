using UnityEngine;
using Unity.VisualScripting;

public class TriggerPickup : MonoBehaviour
{
    public ConfigTraits.Traits traits;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var traitConfig = collision.gameObject.GetComponent<ConfigTraits>();

        if (null != traitConfig && traitConfig.traits == traits)
        {
            Debug.LogFormat("zvp: should emit pickup event");
            EventBus.Trigger(PickupEventUnit.EventHook, gameObject, collision.gameObject);
        }
    }
}
