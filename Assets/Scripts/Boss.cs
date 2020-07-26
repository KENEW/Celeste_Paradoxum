using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int Hp = 100;

    protected Rigidbody2D rigidbody;
    protected Animator animator;
    protected SpriteRenderer sprRenderer;

    protected GameObject charObj;

    protected virtual void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        sprRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        if (charObj == null)
        {
            charObj = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            Attack();

            Vector2 vecDir = (charObj.transform.position - transform.position);
            if (vecDir.x < 0) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            else if (vecDir.x > 0) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }

        UIManager.Instance.SetBossHpGauge(Hp);
    }

    protected virtual void Attack()
    {
    }

    public virtual void Damaging()
    {
        Hp--;
        if (Hp < 0)
        {
            UIManager.Instance.SeeClearScreen();
            Destroy(gameObject);
        }
    }
}