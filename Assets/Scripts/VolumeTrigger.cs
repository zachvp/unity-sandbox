using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using Unity.VisualScripting;

// todo: rename to trigger volume
public class VolumeTrigger : MonoBehaviour
{
    public bool isActive;
    public LayerMask mask;
    new public Collider2D collider;
    public short triggerEntryCount;

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

        isActive = collider.OverlapCollider(filter, colliders) > 0;
    }
}
