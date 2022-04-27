using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class EventHandler : MonoBehaviour
{
    private void Awake()
    {
        //testEvent.AddListener(Trigger);
    }

    public void Trigger(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("triggered");
        }
    }
}
