using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4_Volcano : MonoBehaviour
{
	[SerializeField] float moveSpeed = 5.0f;
	private void Update()
	{
		transform.Translate(Vector2.down * Time.deltaTime * moveSpeed);
	}
}
