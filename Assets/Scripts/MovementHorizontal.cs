using UnityEngine;
using UnityEngine.Events;

public class MovementHorizontal : MonoBehaviour
{
    public short speed;
    public UnityEvent<short> movementEvent;

    public void Trigger(float input)
    {
        movementEvent.Invoke((short)(input * speed));
    }
}
