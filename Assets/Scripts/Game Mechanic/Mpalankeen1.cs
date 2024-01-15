using System.Collections.Generic;
using UnityEngine;

public class Mpalankeen1 : MonoBehaviour
{
	public Animator an;

	public SpriteRenderer sp;

	public GameObject skinObj;

	private float waitTime;

	private float speed;

	internal bool direct;

	private int status;

	private Transform skinT;

	internal string myAn;

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
		Vector2 vector = new Vector2(0.07f, 0.158f);
		posAn.Add("OpenDoor", new Vector2(-0.586f, 0f) + vector);
		posAn.Add("OpenDoor2", new Vector2(-0.586f, 0f) + vector);
		posAn.Add("Run", new Vector2(0.042f, 0f) + vector);
		posAn.Add("Run2", new Vector2(0f, 0f) + vector);
		posAn.Add("Take", new Vector2(0.126f, 0f) + vector);
		posAn.Add("Take2", new Vector2(0.126f, 0f) + vector);
		posAn.Add("GirlUp", new Vector2(-0.281f, -0.021f) + vector);
		posAn.Add("GirlDown", new Vector2(-0.281f, -0.021f) + vector);
		posAn.Add("Fall", new Vector2(-0.262f, 0.205f) + vector);
		posAn.Add("Sleep", new Vector2(-0.095f, -0.012f) + vector);
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
