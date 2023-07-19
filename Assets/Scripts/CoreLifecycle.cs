using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreLifecycle : CoreSingletonBehavior<CoreLifecycle>
{
    public override void OnDestroy()
    {
        base.OnDestroy();

        Notifications.Reset();
    }
}