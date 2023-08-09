using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneRefsReference : MonoBehaviour
{
    [Tooltip("The ID to associate with the object")]
    public SceneRefs.ID id;

    [Tooltip("The component to register with the scene-wide registry")]
    public Object component;

    public void Awake()
    {
        SceneRefs.Instance.Register(id, component);
    }
}
