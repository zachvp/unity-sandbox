using UnityEngine;
using UnityEngine.Events;

public class SignalLogicalAND : Signal
{
    public Signal lhs;
    public Signal rhs;

    public void HandleUpdate()
    {
        //fired.value = lhs.fired.value && rhs.fired.value;
        var value = lhs.fired.value && rhs.fired.value;
        value = invert ? !value : value;

        fired.value = value;
        UpdateValue(value);

        //Debug.LogFormat("zvp: update AND: {0}", value);
    }
}
