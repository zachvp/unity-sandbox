using System;
using UnityEngine;
using TMPro;

using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneRefs : Singleton<SceneRefs>
{
    public TargetGoal targetGoal;
    public float distanceGoalToFloor;
    public Ball ball;
    public TextMeshProUGUI scoreUI;

    public void Register<T>(ID id, T reference)
    {
        switch (id)
        {
            case ID.TARGET_GOAL:
                Debug.Assert(reference is TargetGoal, $"non-{nameof(TargetGoal)} type for reference: {reference}");
                targetGoal = reference as TargetGoal;
                break;
            case ID.BALL:
                Debug.Assert(reference is Ball, $"non-{nameof(Ball)} type for reference: {reference}");
                ball = reference as Ball;
                break;
            case ID.UI_SCORE:
                Debug.Assert(reference is TextMeshProUGUI, $"non-{nameof(TextMeshProUGUI)} type for reference: {reference}");
                scoreUI = reference as TextMeshProUGUI;
                break;
            default:
                Debug.LogWarning($"Unhandled case: {id}");
                break;
        }
    }

    public void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public enum ID
    {
        TARGET_GOAL,
        BALL,
        UI_SCORE
    }
}
