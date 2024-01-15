using System;
using UnityEngine;

public class ShowDownLevel : MonoBehaviour
{
	public GameObject underLevel;

	public GameObject tree;

	public GameObject leafs;

	private Rigidbody2D[] rb;

	private void Awake()
	{
		underLevel.SetActive(value: false);
		tree.SetActive(value: true);
		rb = new Rigidbody2D[leafs.transform.childCount];
		for (int i = 0; i < leafs.transform.childCount; i++)
		{
			rb[i] = leafs.transform.GetChild(i).gameObject.GetComponent<Rigidbody2D>();
			rb[i].isKinematic = true;
			rb[i].gameObject.GetComponent<BoxCollider2D>().enabled = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D n)
	{
		if (n.gameObject.CompareTag("Hero") || n.gameObject.CompareTag("Bro"))
		{
			Main.hero.actionM = null;
			Hero hero = Main.hero;
			hero.actionM = (Hero.SetMethodAction)Delegate.Combine(hero.actionM, new Hero.SetMethodAction(Main.hero.EmptyFunction));
			for (int i = 0; i < rb.Length; i++)
			{
				rb[i].gameObject.GetComponent<BoxCollider2D>().enabled = true;
				rb[i].isKinematic = false;
				Rigidbody2D obj = rb[i];
				obj.velocity = Vector3.zero;
				obj.angularVelocity = 0f;
				Vector3 zero = Vector3.zero;
				zero.y += 0.01f + UnityEngine.Random.value * 0.01f;
				zero.x += UnityEngine.Random.Range(-0.02f, 0.02f);
				obj.AddForce(zero, ForceMode2D.Impulse);
				obj.AddTorque(zero.x);
			}
			underLevel.SetActive(value: true);
			tree.SetActive(value: false);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
