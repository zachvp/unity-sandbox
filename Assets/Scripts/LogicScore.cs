using UnityEngine;
using System;
using TMPro;

public class LogicScore : MonoBehaviour
{
    public BoxCollider2D top;
    public BoxCollider2D bottom;
    public BoxCollider2D backboard;
    public GameObject rim;
    public CoreBody ballBody;
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
            state |= State.TOP;
        }
        else if (c == bottom)
        {
            if (state.HasFlag(State.TOP))
            {
                Debug.Log("SCORE!");
                score++;
                scoreUI.text = score.ToString();
            }

            state = State.NONE;
        }
        else if (c != backboard && c.gameObject != rim)
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
