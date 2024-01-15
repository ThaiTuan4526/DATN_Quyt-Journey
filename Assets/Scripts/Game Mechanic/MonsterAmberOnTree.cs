using System.Collections.Generic;
using UnityEngine;

public class MonsterAmberOnTree : MonoBehaviour
{
	public Animator an;

	public GameObject skinObj;

	public SpriteRenderer sp;

	public GameObject amber;

	public GameObject poop;

	public GameObject plat;

	private Dictionary<string, Vector2> posAn = new Dictionary<string, Vector2>();

	private string myAn = "";

	private int status;

	private bool addAmber;

	private Vector3 p;

	private Vector3 defaultPos;

	private float wAn;

	private void Start()
	{
		InitAn();
		SetAn("Idle");
		addAmber = false;
		defaultPos = poop.transform.position;
		poop.SetActive(value: false);
		base.gameObject.SetActive(value: false);
	}

	private void InitAn()
	{
		posAn.Add("Idle", new Vector2(0f, 0.15f));
		posAn.Add("Throw", new Vector2(-0.112f, 0.15f));
		posAn.Add("Happy", new Vector2(0f, 0.15f));
		posAn.Add("Amber", new Vector2(0.329f, 0.165f));
	}

	public bool SetAction(bool n)
	{
		if (base.gameObject.activeSelf)
		{
			if (n)
			{
				Singleton<ManagerFunctions>.use.addFunction(Good);
			}
			else
			{
				SetAn("Throw");
				Singleton<ManagerFunctions>.use.addFunction(Bad);
			}
			status = 1;
			wAn = 0f;
			return true;
		}
		return false;
	}

	private void Good()
	{
		if (status == 1)
		{
			wAn += Time.deltaTime;
			if (wAn > 2.2f)
			{
				SetAn("Happy");
				status = 2;
				wAn = 0f;
			}
		}
		else if (status == 2)
		{
			wAn += Time.deltaTime;
			if (wAn > 0.5f && AnCompleted())
			{
				wAn = 0f;
				if (!addAmber)
				{
					SetAn("Amber");
					status = 3;
					amber.SetActive(value: true);
					addAmber = true;
				}
				else
				{
					SetAn("Idle");
					Singleton<ManagerFunctions>.use.removeFunction(Good);
				}
			}
		}
		else if (status == 2 && wAn > 0.5f && AnCompleted())
		{
			SetAn("Idle");
			Singleton<ManagerFunctions>.use.removeFunction(Good);
		}
	}

	public void SetFly()
	{
		poop.SetActive(value: true);
		poop.transform.position = defaultPos;
		p = Main.hero.heroT.position;
		p.y += 1.58f;
		poop.transform.right = p - poop.transform.position;
		status = 2;
	}

	private void Bad()
	{
		if (status == 2)
		{
			poop.transform.GetChild(0).transform.Rotate(Vector3.back * Time.deltaTime * 280f);
			poop.transform.Translate(10f * Vector3.right * Time.deltaTime);
			if (Vector2.Distance(poop.transform.position, p) < 0.5f)
			{
				Singleton<SteamF>.use.AddAch(8);
				Singleton<Sounds>.use.So("tomatoToFace");
				Main.hero.SetAn("PopToFace");
				poop.SetActive(value: false);
				plat.SetActive(value: false);
				status = 3;
			}
		}
		else if (status == 3 && Main.hero.AnCompleted())
		{
			SetAn("Idle");
			plat.SetActive(value: true);
			Singleton<ManagerFunctions>.use.removeFunction(Bad);
			Main.hero.ReturnToGame();
		}
	}

	private bool AnCompleted()
	{
		return an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;
	}

	private void SetAn(string n)
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
}
