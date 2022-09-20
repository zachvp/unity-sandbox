using UnityEngine;
using UnityEngine.Events;

public class MovementLogic : MonoBehaviour
{
    public DataVector2 input;
    public DataVector2 output;
    public float speed;
    public UnityEvent movementEvent;


    public void Trigger()
    {
        output.data = input.data * speed;

        movementEvent.Invoke();
    }
}
