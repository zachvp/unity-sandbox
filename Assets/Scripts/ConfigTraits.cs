using UnityEngine;
using System;

public class ConfigTraits : MonoBehaviour
{
    [Flags]
    public enum Traits
    {
        NONE = 0,
        PICKUP = 1
    }

    public Traits traits;
}
