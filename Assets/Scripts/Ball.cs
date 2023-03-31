using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    public CoreBody body;

    public GameObject pickup;
    public GameObject held;
    public GameObject released;
    public MovementFollowTransform heldMovement;

    public float assistUpward = 50;

    // Activate the held object, deactivate the pickup object.
    public void Grab(Transform holdAnchor)
    {
        heldMovement.root = holdAnchor;
        body.StopPhysics();

        held.SetActive(true);
        pickup.SetActive(false);
    }

    // Activate the temp release object, deactivate the held object.
    public void Release()
    {
        body.ResetPhysics();
        released.SetActive(true);
        held.SetActive(false);
        released.transform.position = held.transform.position;
    }

    // Deactivate the released object, activate the pickup object.
    public void ReleaseReset()
    {
        released.SetActive(false);
        pickup.SetActive(true);
        body.ResetPhysics();
        //coll.enabled = true;
        pickup.transform.position = released.transform.position;
    }

    // Deactivate the held object, activate the temp released object.
    public void Throw(Vector2 baseVelocity)
    {
        held.gameObject.SetActive(false);
        released.transform.position = held.transform.position;
        released.SetActive(true);
        //coll.enabled = true;
        body.ResetPhysics();

        baseVelocity.y += assistUpward;
        body.Trigger(baseVelocity);
    }

    // Activate the pickup object, deactivate the temp released object.
    public void ThrowReset()
    {
        pickup.transform.position = released.transform.position;
        pickup.SetActive(true);
        released.gameObject.SetActive(false);
    }
}
