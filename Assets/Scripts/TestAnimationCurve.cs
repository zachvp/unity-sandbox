using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TestAnimationCurve : MonoBehaviour
{
    public Transform target;
    public AnimationCurve xCurve;
    public AnimationCurve yCurve;

    public float t;
    public int multiplier = 1;
    public float totalTime;
    public float totalDistance;
    public float counter;
    public Vector3 toTarget;

    public void Start()
    {
        toTarget = target.position - transform.position;
    }

    public void Update()
    {
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            Debug.Log("move right");
            var newPos = transform.position;
            newPos.x = xCurve.Evaluate(t / totalTime) * totalDistance;
            newPos.y = yCurve.Evaluate(t / totalTime) * totalDistance;

            transform.position = newPos;

            t += Time.deltaTime * multiplier;

            if (t < 0 || t > totalTime)
            {
                multiplier *= -1;
            }
        }
        if (Keyboard.current.spaceKey.isPressed)
        {
            Debug.Log("move to target");
            var newPos = transform.position;

            newPos.x = xCurve.Evaluate(t / totalTime) * toTarget.x;
            newPos.y = yCurve.Evaluate(t / totalTime) * toTarget.y;

            transform.position = newPos;

            t = Mathf.PingPong(counter, totalTime);
            counter += Time.deltaTime * multiplier;

            //t += Time.deltaTime * multiplier;

            //if (t < 0 || t > totalTime)
            //{
            //    multiplier *= -1;
            //}
        }
        if (Keyboard.current.upArrowKey.isPressed)
        {
            transform.rotation = Quaternion.AngleAxis(t, Vector3.forward);

            t += Time.deltaTime * multiplier;

            if (t < 0 || t > totalTime)
            {
                multiplier *= -1;
            }
        }
        if (Keyboard.current.pKey.isPressed)
        {
            //float newPositionX = Mathf.PingPong(Time.time * speed, maxXPosition * 2) - maxXPosition;
            var newX = Mathf.PingPong(t * multiplier, 10);
            var newPos = transform.position;
            newPos.x = newX;

            transform.position = newPos;
            t += Time.deltaTime;
        }

        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            Debug.Log("reset");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
