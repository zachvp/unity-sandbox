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
        //if (input.data.sqrMagnitude > 0)
        {
            var outputData = input.data * speed;
            outputData.y = Mathf.Max(0, jump * input.data.y);

            output.data = outputData;
            movementEvent.Invoke();
            //Debug.LogFormat("zvp: moved {0}", output.data);
        }
    }
}
