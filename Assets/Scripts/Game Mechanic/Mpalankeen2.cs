using System.Collections.Generic;
using UnityEngine;

public class Mpalankeen2 : MonoBehaviour
{
	public Animator an;

	public SpriteRenderer sp;

	public GameObject skinObj;

	private float waitTime;

	private float speed;

	internal bool direct;

	private int status;

	private Transform skinT;

	private string myAn;

	private Dictionary<string, Vector2> posAn = new Dictionary<string, Vector2>();

	private bool initEnd;

	private void Start()
	{
		InitMonster();
	}

	public void InitMonster()
	{
		if (!initEnd)
		{
			InitAn();
			skinT = base.gameObject.transform;
			an.keepAnimatorControllerStateOnDisable = true;
			initEnd = true;
		}
	}

	private void InitAn()
	{
		Vector2 vector = new Vector2(-0.085f, 0.108f);
		posAn.Add("Run", new Vector2(0f, 0f) + vector);
		posAn.Add("Run2", new Vector2(0f, 0f) + vector);
		posAn.Add("Take", new Vector2(0.038f, 0f) + vector);
		posAn.Add("Take2", new Vector2(0.038f, 0f) + vector);
		posAn.Add("Fall", new Vector2(-0.188f, 0.237f) + vector);
		posAn.Add("Sleep", new Vector2(0f, -0.134f) + vector);
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
