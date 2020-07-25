﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharityBoss : Boss
{
	public GameObject Player = null;

	[SerializeField] float dashSpeed = 70.0f;
	[SerializeField] float dashVector = 2f;
	int curDashCount = 0;
	const int DashCount = 2;

	Vector2 moveDir = Vector2.zero;
	protected override void Start()
	{
		base.Start();
	}

	protected override void Update()
	{
		base.Update();

		if (Input.GetKeyDown(KeyCode.Q))
		{
			DashAttack();
		}
	}

	protected void DashAttack()
	{
		Vector2 playPos = Player.transform.position;

		if (playPos.x - transform.position.x < 0)
		{
			transform.localScale = new Vector3(1, 1, 1);
			moveDir = Vector2.left;
		}
		else
		{
			transform.localScale = new Vector3(-1, 1, 1);
			moveDir = Vector2.right;
		}

		StartCoroutine(DelayCo(1.0f));
	}

	IEnumerator DashAttackCo()
	{
		animator.SetTrigger("Dash");

		Vector2 destPos = new Vector2(transform.position.x + ((-1 * dashVector) * transform.localScale.x), transform.position.y);
		while (Vector2.Distance(destPos, transform.position) >= 0.3f)
		{
			transform.Translate(moveDir * Time.deltaTime * dashSpeed);
			yield return null;
		}

		animator.ResetTrigger("Dash");
		//animator.SetTrigger("IdleTrigger");
	}
	
	IEnumerator DelayCo(float time)
	{
		yield return new WaitForSeconds(time);
		StartCoroutine(DashAttackCo());
	}
}
