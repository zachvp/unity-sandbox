using UnityEngine;

public class Ball : MonoBehaviour
{
    public CoreBody pickup;
    public CoreBody held;
    public CoreBody released;

    public MovementFollowTransform heldMovement;

    // Activate the held object, deactivate the pickup object.
    public void Grab(Transform holdAnchor)
    {
        heldMovement.root = holdAnchor;

        held.gameObject.SetActive(true);
        held.rotation = pickup.rotation;
        pickup.gameObject.SetActive(false);
    }

    // Activate the temp release object, deactivate the held object.
    public void Release()
    {
        released.gameObject.SetActive(true);
        held.gameObject.SetActive(false);
        released.transform.position = held.transform.position;
        released.rotation = held.rotation;
    }

    // Deactivate the released object, activate the pickup object.
    public void ReleaseReset()
    {
        released.gameObject.SetActive(false);
        pickup.gameObject.SetActive(true);
        pickup.transform.position = released.transform.position;
        pickup.rotation = released.rotation;
    }

    // Deactivate the held object, activate the temp released object.
    public void Throw(Vector2 baseVelocity)
    {
        held.gameObject.SetActive(false);
        released.transform.position = held.transform.position;
        released.gameObject.SetActive(true);
        released.Trigger(baseVelocity);
    }

    // Activate the pickup object, deactivate the temp released object.
    public void ThrowReset()
    {
        pickup.transform.position = released.transform.position;
        pickup.gameObject.SetActive(true);
        pickup.Trigger(released.velocity);
        released.gameObject.SetActive(false);
    }
}
