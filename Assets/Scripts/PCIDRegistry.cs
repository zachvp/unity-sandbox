using System;
using System.Collections.Generic;
using UnityEngine;

public class PCIDRegistry : CoreSingletonBehavior<PCIDRegistry>
{
    public Dictionary<Type, int> assignments;

    public override void Awake()
    {
        base.Awake();

        assignments = new Dictionary<Type, int>();
    }

    public void Register(object associate, Action<int> callback)
    {
        var key = associate.GetType();
        if (assignments.ContainsKey(key))
        {
            assignments[key] += 1;
            Debug.Log($"assigning {assignments[key]} to associate: {associate}");
        }
        else
        {
            assignments.Add(key, 0);
            Debug.Log($"assigning {0} to associate: {associate}");
        }

        callback(assignments[key]);
    }
}
