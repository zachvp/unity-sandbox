using UnityEngine;
using UnityEngine.Events;

public class MovementLogic : MonoBehaviour
{
    public DataVector2 input;
    public DataVector2 output;
    public int speed;
    public int jump;
    public UnityEvent movementEvent;

    public void Trigger()
    {
        output.data = input.data * speed;
        movementEvent.Invoke();
    }

    public void TriggerJump()
    {
        Debug.LogFormat("zvp: trigger jump");

        var outputData = output.data;
        outputData.y = jump;

        movementEvent.Invoke();
    }
}
