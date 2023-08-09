using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBar : MonoBehaviour
{
    public RectTransform rect;
    public float speed = 256;
    public bool shrink;
    public Vector2 sizeStart;

    public float t;
    public float totalTime;

    public void Awake()
    {
        sizeStart = rect.sizeDelta;
    }

    // todo: determine how to set position to given world space coords
    public void Update()
    {
        if ((t < totalTime && !shrink) || (t > 0 && shrink))
        {
            var newSize = rect.sizeDelta;

            newSize.x = Mathf.Lerp(0, sizeStart.x, t / totalTime);
            rect.sizeDelta = newSize;

            if (shrink)
            {
                t -= Time.deltaTime;
            }
            else
            {
                t += Time.deltaTime;
            }
        }

    }

    public void SimpleScale()
    {
        var newSize = rect.sizeDelta;

        if (shrink)
        {
            if (rect.sizeDelta.x < 0)
            {
                newSize.x = 0;
            }
            else
            {
                newSize.x -= speed * Time.deltaTime;
            }
        }
        else
        {
            if (rect.sizeDelta.x > sizeStart.x)
            {
                newSize.x = sizeStart.x;
            }
            else
            {
                newSize.x += speed * Time.deltaTime;
            }
        }

        rect.sizeDelta = newSize;
    }
}
