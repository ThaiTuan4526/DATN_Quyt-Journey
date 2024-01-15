using System.Collections.Generic;
using UnityEngine;

public class Girl : MonoBehaviour, InterfaceBreakObj
{
	public Animator an;

	public SpriteRenderer sp;

	public GameObject skinObj;

	public HeroFloor heroFloor;

	public Monster8 monster8;

	public GameObject door;

	public GameObject notebook;

	private bool secondLoop;

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
		Main.game.arrBreakObj.Add(this);
		speed = 1.5f;
		notebook.SetActive(value: false);
		BreakObj();
	}

	public void BreakObj()
	{
		Singleton<ManagerFunctions>.use.removeFunction(ClassicActions);
		Singleton<ManagerFunctions>.use.removeFunction(ClassicActionsIdle);
		skinT.position = defaultPos;
		SetDirect(n: false);
		SetAn("Run");
		status = 2;
		door.SetActive(value: true);
		if (secondLoop)
		{
			Singleton<ManagerFunctions>.use.addFunction(ClassicActions);
		}
		else
		{
			Singleton<ManagerFunctions>.use.addFunction(ClassicActionsIdle);
		}
	}

	private void InitAn()
	{
		posAn.Add("Run", new Vector2(0f, 0f));
		posAn.Add("Run2", new Vector2(-0.065f, -0.011f));
		posAn.Add("Tremble", new Vector2(-0.06f, 0f));
		posAn.Add("Command", new Vector2(0f, 0f));
		posAn.Add("Command2", new Vector2(0f, 0f));
		posAn.Add("Fright", new Vector2(-0.019f, 0.004f));
		posAn.Add("Note", new Vector2(0f, 0f));
		posAn.Add("Think", new Vector2(0f, 0f));
		posAn.Add("Think2", new Vector2(0f, 0f));
	}

	public void SetSecondLoop()
	{
		secondLoop = true;
		Singleton<ManagerFunctions>.use.removeFunction(ClassicActions);
		Singleton<ManagerFunctions>.use.removeFunction(ClassicActionsIdle);
		status = 3;
		Singleton<ManagerFunctions>.use.addFunction(ClassicActions);
	}

	private void ClassicActions()
	{
		if (status == 1)
		{
			if (AnCompleted())
			{
				status = 2;
				SetDirect(n: false);
				SetAn("Run");
			}
		}
		else if (status == 2)
		{
			Move();
			if (base.transform.position.x < 15f && monster8.status == 1)
			{
				monster8.status = 2;
			}
			if (base.transform.position.x < 5f)
			{
				status = 3;
				SetAn("Note");
			}
		}
		else if (status == 3)
		{
			if (AnCompleted())
			{
				status = 4;
				SetDirect(n: true);
				SetAn("Run");
			}
		}
		else if (status == 4)
		{
			Move();
			if (base.transform.position.x > 7.5f && monster8.status == 5)
			{
				monster8.status = 6;
			}
			if (base.transform.position.x > 11f)
			{
				status = 5;
				SetAn("Note");
			}
		}
		else if (status == 5)
		{
			if (AnCompleted())
			{
				status = 6;
				SetAn("Run");
			}
		}
		else if (status == 6)
		{
			Move();
			if (base.transform.position.x > 11.5f && monster8.status == 9)
			{
				monster8.status = 10;
			}
			if (base.transform.position.x > 19f)
			{
				status = 1;
				SetAn("Note");
			}
		}
		CheckHeroOnView();
	}

	private void ClassicActionsIdle()
	{
		if (status == 2)
		{
			Move();
			if (base.transform.position.x < 15f && monster8.status == 1)
			{
				monster8.status = 2;
			}
			if (base.transform.position.x < 5f)
			{
				status = 3;
				SetAn("Think");
			}
		}
		else if (status == 3 && AnCompleted())
		{
			status = 4;
			SetAn("Think2");
		}
		CheckHeroOnView();
	}

	private void Kill()
	{
		if (status == 1)
		{
			if (AnCompleted())
			{
				notebook.SetActive(value: true);
				notebook.transform.position = base.transform.position;
				SetDirect(n: true);
				SetAn("Run2");
				status = 2;
			}
		}
		else if (status == 2)
		{
			Move();
			if (base.transform.position.x > 18f)
			{
				base.transform.position = new Vector2(18f, base.transform.position.y);
				status = 3;
				SetDirect(n: false);
				SetAn("Tremble");
				Singleton<ManagerFunctions>.use.removeFunction(Kill);
			}
		}
		else
		{
			_ = status;
			_ = 3;
		}
	}

	public void Fear()
	{
		speed = 2.5f;
		SetDirect(n: false);
		SetAn("Fright");
		status = 1;
		Singleton<ManagerFunctions>.use.removeFunction(ClassicActions);
		Singleton<ManagerFunctions>.use.removeFunction(ClassicActionsIdle);
		Singleton<ManagerFunctions>.use.addFunction(Kill);
	}

	public void MonsterShow()
	{
		if (myAn != "Command")
		{
			Singleton<ManagerFunctions>.use.removeFunction(ClassicActions);
			Singleton<ManagerFunctions>.use.removeFunction(ClassicActionsIdle);
			SetDirectToHero();
			SetAn("Command2");
			door.SetActive(value: false);
		}
	}

	private void CheckHeroOnView()
	{
		if (heroFloor.floor == 2 && OnSight())
		{
			door.SetActive(value: false);
			Singleton<ManagerFunctions>.use.removeFunction(ClassicActions);
			Singleton<ManagerFunctions>.use.removeFunction(ClassicActionsIdle);
			monster8.ToAttack();
			SetDirectToHero();
			SetAn("Command");
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
