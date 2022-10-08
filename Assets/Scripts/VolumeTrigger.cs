using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class VolumeTrigger : MonoBehaviour
{
    public bool isActive;
    public LayerMask mask;
    new public Collider2D collider;
    public UnityEvent<bool> onUpdate;

    public void OnTriggerEnter2D(Collider2D other)
    {
        UpdateState();
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        UpdateState();
    }

    private void UpdateState()
    {
        var filter = new ContactFilter2D();
        var colliders = new Collider2D[1];

        filter.useLayerMask = true;
        filter.layerMask = mask;

        var result = collider.OverlapCollider(filter, colliders);

        isActive = result > 0;
        onUpdate.Invoke(isActive);
    }
}
