using UnityEngine;
using UnityEngine.Events;

public class MovementJump : MonoBehaviour
{
    public int jump;
    public DataBool input;
    public DataVector2 output;
    public UnityEvent movementEvent;

    private bool didJump;

    public void Trigger()
    {
        Debug.LogFormat("zvp: trigger jump");

        if (input.data)
        {
            output.data.y = jump;
            didJump = true;
            movementEvent.Invoke();
        }
        else if (didJump)
        {
            didJump = false;
            output.data.y = 0;
        }
    }
}
