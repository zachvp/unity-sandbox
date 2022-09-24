using UnityEngine;
using UnityEngine.Events;

// Signal fires when value in range.
public class SignalRangeFloat : Signal
{
    //public DataFloat target;
    public float min;
    public float max;

    public void UpdateValue(float value)
    {
        //target.value = value;
        fired.value = min < value && value < max;

        if (invert)
        {
            fired.value = !fired.value;
        }

        UpdateValue(fired.value);

        //Debug.LogFormat("zvp: update range value: {0}", value);
    }
}
