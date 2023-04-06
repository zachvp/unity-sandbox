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

    // todo: determine the player that scored
    // todo: reset state when player has picked up ball rather than colliding with random things

    public void Awake()
    {
        ballBody.OnAnyColliderEnter += HandleBallColliderEntry;
    }

    public void HandleBallColliderEntry(Collider2D c)
    {
        if (c == top)
        {
            state |= State.TOP;
        }
        else if (c == bottom)
        {
            if (state.HasFlag(State.TOP))
            {
                score++;
                scoreUI.text = score.ToString();
            }

            state = State.NONE;
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
    }
}
