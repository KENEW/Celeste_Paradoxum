using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss5 : MonoBehaviour
{
	public GameObject[] Ice;
	public GameObject Player = null;

	[SerializeField] float moveSpeed = 5.0f;

	public void Update()
	{
		if(Input.GetKeyDown(KeyCode.Z))
		{
			IceAttack1();
		}
		if(Input.GetKeyDown(KeyCode.X))
		{
			IceAttack2();
		}

	}
	public void IceAttack1()
	{
		Vector2 playerPos = Player.transform.position;

		for (int i = 0; i < Ice.Length; i++)
		{
			StartCoroutine(IceAttackCo(Ice[i], playerPos));
		}
	}
	public void IceAttack2()
	{
		for (int i = 0; i < Ice.Length; i++)
		{
			StartCoroutine(IceAttack2Co(Ice[i]));
		}
	}

	IEnumerator IceAttack2Co(GameObject ice)
	{
		Vector2 playerDir = (Player.transform.position - ice.transform.position).normalized;

		while(true)
		{
			ice.transform.Translate(playerDir * Time.deltaTime * moveSpeed);
			yield return null;
		}
	}

	IEnumerator IceAttackCo(GameObject obj, Vector2 playerPos)
	{
		while (Vector2.SqrMagnitude(obj.transform.position - transform.position) >= 0.001f)
		{
			obj.transform.position = Vector2.MoveTowards(obj.transform.position, playerPos, moveSpeed * Time.deltaTime);
			yield return null;
		}
	}
}
