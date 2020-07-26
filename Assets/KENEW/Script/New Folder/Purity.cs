using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purity : Boss
{
	public GameObject stonePre = null;

	private void Start()
	{
		StartCoroutine(repeatAttack());
	}
	IEnumerator repeatAttack()
	{
		yield return new WaitForSeconds(3f);
		while(true)
		{
			StartCoroutine(StoneAttack());
			yield return new WaitForSeconds(5f);
		}
	}
	IEnumerator StoneAttack()
	{
		for(int i = 0; i < 3; i++)
		{
			Instantiate(stonePre, transform.position, Quaternion.identity);
			yield return new WaitForSeconds(0.5f);
		}
	}
	
	public override void Damaging()
	{
		Hp--;
		if (Hp < 0)
		{
			UIManager.Instance.SeeClearScreen();
			Destroy(gameObject);
		}

	}
}
