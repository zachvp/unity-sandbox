using UnityEngine;

public class SignalLogicalOR : Signal
{
    public Signal lhs;
    public Signal rhs;

    public void HandleUpdate()
    {
        //fired.value = lhs.fired.value && rhs.fired.value;
        var value = lhs.fired.value || rhs.fired.value;
        onChange.Invoke(value);

        Debug.LogFormat("zvp: update AND: {0}", value);
    }
}
