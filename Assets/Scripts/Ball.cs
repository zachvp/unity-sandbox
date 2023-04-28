using UnityEngine;
using UnityEngine.EventSystems;

public class Ball : MonoBehaviour
{
    public CoreBody body;

    public GameObject pickup;
    public GameObject held;
    public GameObject released;
    public MovementFollowTransform heldMovement;
    public GameObject target;
    public GameObject measureFloorToTarget;

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

        var magicVel = Vector2.zero;
        //if (Mathf.Abs(motor.body.velocity.y) < 100 && !motor.state.down.isTriggered)
        {
            // todo: rather than static number, compute optimal velocity for score
            var toTarget = target.transform.position - released.transform.position;
            var a = new Vector2(-body.body.drag, body.originalGravity * Physics2D.gravity.y - body.body.drag);
            //target.transform.position.

            magicVel.x = 2 * (toTarget.x / 2.55f);
            //magicVel.x = Mathf.Sqrt(Mathf.Pow(baseVelocity.x, 2) - (2 * a.x * toTarget.x));
            magicVel.y = Mathf.Sqrt(Mathf.Pow(baseVelocity.y, 2) - (2 * a.y * toTarget.y));

            if (float.IsNaN(magicVel.y))
            {
                magicVel.y = 0;
                magicVel.x = toTarget.x;
                Debug.LogWarning("magicVelocity.y is NaN");
            }

            //var yFudge = 1 / (toTarget.y / target to floor dist) // todo: 
            var baseFudge = 1 + body.body.drag;
            var yFudge = 0.8f / (Mathf.Max(0.25f, toTarget.y / measureFloorToTarget.transform.localScale.y));

            magicVel.x *= baseFudge;
            magicVel.y *= Mathf.Max(baseFudge, yFudge);
            Debug.Log($"precise jump shot! | {magicVel}; fudge denom: {toTarget.y / measureFloorToTarget.transform.localScale.y}");
        }

        var modVelocity = modDirection * baseVelocity.magnitude;

        modVelocity += modAssistThrow;

        if (inputDirection.x < 0)
        {
            modVelocity.x = baseVelocity.x - assistThrow.x;
        }

        Debug.DrawRay(released.transform.position, modDirection*32, Color.red, 12);

        body.Trigger(magicVel);
    }

    // Activate the pickup object, deactivate the temp released object.
    public void ThrowReset()
    {
        pickup.transform.position = released.transform.position;
        pickup.SetActive(true);
        released.gameObject.SetActive(false);
    }
}
