using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
	private void Start()
	{
		//transform.Rotate(new Vector3(0, 0, 45));
	}
	private void Update()
	{
		transform.Translate(Vector2.down * Time.deltaTime * 3f);
	}
}
