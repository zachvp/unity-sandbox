using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class ModifyVelocity : MonoBehaviour
{
    public Vector2 value;
    public Rigidbody2D body;

    public void Trigger()
    {
        body.velocity = value;
    }
}
