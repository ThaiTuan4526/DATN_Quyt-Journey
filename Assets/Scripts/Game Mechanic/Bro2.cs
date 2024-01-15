using System.Collections.Generic;
using UnityEngine;

public class Bro2 : MonoBehaviour
{
	public Animator an;

	public SpriteRenderer sp;

	public GameObject skinObj;

	private float waitTime;

	private float speed;

	private bool direct;

	private int status;

	private Transform skinT;

	private string myAn;

	private Vector2 defaultPos;

	private Dictionary<string, Vector2> posAn = new Dictionary<string, Vector2>();

	private void Start()
	{
		InitAn();
		skinT = base.gameObject.transform;
		defaultPos = skinT.position;
	}

	private void InitAn()
	{
		posAn.Add("Swing", new Vector2(0f, 0f));
		posAn.Add("Puke", new Vector2(-0.12f, 0f));
		posAn.Add("Breath", new Vector2(-0.12f, 0f));
	}

	internal void SetAn(string n)
	{
		if (!(myAn != n))
		{
			return;
		}
		an.Play(n, -1, 0f);
		myAn = n;
		if (posAn.ContainsKey(n))
		{
			Vector2 vector = posAn[n];
			if (sp.flipX)
			{
				vector.x *= -1f;
			}
			skinObj.transform.localPosition = vector;
		}
	}

	private void SetDirect(bool n)
	{
		direct = n;
		sp.flipX = !n;
	}

	internal bool AnCompleted()
	{
		return an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;
	}
}
