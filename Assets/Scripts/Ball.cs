using UnityEngine;

public class Ball : MonoBehaviour
{
    public CoreBody body;

    public GameObject pickup;
    public GameObject held;
    public GameObject released;
    public MovementFollowTransform heldMovement;

    public Vector2 assistThrow = new Vector2(1.1f, 1.1f);

    // todo: consider gesture input direction in throw
    // todo: possibly related to above: improve jump shot feasibility

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
    public void Throw(Vector2 baseVelocity, Vector2 inputDirection)
    {
        held.gameObject.SetActive(false);
        released.transform.position = held.transform.position;
        released.SetActive(true);
        //coll.enabled = true;
        body.ResetPhysics();

        var modVelocity = inputDirection * baseVelocity.magnitude;

        modVelocity += assistThrow;

        if (baseVelocity.x < 0)
        {
            modVelocity.x = baseVelocity.x - assistThrow.x;
        }

        //modVelocity.Scale(assistThrow);

        body.Trigger(modVelocity);
    }

    // Activate the pickup object, deactivate the temp released object.
    public void ThrowReset()
    {
        pickup.transform.position = released.transform.position;
        pickup.SetActive(true);
        released.gameObject.SetActive(false);
    }
}
