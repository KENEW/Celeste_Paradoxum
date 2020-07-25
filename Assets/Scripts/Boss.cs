using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int Hp = 0;

    protected Rigidbody2D rigidbody;
    protected Animator animator;
    protected SpriteRenderer sprRenderer;

    protected virtual void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        sprRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
    }

    protected virtual void Attack()
    {
    }
}