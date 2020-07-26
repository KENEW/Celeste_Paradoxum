using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kindnessboss : Boss
{
	public GameObject IcePre = null;
	[SerializeField] float IceHeight = 4f;

	protected override void Start()
	{
		base.Start();

		InvokeRepeating("IceAttack", 3f, 2f);
	}

	protected override void Update()
	{
		base.Update();
	}
	
	public void IceAttack()
	{
		Vector2 pos = new Vector2(charObj.transform.position.x, IceHeight);
		Instantiate(IcePre, pos, Quaternion.identity);
	}
}
