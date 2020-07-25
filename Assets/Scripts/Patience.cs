using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patience : Boss
{
    public GameObject FistPrefab;
    public GameObject WarningPrefab;

    public float DashSpeed = 10.0f;

    private Timer attackTimer;
    private int attackPhase = 0;

    private bool IsGuard = false;
    private int GuardDamage = 0;

    private Vector3 fistPosition;

    protected override void Start()
    {
        base.Start();
        attackTimer = new Timer(3.0f, true)
        {
            IsEnable = true
        };
    }

    protected override void Update()
    {
        base.Update();
        animator.SetBool("IsGuard", IsGuard);
    }

    protected override void Attack()
    {
        if (attackTimer.IsDone)
        {
            if (attackPhase < 3)
            {
                attackPhase++;
                attackTimer = new Timer(2.0f, true)
                {
                    IsEnable = true
                };
                animator.SetTrigger("Dashing");
                Vector2 vecDir = (charObj.transform.position - transform.position).normalized;
                rigidbody.AddForce(vecDir * DashSpeed, ForceMode2D.Impulse);
            }
            else if (attackPhase < 4)
            {
                attackPhase++;
                attackTimer = new Timer(10.0f, true)
                {
                    IsEnable = true
                };
                IsGuard = true;
            }
            else if (attackPhase < 5)
            {
                attackPhase++;
                attackTimer = new Timer(2.0f, true)
                {
                    IsEnable = true
                };
                IsGuard = false;
                GuardDamage = 0;
            }
            else if (attackPhase < 6)
            {
                attackPhase++;
                attackTimer = new Timer(1.0f, true)
                {
                    IsEnable = true
                };
                fistPosition = charObj.transform.position;
                Instantiate(WarningPrefab, charObj.transform.position, Quaternion.identity);
            }
            else if (attackPhase < 7)
            {
                attackPhase++;
                attackTimer = new Timer(0.5f, true)
                {
                    IsEnable = true
                };
                animator.SetTrigger("Attacking");
            }
            else if (attackPhase < 8)
            {
                attackPhase++;
                attackTimer = new Timer(1.5f, true)
                {
                    IsEnable = true
                };
                Instantiate(FistPrefab, fistPosition, Quaternion.identity);
            }
            else if (attackPhase < 9)
            {
                attackPhase++;
                attackTimer = new Timer(1.0f, true)
                {
                    IsEnable = true
                };
                fistPosition = charObj.transform.position;
                Instantiate(WarningPrefab, charObj.transform.position, Quaternion.identity);
            }
            else if (attackPhase < 10)
            {
                attackPhase++;
                attackTimer = new Timer(0.5f, true)
                {
                    IsEnable = true
                };
                animator.SetTrigger("Attacking");
            }
            else if (attackPhase < 11)
            {
                attackPhase++;
                attackTimer = new Timer(1.5f, true)
                {
                    IsEnable = true
                };
                Instantiate(FistPrefab, fistPosition, Quaternion.identity);
            }
            else
            {
                attackPhase = 0;
                attackTimer = new Timer(1.5f, true)
                {
                    IsEnable = true
                };
            }
        }
    }

    public override void Damaging()
    {
        if (!IsGuard)
        {
            Hp--;
            if (Hp < 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            GuardDamage += 1;
        }
    }
}