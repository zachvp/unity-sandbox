using UnityEngine;
using UnityEngine.Events;

public class MovementHorizontal : MonoBehaviour
{
    public int speed;
    public UnityEvent<int> movementEvent;

    public void Trigger(float input)
    {
        movementEvent.Invoke((int) input * speed);
    }
}
