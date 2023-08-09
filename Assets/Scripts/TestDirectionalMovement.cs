using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestDirectionalMovement : MonoBehaviour
{
    public float speed = 4;

    void Update()
    {
        if (Keyboard.current.anyKey.isPressed)
        {
            var newPos = transform.position;

            if (Keyboard.current.rightArrowKey.isPressed)
            {
                newPos.x += speed * Time.deltaTime;
            }
            if (Keyboard.current.leftArrowKey.isPressed)
            {
                newPos.x -= speed * Time.deltaTime;
            }

            if (Keyboard.current.upArrowKey.isPressed)
            {
                newPos.y += speed * Time.deltaTime;
            }
            if (Keyboard.current.downArrowKey.isPressed)
            {
                newPos.y -= speed * Time.deltaTime;
            }

            var rounded = CoreUtilities.RoundTo(newPos, CoreConstants.UNIT_ROUND_POSITION);
            Vector2 truncated = transform.position;
            if ((rounded - truncated).sqrMagnitude < CoreConstants.DEADZONE_FLOAT)
            {
                transform.position = newPos;
            }
            else
            {
                transform.position = rounded;
            }
        }
    }
}
