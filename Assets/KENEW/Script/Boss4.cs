using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4 : MonoBehaviour
{
	private Animator animator = null;

	public GameObject HpPrefab = null;
	public GameObject EnPrefab = null;

	public GameObject Player;

	public GameObject[] FragmentPre = null;
	public GameObject[] FragmentObj = null;
	[SerializeField] const int numFragmentObj = 5;

	[SerializeField] int hpPer = 50;
	[SerializeField] int bossHp = 100;

	private void Start()
	{
		FragmentObj = new GameObject[numFragmentObj];
		Player = GameObject.Find("Player");
	}
	public void AbsortHP()
	{
		GameObject hpObj = Instantiate(HpPrefab, Player.transform.position, Quaternion.identity);
		animator = hpObj.GetComponent<Animator>();
		animator.SetTrigger("AbsorbHP");
		StartCoroutine(AbsorbCo(hpObj));
	}

	public void AbsorbEnergy()
	{
		GameObject enObj = Instantiate(EnPrefab, Player.transform.position, Quaternion.identity);
		animator = enObj.GetComponent<Animator>();
		animator.SetTrigger("AbsorbHP");
		StartCoroutine(AbsorbCo(enObj));
	}

	public void Volcano()
	{
		StartCoroutine(VolcanoCo());
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("AbsorbHP"))
		{
			bossHp += 10;
			Destroy(collision.gameObject);
		}
	}
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Z))
			AbsortHP();
		if(Input.GetKeyDown(KeyCode.X))
			Volcano();
		if (Input.GetKeyDown(KeyCode.C))
			AbsorbEnergy();
	}

	IEnumerator VolcanoCo()
	{
		for (int i = 0; i < numFragmentObj; i++)
		{
			FragmentObj[i] = Instantiate(FragmentPre[Random.RandomRange(0, 3)], new Vector2(Player.transform.position.x, 4.6f), Quaternion.identity);
			yield return new WaitForSeconds(0.4f);
		}
	}
	IEnumerator AbsorbCo(GameObject obj)
	{
		while(Vector2.SqrMagnitude(obj.transform.position - transform.position) >= 0.001f)
		{
			obj.transform.position = Vector2.MoveTowards(obj.transform.position, transform.position, 8f * Time.deltaTime);
			yield return null;
		}
		obj.transform.position = transform.position;
	}
}
