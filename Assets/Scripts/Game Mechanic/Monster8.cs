using System.Collections.Generic;
using UnityEngine;

public class Monster8 : MonoBehaviour, InterfaceBreakObj
{
	public Animator an;

	public SpriteRenderer sp;

	public GameObject skinObj;

	public Transform girl;

	public HeroFloor heroFloor;

	public Girl girlS;

	public GoodMonster goodMonster;

	private float waitTime;

	private float speed;

	private bool direct;

	internal int status;

	private Transform skinT;

	private string myAn;

	private Vector2 defaultPos;

	private Dictionary<string, Vector2> posAn = new Dictionary<string, Vector2>();

	private void Start()
	{
		skinT = base.gameObject.transform;
		defaultPos = skinT.position;
		Main.game.arrBreakObj.Add(this);
		InitAn();
		BreakObj();
	}

	public void BreakObj()
	{
		Singleton<ManagerFunctions>.use.removeFunction(ClassicActions);
		Singleton<ManagerFunctions>.use.removeFunction(GoToHero);
		skinT.position = defaultPos;
		speed = 1.2f;
		status = 1;
		SetDirect(n: false);
		SetAn("Idle");
		Singleton<ManagerFunctions>.use.addFunction(ClassicActions);
	}

	private void InitAn()
	{
		Vector2 vector = new Vector2(0f, -0.157f);
		posAn.Add("Run", new Vector2(0f, 0f) + vector);
		posAn.Add("Attack", new Vector2(0.447f, 0f) + vector);
		posAn.Add("Idle", new Vector2(0f, 0f) + vector);
		posAn.Add("Dead", new Vector2(0.233f, 0f) + vector);
	}

	private void ClassicActions()
	{
		if (status != 1)
		{
			if (status == 2)
			{
				SetDirect(n: false);
				status = 3;
			}
			else if (status == 3)
			{
				if (girl.position.x < 13.4f)
				{
					status = 4;
					SetAn("Run");
				}
			}
			else if (status == 4)
			{
				Move();
				if (base.transform.position.x < 7.3f)
				{
					status = 5;
					SetAn("Idle");
				}
			}
			else if (status != 5)
			{
				if (status == 6)
				{
					SetDirect(n: true);
					status = 9;
				}
				else if (status == 7)
				{
					if (girl.position.x > 8f)
					{
						status = 8;
						SetAn("Run");
					}
				}
				else if (status == 8)
				{
					Move();
					if (base.transform.position.x > 9f)
					{
						status = 9;
						SetAn("Idle");
					}
				}
				else if (status != 9 && status == 10)
				{
					SetAn("Run");
					Move();
					if (base.transform.position.x > 15.7f)
					{
						status = 1;
						SetAn("Idle");
					}
				}
			}
		}
		CheckHeroOnView();
	}

	private void GoToHero()
	{
		Move();
		Main.hero.SetFear(base.transform.position.x);
		if (Mathf.Abs(base.transform.position.x - Main.hero.heroT.position.x) < 1f)
		{
			SetAn("Attack");
			status = 1;
			waitTime = 0f;
			Singleton<ManagerFunctions>.use.removeFunction(GoToHero);
			Singleton<ManagerFunctions>.use.addFunction(Attack);
		}
	}

	private void Attack()
	{
		if (status == 1)
		{
			if (an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.78f)
			{
				Main.hero.gameObject.SetActive(value: false);
				status = 2;
				waitTime = 0f;
				Singleton<Sounds>.use.So("isDead");
				Singleton<InputChecker>.use.SetVibro(1f);
			}
		}
		else
		{
			waitTime += Time.deltaTime;
			if (waitTime > 1f)
			{
				Singleton<ManagerFunctions>.use.removeFunction(Attack);
				Main.game.ReLoadScene();
			}
		}
	}

	private void GoToMonster()
	{
		Move();
		if (Mathf.Abs(base.transform.position.x - goodMonster.transform.position.x) < 1f)
		{
			Singleton<ManagerFunctions>.use.removeFunction(GoToMonster);
			base.gameObject.SetActive(value: false);
			goodMonster.Attack1();
		}
	}

	public void ToAttack()
	{
		SetDirectToHero();
		Singleton<ManagerFunctions>.use.removeFunction(ClassicActions);
		Singleton<ManagerFunctions>.use.addFunction(GoToHero);
		SetAn("Run");
		speed = 3.9f;
		Singleton<Sounds>.use.So("monsterGo");
	}

	private void CheckHeroOnView()
	{
		if (goodMonster.onFloor2 && goodMonster.gameObject.transform.position.x > 2f)
		{
			speed = 3f;
			SetDirect(n: false);
			SetAn("Run");
			Singleton<ManagerFunctions>.use.removeFunction(ClassicActions);
			Singleton<ManagerFunctions>.use.addFunction(GoToMonster);
			girlS.Fear();
		}
		if (heroFloor.floor == 2 && OnSight())
		{
			ToAttack();
			girlS.MonsterShow();
		}
	}

	private bool OnSight()
	{
		if (Main.hero.heroT.position.x > 1.95f)
		{
			if (Mathf.Abs(base.transform.position.x - Main.hero.heroT.position.x) < 1f)
			{
				return true;
			}
			if (base.transform.position.x > Main.hero.heroT.position.x)
			{
				if (!direct)
				{
					return true;
				}
			}
			else if (base.transform.position.x < Main.hero.heroT.position.x && direct)
			{
				return true;
			}
		}
		return false;
	}

	private void Move()
	{
		if (direct)
		{
			base.transform.Translate(speed * Time.deltaTime * Vector3.right);
		}
		else
		{
			base.transform.Translate(speed * Time.deltaTime * Vector3.left);
		}
	}

	private void SetDirectToHero()
	{
		if (base.transform.position.x > Main.hero.heroT.position.x)
		{
			SetDirect(n: false);
		}
		else
		{
			SetDirect(n: true);
		}
	}

	public void SetAn(string n)
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

	private bool AnCompleted()
	{
		return an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;
	}
}
