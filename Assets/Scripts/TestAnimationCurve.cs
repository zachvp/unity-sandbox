using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TestAnimationCurve : MonoBehaviour
{
    public AnimationCurve xCurve;
    public AnimationCurve yCurve;
    public float t;
    public int multiplier = 1;
    public float totalTime;
    public float totalDistance;

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
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            Debug.Log("move left");
            var newPos = transform.position;
            newPos.x = xCurve.Evaluate(t / totalTime) * totalDistance;
            newPos.y = yCurve.Evaluate(t / totalTime) * totalDistance;

            transform.position = newPos;

            if (t > 0)
            {
                t -= Time.deltaTime;
            }
        }

        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            Debug.Log("reset");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
