using UnityEngine;

public class Ball : MonoBehaviour
{
    public CoreBody body;

    public GameObject pickup;
    public GameObject held;
    public GameObject released;
    public MovementFollowTransform heldMovement;

    public Vector2 assistThrow = new Vector2(50, 50);

    // todo: possibly related to above: improve jump shot feasibility

    // Activate the held object, deactivate the pickup object.
    public void Grab(Transform holdAnchor)
    {
        heldMovement.root = holdAnchor;
        body.StopVertical();
        body.FreezeRotation();
        
        held.SetActive(true);
        pickup.SetActive(false);
    }

    // Activate the temp release object, deactivate the held object.
    public void Release()
    {
        body.ResetVertical();
        body.UnfreezeRotation();

        released.SetActive(true);
        held.SetActive(false);
        released.transform.position = held.transform.position;
    }

    // Deactivate the released object, activate the pickup object.
    public void ReleaseReset()
    {
        released.SetActive(false);
        pickup.SetActive(true);

        body.ResetVertical();
        body.UnfreezeRotation();

        pickup.transform.position = released.transform.position;
    }

    // Deactivate the held object, activate the temp released object.
    public void Throw(Vector2 baseVelocity, Vector2 inputDirection, PCPlatformMotor motor)
    {
        held.gameObject.SetActive(false);
        released.transform.position = held.transform.position;
        released.SetActive(true);

        body.ResetVertical();
        body.UnfreezeRotation();

        var dotRight = Vector2.Dot(Vector2.right, inputDirection);
        var modDirection = inputDirection;
        var modAssistThrow = assistThrow;

        //Debug.Log($"up: {dotUp}");
        //Debug.Log($"right: {dotRight}");

        if (Mathf.Abs(dotRight) > 0.81f )
        {
            // direct, straight throw
            modDirection = Vector2.right;
        }

        if (Mathf.Abs(motor.body.velocity.y) < 60 && !motor.state.down.isTriggered)
        {
            // todo: rather than static number, compute optimal velocity for score
            modAssistThrow.y += 80;
            Debug.Log("precise jump shot!");
        }

        var modVelocity = modDirection * baseVelocity.magnitude;

        modVelocity += modAssistThrow;

        if (inputDirection.x < 0)
        {
            modVelocity.x = baseVelocity.x - assistThrow.x;
        }

        Debug.DrawRay(released.transform.position, modDirection*32, Color.red, 12);

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
