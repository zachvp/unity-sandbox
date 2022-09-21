using UnityEngine;
using UnityEngine.Events;

public class MovementLogic : MonoBehaviour
{
    public DataVector2 input;
    public DataVector2 output;
    public int speed;
    public UnityEvent movementEvent;

    public void Trigger()
    {
        output.data.x = input.data.x * speed;
        movementEvent.Invoke();
    }
}
