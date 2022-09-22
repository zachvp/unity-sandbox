using UnityEngine;
using UnityEngine.Events;

public class MovementLogic : MonoBehaviour
{
    public int speed;
    public UnityEvent<int> movementEvent;

    public void Trigger(Vector2 input)
    {
        movementEvent.Invoke((int) input.x * speed);
    }
}
