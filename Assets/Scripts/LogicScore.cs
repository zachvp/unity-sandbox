using UnityEngine;
using System;
using TMPro;

public class LogicScore : MonoBehaviour
{
    public Collider2D top;
    public Collider2D bottom;
    public Collider2D scoreZone;
    public CoreBody ballBody;
    public Collider2D ballCollider;
    public State state;
    public TextMeshProUGUI scoreUI;
    public int score;

    public void Awake()
    {
        ballBody.OnAnyColliderEnter += HandleBallColliderEntry;
    }

    public void HandleBallColliderEntry(Collider2D c)
    {
        if (c == top)
        {
            if (!state.HasFlag(State.BOTTOM))
            {
                state |= State.TOP;
            }
            else
            {
                Debug.Log("INVALID SHOT: score from under the basket");
            }
        }
        else if (c == bottom)
        {
            if (state.HasFlag(State.TOP))
            {
                score++;
                scoreUI.text = score.ToString();
            }

            state = State.BOTTOM;
        }

        if (!ballCollider.IsTouching(scoreZone))
        {
            state = State.NONE;
        }
    }

    [Flags]
    public enum State
    {
        NONE = 0,
        TOP = 1 << 0,
        BOTTOM = 1 << 1
    }
}
