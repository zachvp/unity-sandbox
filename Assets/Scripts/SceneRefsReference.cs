using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneRefsReference : MonoBehaviour
{
    public SceneRefs.ID id;

    public void Awake()
    {
        SceneRefs.Instance.Register(id, transform);
    }
}
