using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TestAnimationCurve : MonoBehaviour
{
    public AnimationCurve curve;
    public float t;
    public float totalTime;
    public float totalDistance;
    public bool isLeft;

    public void Update()
    {
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            Debug.Log("move right");
            var newPos = transform.position;
            newPos.x = curve.Evaluate(t / totalTime) * totalDistance;

            transform.position = newPos;

            if (t < totalTime)
            {
                t += Time.deltaTime;
            }
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            Debug.Log("move left");
            var newPos = transform.position;
            newPos.x = curve.Evaluate(t / totalTime) * totalDistance;

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
