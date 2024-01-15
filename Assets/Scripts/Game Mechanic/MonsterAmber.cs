using System.Collections.Generic;
using UnityEngine;

public class MonsterAmber : MonoBehaviour
{
	public Animator an;

	public GameObject monsterAmberOnTree;

	public GameObject skinObj;

	public SpriteRenderer sp;

	private Dictionary<string, Vector2> posAn = new Dictionary<string, Vector2>();

	private string myAn = "";

	private int status;

	private bool toKick;

	private void Start()
	{
		InitAn();
		SetAn("Sleep");
	}

	private void InitAn()
	{
		posAn.Add("Run", new Vector2(0f, 0.15f));
		posAn.Add("Sleep", new Vector2(0.462f, 0.124000005f));
		posAn.Add("WakeUp", new Vector2(0.462f, 0.124000005f));
	}

	public void WakeUp()
	{
		monsterAmberOnTree.SetActive(value: true);
		Main.hero.stopActions = true;
		Main.hero.rb.velocity = Vector2.zero;
		Main.hero.jump = false;
		Main.hero.fall = false;
		Main.hero.SetDirect(n: true);
		Main.hero.SetAn("Watch");
		SetAn("WakeUp");
		status = 1;
		Singleton<ManagerFunctions>.use.addFunction(Go);
	}

	private void Go()
	{
		if (status == 1)
		{
			if (AnCompleted())
			{
				status = 2;
				SetAn("Run");
				sp.sortingLayerName = "overHero";
			}
		}
		else if (status == 2)
		{
			base.transform.Translate(Vector3.left * Time.deltaTime * 3.1f);
			if (!(base.transform.position.x < -2.23f))
			{
				return;
			}
			if (base.transform.position.y > -1.95f)
			{
				base.transform.Translate(Vector3.down * Time.deltaTime * 1.8f);
				if (base.transform.position.y < -1.95f)
				{
					base.transform.position = new Vector2(base.transform.position.x, -1.95f);
				}
			}
			if (!toKick && Mathf.Abs(base.transform.position.x - Main.hero.heroT.position.x) < 0.3f)
			{
				toKick = true;
				Main.hero.SetSomeAn("FallAss");
				Singleton<Sounds>.use.So("fall");
			}
			if (toKick && base.transform.position.y <= -1.95f)
			{
				status = 3;
			}
		}
		else if (status == 3)
		{
			base.transform.Translate(Vector3.left * Time.deltaTime * 3.1f);
			if (base.transform.position.x < -6.4f)
			{
				Main.hero.stopActions = false;
				Main.hero.SetSomeAn("Stand");
				Singleton<ManagerFunctions>.use.removeFunction(Go);
				base.gameObject.SetActive(value: false);
			}
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
