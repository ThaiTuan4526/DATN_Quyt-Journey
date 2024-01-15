using System.Collections.Generic;
using UnityEngine;

public class Monster7 : MonoBehaviour, InterfaceBreakObj
{
	public Animator an;

	public SpriteRenderer sp;

	public GameObject skinObj;

	public HeroFloor heroFloor;

	public Transform candleT;

	public FlowerBox flowerBox;

	public GameObject fish;

	private int status;

	private bool bellMode1;

	private bool bellMode2;

	private Transform skinT;

	private int statusAttack;

	private float waitTime;

	private string myAn;

	private float speed;

	private bool direct;

	private int room;

	private Vector2 defaultPos;

	internal int floor;

	internal bool jump;

	private bool stopMainF;

	private bool savedDirect;

	private string savedAn;

	private Dictionary<string, Vector2> posAn = new Dictionary<string, Vector2>();

	private float frameTake;

	private bool fishMode;

	private bool idleJump;

	private void Start()
	{
		InitAn();
		skinT = base.gameObject.transform;
		defaultPos = skinT.position;
		Main.game.arrBreakObj.Add(this);
		jump = false;
		stopMainF = false;
		BreakObj();
	}

	public void BreakObj()
	{
		if (!jump)
		{
			Singleton<ManagerFunctions>.use.removeFunction(GoToHero);
			Singleton<ManagerFunctions>.use.removeFunction(Bell1);
			Singleton<ManagerFunctions>.use.removeFunction(Bell2);
			Singleton<ManagerFunctions>.use.removeFunction(Attack);
			Singleton<ManagerFunctions>.use.removeFunction(Fish);
			Singleton<ManagerFunctions>.use.addFunction(ClassicActions);
			fishMode = false;
			stopMainF = false;
			floor = 3;
			skinT.position = defaultPos;
			status = 4;
			waitTime = 2f;
			bellMode1 = false;
			bellMode2 = false;
			speed = 1f;
			SetDirect(n: true);
			SetAn("Idle");
		}
		else
		{
			SetAn("Jump");
		}
	}

	private void InitAn()
	{
		posAn.Add("Run", new Vector2(0f, 0f));
		posAn.Add("Run2", new Vector2(0f, 0f));
		posAn.Add("Eat", new Vector2(0f, 0.017f));
		posAn.Add("Eat2", new Vector2(0f, 0.346f));
		posAn.Add("See", new Vector2(0.039f, 0.02f));
		posAn.Add("See2", new Vector2(-0.01f, 0.025f));
		posAn.Add("Idle", new Vector2(0f, 0f));
		posAn.Add("Idle2", new Vector2(0f, 0f));
		posAn.Add("Attack", new Vector2(-0.014f, 0f));
		posAn.Add("Evil", new Vector2(0.089f, 0f));
		posAn.Add("Jump", new Vector2(-0.02f, 0.232f));
		posAn.Add("JumpIdle", new Vector2(-0.018f, -0.023f));
		Singleton<ManagerFunctions>.use.addFunction(ClassicActions);
	}

	private void ClassicActions()
	{
		if (stopMainF)
		{
			return;
		}
		if (status >= 3 && fish.activeSelf && fish.transform.position.y < 9.86f)
		{
			waitTime = 0f;
			Singleton<Sounds>.use.So("seeFish");
			SetAn("See");
			status = 1;
			fishMode = true;
			Singleton<ManagerFunctions>.use.addFunction(Fish);
			Singleton<ManagerFunctions>.use.removeFunction(ClassicActions);
			return;
		}
		if (status == 1)
		{
			Move();
			if (skinT.position.x > 5.33f)
			{
				SetAn("Idle2");
				status = 2;
			}
		}
		else if (status == 2)
		{
			if (AnCompleted())
			{
				SetDirect(n: false);
				SetAn("Run");
				status = 3;
			}
		}
		else if (status == 3)
		{
			Move();
			if (skinT.position.x < -1.5f)
			{
				SetAn("Idle");
				status = 4;
				waitTime = 0f;
			}
		}
		else if (status == 4)
		{
			waitTime += Time.deltaTime;
			if (waitTime > 3f)
			{
				status = 1;
				SetDirect(n: true);
				SetAn("Run");
			}
		}
		CheckHeroOnView();
	}

	private void Fish()
	{
		if (status == 1)
		{
			if ((waitTime += 1f) > 5f && AnCompleted())
			{
				status = 2;
				SetAn("Run");
			}
		}
		else if (status == 2)
		{
			Move();
			if (fish.transform.position.y > 11.5f)
			{
				status = 10;
				SetAn("Evil");
				Singleton<Sounds>.use.So("evilMonster72");
			}
			else
			{
				if (!(skinT.position.x < -6.8f))
				{
					return;
				}
				if (fish.transform.position.y < 10.35f)
				{
					status = 4;
					if (fish.transform.position.y < 9.2f)
					{
						SetAn("Eat");
					}
					else
					{
						SetAn("Eat2");
					}
				}
				else if (fish.transform.position.y < 11.5f && fish.transform.position.y >= 10.35f)
				{
					status = 5;
					SetAn("Jump");
					jump = true;
					idleJump = false;
				}
				else if (fish.transform.position.y > 11.5f)
				{
					status = 10;
					SetAn("Evil");
					Singleton<Sounds>.use.So("evilMonster72");
				}
			}
		}
		else if (status == 3)
		{
			AnCompleted();
		}
		else if (status == 4)
		{
			if (fish.activeSelf && an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.21f)
			{
				fish.SetActive(value: false);
				Singleton<Sounds>.use.So2("placeMeat", 0.5f);
			}
			if (AnCompleted())
			{
				Singleton<ManagerFunctions>.use.removeFunction(Fish);
				Singleton<ManagerFunctions>.use.addFunction(ClassicActions);
				status = 1;
				SetDirect(n: true);
				SetAn("Run");
				fishMode = false;
			}
		}
		else if (status == 5)
		{
			if (idleJump)
			{
				if (AnCompleted())
				{
					idleJump = !idleJump;
					SetAn("Jump");
				}
			}
			else if (AnCompleted())
			{
				idleJump = !idleJump;
				SetAn("JumpIdle");
			}
			CheckHeroOnView();
			if (stopMainF)
			{
				fishMode = false;
				Singleton<ManagerFunctions>.use.removeFunction(Fish);
			}
			else if (fish.transform.position.y < 10.35f)
			{
				status = 4;
				if (fish.transform.position.y < 9.2f)
				{
					SetAn("Eat");
				}
				else
				{
					SetAn("Eat2");
				}
				jump = false;
			}
			else if (fish.transform.position.y > 11.5f)
			{
				status = 10;
				SetAn("Evil");
				jump = false;
				Singleton<Sounds>.use.So("evilMonster72");
			}
		}
		else if (status == 10 && AnCompleted())
		{
			fishMode = false;
			Singleton<ManagerFunctions>.use.removeFunction(Fish);
			Singleton<ManagerFunctions>.use.addFunction(ClassicActions);
			status = 1;
			SetDirect(n: true);
			SetAn("Run");
		}
	}

	private void Bell1()
	{
		if (stopMainF)
		{
			return;
		}
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
			if (skinT.position.x < -3.95f)
			{
				status = 3;
				SetDirect(n: true);
				base.transform.position = new Vector2(-3.51f, 1.14f);
				floor = 2;
			}
		}
		else if (status == 3)
		{
			Move();
			if (skinT.position.x > 0.7f)
			{
				status = 4;
				SetAn("Evil");
				Singleton<Sounds>.use.So("evilMonster7");
			}
		}
		else if (status == 4)
		{
			if (AnCompleted())
			{
				status = 5;
				SetDirect(n: false);
				SetAn("Run");
			}
		}
		else if (status == 5)
		{
			Move();
			if (skinT.position.x < -3.95f)
			{
				SetDirect(n: true);
				base.transform.position = new Vector2(-3.38f, 7.12f);
				bellMode1 = false;
				floor = 3;
				status = 1;
				Singleton<ManagerFunctions>.use.removeFunction(Bell1);
				Singleton<ManagerFunctions>.use.addFunction(ClassicActions);
			}
		}
		CheckHeroOnView();
	}

	private void Bell2()
	{
		if (stopMainF)
		{
			return;
		}
		if (status == 1)
		{
			if (AnCompleted())
			{
				status = 2;
				SetDirect(n: true);
				SetAn("Run");
			}
		}
		else if (status == 2)
		{
			if (skinT.position.x > 5.12f)
			{
				if (!HeroIsMovingFloors())
				{
					CheckHeroOnView();
					if (!stopMainF)
					{
						status = 3;
						base.transform.position = new Vector2(4.4f, -4.9f);
						floor = 1;
						CheckHeroOnView();
						if (!stopMainF)
						{
							SetDirect(n: false);
						}
					}
				}
			}
			else
			{
				Move();
			}
		}
		else if (status == 3)
		{
			Move();
			if (skinT.position.x < -1f)
			{
				status = 4;
				SetAn("Evil");
				Singleton<Sounds>.use.So("evilMonster7");
			}
		}
		else if (status == 4)
		{
			if (AnCompleted())
			{
				status = 5;
				SetDirect(n: true);
				SetAn("Run");
			}
		}
		else if (status == 5)
		{
			Move();
			if (skinT.position.x > 5.12f)
			{
				status = 6;
				floor = 2;
				base.transform.position = new Vector2(4.47f, 1.14f);
				SetDirect(n: false);
			}
		}
		else if (status == 6)
		{
			Move();
			if (skinT.position.x < -3.95f)
			{
				SetDirect(n: true);
				base.transform.position = new Vector2(-3.38f, 7.12f);
				bellMode2 = false;
				floor = 3;
				status = 1;
				Singleton<ManagerFunctions>.use.removeFunction(Bell2);
				Singleton<ManagerFunctions>.use.addFunction(ClassicActions);
			}
		}
		CheckHeroOnView();
	}

	private void GoToHero()
	{
		if (statusAttack == 1)
		{
			if (heroFloor.floor != floor || heroFloor.goTo)
			{
				SetAn("Idle2");
				statusAttack = 3;
				return;
			}
			if (Mathf.Abs(skinT.position.x - Main.hero.heroT.position.x) < 3.5f)
			{
				Main.hero.SetFear(skinT.position.x);
			}
			SetDirectToHero();
			Move();
			if (Main.hero.gameObject.activeSelf && Mathf.Abs(base.transform.position.x - Main.hero.heroT.position.x) < 1f && heroFloor.floor == floor)
			{
				ToCatch();
			}
		}
		else if (statusAttack == 2)
		{
			if (Main.hero.gameObject.activeSelf && Mathf.Abs(base.transform.position.x - Main.hero.heroT.position.x) < 1f && heroFloor.floor == floor)
			{
				ToCatch();
			}
			else if ((waitTime += 1f) > 5f && AnCompleted())
			{
				statusAttack = 1;
				SetAn("Run2");
			}
		}
		else if (statusAttack == 3 && AnCompleted())
		{
			speed = 1f;
			stopMainF = false;
			SetDirect(savedDirect);
			SetAn(savedAn);
			Singleton<ManagerFunctions>.use.removeFunction(GoToHero);
		}
	}

	private void Attack()
	{
		if (status == 1)
		{
			if (an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f)
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
				heroFloor.floor = 1;
				Singleton<ManagerFunctions>.use.removeFunction(Attack);
				Main.game.ReLoadScene();
			}
		}
	}

	private bool HeroIsMovingFloors()
	{
		if (heroFloor.goTo)
		{
			an.speed = 0f;
			CheckHeroOnView();
			if (stopMainF)
			{
				an.speed = 1f;
			}
			return true;
		}
		an.speed = 1f;
		return false;
	}

	public void SetBell(int n)
	{
		if (fishMode)
		{
			return;
		}
		switch (n)
		{
		case 1:
			Singleton<ManagerFunctions>.use.removeFunction(ClassicActions);
			Singleton<ManagerFunctions>.use.removeFunction(Bell1);
			Singleton<ManagerFunctions>.use.removeFunction(Bell2);
			Singleton<ManagerFunctions>.use.addFunction(Bell1);
			bellMode2 = false;
			if (!bellMode1)
			{
				SetAn("See");
				bellMode1 = true;
				status = 1;
			}
			else if (status == 5)
			{
				status = 3;
				SetDirect(n: true);
			}
			break;
		case 2:
			if (Mathf.Abs(base.transform.position.y - 1.14f) < 1f)
			{
				Singleton<ManagerFunctions>.use.removeFunction(ClassicActions);
				Singleton<ManagerFunctions>.use.removeFunction(Bell1);
				Singleton<ManagerFunctions>.use.removeFunction(Bell2);
				Singleton<ManagerFunctions>.use.addFunction(Bell2);
				bellMode1 = false;
				if (!bellMode2)
				{
					SetAn("See");
					bellMode2 = true;
					status = 1;
				}
				else if (status == 6)
				{
					status = 2;
					SetDirect(n: true);
				}
			}
			break;
		}
	}

	private void ToCatch()
	{
		SetAn("Attack");
		status = 1;
		waitTime = 0f;
		Singleton<ManagerFunctions>.use.removeFunction(GoToHero);
		Singleton<ManagerFunctions>.use.addFunction(Attack);
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

	private void CheckHeroOnView()
	{
		if (!flowerBox.isHide && heroFloor.floor == floor && !heroFloor.goTo && OnSight())
		{
			savedAn = myAn;
			savedDirect = direct;
			Singleton<ManagerFunctions>.use.addFunction(GoToHero);
			SetDirectToHero();
			SetAn("See");
			waitTime = 0f;
			speed = 1.8f;
			statusAttack = 2;
			stopMainF = true;
			Singleton<Sounds>.use.So("seeHeroM7");
		}
	}

	private bool OnSight()
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
		return false;
	}

	private void SetDirect(bool n)
	{
		direct = n;
		sp.flipX = !n;
		if (direct)
		{
			candleT.localPosition = new Vector3(0.6f, candleT.localPosition.y, -2f);
		}
		else
		{
			candleT.localPosition = new Vector3(-0.6f, candleT.localPosition.y, -2f);
		}
	}

	private void Move()
	{
		if (direct)
		{
			base.transform.Translate(speed * Vector3.right * Time.deltaTime);
		}
		else
		{
			base.transform.Translate(speed * Vector3.left * Time.deltaTime);
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

	private bool AnCompleted()
	{
		return an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;
	}
}
