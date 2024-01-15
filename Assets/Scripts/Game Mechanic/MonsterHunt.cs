using System.Collections.Generic;
using UnityEngine;

public class MonsterHunt : MonoBehaviour
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

	private Dictionary<string, Vector2> posAn = new Dictionary<string, Vector2>();

	private void Start()
	{
		InitAn();
		skinT = base.gameObject.transform;
	}

	private void InitAn()
	{
		Vector2 vector = new Vector2(0f, -0.157f);
		posAn.Add("Run", new Vector2(0f, 0f) + vector);
		posAn.Add("Idle", new Vector2(0f, 0f) + vector);
		posAn.Add("Turn", new Vector2(-0.63f, 0.013f) + vector);
		posAn.Add("Fall", new Vector2(0f, 0f) + vector);
		posAn.Add("Roof", new Vector2(0f, 0f) + vector);
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

	public void SetDirect(bool n)
	{
		direct = n;
		sp.flipX = !n;
	}

	internal bool AnCompleted()
	{
		return an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;
	}
}
