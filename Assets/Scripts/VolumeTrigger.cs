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
    public Collider2D[] overlappingObjects;

#if DEBUG
    public void Awake()
    {
        Debug.AssertFormat(overlappingObjects.Length > 0, "non-zero length required for trigger");
    }
#endif

    public void OnTriggerEnter2D(Collider2D other)
    {
        ClearState();
        UpdateState();
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        ClearState();
        UpdateState();
    }

    private void UpdateState()
    {
        var filter = new ContactFilter2D();

        filter.useLayerMask = true;
        filter.layerMask = mask;

        isActive = collider.OverlapCollider(filter, overlappingObjects) > 0;
    }

    private void ClearState()
    {
        for (var i = 0; i < overlappingObjects.Length; i++)
        {
            overlappingObjects[i] = null;
        }
    }
}
