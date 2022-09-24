using UnityEngine;

public class BehaviorToggle : MonoBehaviour
{
    public Behaviour behavior;
    public DataBool state;

    public void Update()
    {
        behavior.enabled = state.value;
    }

    public void ToggleEnabled(bool value)
    {
        //behavior.enabled = value;
    }
}
