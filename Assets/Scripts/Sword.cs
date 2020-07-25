using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public GameObject SwordEffect;
    public GameObject ExpParticle;

    public float Speed = 1.0f;
    public float SpeedTemp = 0.0f;

    private Vector2 moveVec = Vector2.zero;

    private void Start()
    {
        float rad = transform.rotation.z * Mathf.Deg2Rad;
        moveVec = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        Instantiate(SwordEffect, transform.position + new Vector3(0.0f, 0.0f, -2.0f), Quaternion.Euler(0, 0, Random.Range(0, 360)));
        Destroy(gameObject, 5.0f);
    }

    private void Update()
    {
        SpeedTemp += Speed;
        transform.Translate(moveVec * SpeedTemp * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Boss bossObject = collision.gameObject.GetComponent<Boss>();
            bossObject.Damaging();

            Instantiate(ExpParticle, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            Destroy(gameObject);
        }
    }
}