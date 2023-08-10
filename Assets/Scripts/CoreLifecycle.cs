using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// todo: delete
public class CoreLifecycle : CoreSingletonBehavior<CoreLifecycle>
{
    public override void OnDestroy()
    {
        base.OnDestroy();
    }
}
