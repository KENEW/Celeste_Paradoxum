using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
	public GameObject playPos = null;
	private void Start()
	{
		playPos = GameObject.Find("Player");
		StoneAttack(playPos.transform.position);
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		Destroy(this.gameObject);
	}
	public void StoneAttack(Vector2 pos)
	{
		StartCoroutine(AttackCo(pos));
	}
   IEnumerator AttackCo(Vector2 pos)
   {
		while(Vector2.Distance(pos, transform.position) >= 0.1f)
		{
			transform.position = Vector2.Lerp(transform.position, pos, 3.0f * Time.deltaTime);
			yield return null;
		}
		Destroy(this.gameObject);
   }
}
