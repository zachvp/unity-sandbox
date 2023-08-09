using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBar : MonoBehaviour
{
    public RectTransform rect;
    public bool shrink;
    public Vector2 sizeStart;

    public float t;
    public float totalTime;

    public void Awake()
    {
        sizeStart = rect.localScale;
    }

    public void Update()
    {
        var newSize = rect.localScale;

        if ((t < totalTime && !shrink) || (t > 0 && shrink))
        {
            if (shrink)
            {
                t = Mathf.Clamp(t - Time.deltaTime, 0, totalTime);
            }
            else
            {
                t = Mathf.Clamp(t + Time.deltaTime, 0, totalTime);
            }

            newSize.x = Mathf.Lerp(0, sizeStart.x, t / totalTime);
            rect.localScale = newSize;
        }
    }
}
