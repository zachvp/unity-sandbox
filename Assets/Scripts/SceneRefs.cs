using System;
using UnityEngine;
using System.Collections.Generic;

public class SceneRefs : CoreSingletonBehavior<SceneRefs>
{
    [NonSerialized]
    public Transform targetGoal;
    [NonSerialized]
    public float distanceGoalToFloor;

    [NonSerialized]
    public Ball ball;

    public void Start()
    {
        var filter = new ContactFilter2D();
        var results = new RaycastHit2D[4];
        var cast = Physics2D.Raycast(targetGoal.position, Vector2.down, filter, results);

        for (var i = 0; i < cast; i++)
        {
            distanceGoalToFloor = Mathf.Max(distanceGoalToFloor, results[i].distance);
        }
    }

    public void Register<T>(ID id, T reference)
    {
        switch (id)
        {
            case ID.TARGET_GOAL:
                Debug.Assert(reference is Transform, $"non-{nameof(Transform)} type for reference: {reference}");
                targetGoal = reference as Transform;
                break;
            case ID.BALL:
                Debug.Assert(reference is Ball, $"non-{nameof(Ball)} type for reference: {reference}");
                ball = reference as Ball;
                break;
            default:
                Debug.LogWarning($"Unhandled case: {id}");
                break;
        }
    }

    public enum ID
    {
        TARGET_GOAL,
        BALL
    }
}