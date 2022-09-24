using UnityEngine;
using UnityEngine.Events;

public class Signal : MonoBehaviour
{
    public DataBool fired;
    public UnityEvent<bool> onChange;
    public bool invert;

    protected bool previousFired;

    public void UpdateValue(bool value)
    {
        onChange.Invoke(value);

        //if (previousFired != value)
        //{
        //    if (invert)
        //    {
        //        onChange.Invoke(!value);
        //    }
        //    else
        //    {
        //        onChange.Invoke(value);
        //    }

        //    Debug.LogFormat("zvp: invoke signal");
        //}

        previousFired = value;

        Debug.LogFormat("zvp: update signal: {0}", value);
    }
}
