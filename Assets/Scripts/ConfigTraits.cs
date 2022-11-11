using UnityEngine;
using System;

public class ConfigTraits : MonoBehaviour
{
    [Flags]
    public enum Traits
    {
        NONE    = 0,
        PICKUP  = 1 << 0,
        HELD    = 1 << 1
    }

    public Traits traits;
}
