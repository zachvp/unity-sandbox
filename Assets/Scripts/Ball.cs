using UnityEngine;
using System;
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

    // todo: remove

    public void LateUpdate()
    {
        var modPos = new Vector2Int(Mathf.RoundToInt(body.position.x), Mathf.RoundToInt(body.position.y));
        body.position = modPos;
    }

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

        //Debug.Log($"up: {dotUp}");
        //Debug.Log($"right: {dotRight}");

        var modVelocity = inputDirection * baseVelocity.magnitude * 1.5f + assistThrow;
        var shotMagic = 0f;

        if (inputDirection.x < 0)
        {
            modVelocity.x -= 2*assistThrow.x;
        }

        var dotGesture = Vector2.Dot(inputDirection, Vector2.right + Vector2.up);
        if (Mathf.Abs(dotGesture) > 0.84f)
        {
            shotMagic += 50;
            Debug.Log("shot motion: magic +50");
        }

        var dotRight = Vector2.Dot(Vector2.right, inputDirection);
        if (Mathf.Abs(dotRight) > 0.84f)
        {
            // direct, straight throw
            modVelocity.x = 1;
        }

        // todo: adjust closeness to "magic shot" depending on context
        //      + boost if close to jump peak (+15)
        //      + boost if at jump peak (+30)
        //      + boost if right stick gesture matches shot (+30)
        //      + boost if right stick gesture matches follow thru (+20)
        //      + boost if have dribble stack of TBD (+20)
        if (!motor.state.down.isTriggered)
        {
            if (Mathf.Abs(motor.body.velocity.y) < 50)
            {
                shotMagic += 30;
                Debug.Log("precise! magic +30");
            }
            else if (Mathf.Abs(motor.body.velocity.y) < 100)
            {
                shotMagic += 15;
                Debug.Log("close; magic +15");
            }
        }

        if (shotMagic > 0)
        {
            var magicVel = Vector2.zero;
            var toTarget = target.transform.position - released.transform.position;
            var a = new Vector2(-body.body.drag, body.originalGravity * Physics2D.gravity.y - body.body.drag);

            magicVel.x = 2 * (toTarget.x / 2.55f);
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
            shotMagic = Mathf.Min(100, shotMagic);
            modVelocity = Vector2.Lerp(modVelocity, magicVel, shotMagic / 100);
            //modVelocity = magicVel;
            //Debug.Log($"precise jump shot! | {magicVel}; fudge denom: {toTarget.y / measureFloorToTarget.transform.localScale.y}");
            Debug.Log($"shot magic: {shotMagic / 100}");
        }

        Debug.DrawRay(released.transform.position, modVelocity.normalized*32, Color.yellow, 12);

        //body.Trigger(magicVel);
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
