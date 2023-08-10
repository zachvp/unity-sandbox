using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimatorController : MonoBehaviour
{
    public Animator animator;
    public float speed;

    public void Awake()
    {
        speed = animator.speed;
    }

    public void Update()
    {
        animator.speed = speed;
    }
}
