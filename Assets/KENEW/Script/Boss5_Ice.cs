using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss5_Ice : MonoBehaviour
{
	public GameObject Player = null;
	private void Update()
	{
		transform.LookAt(Player.transform.localPosition);
	}
}
