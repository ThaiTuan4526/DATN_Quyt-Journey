using UnityEngine;

public class Witch : AnimationCharacter, InterfaceBreakObj
{
	public Light sun;

	public TableMush mushGame;

	public GameObject sunHero;

	public Cupboard cupboard;

	public CabOpen cabOpen;

	public GameObject witchDead;

	public Fireplace fireplace;

	public GameObject heartSo;

	public BoxCollider2D poisonCollider2D;

	internal bool potionAdded;

	private int status;

	private int status2;

	private float timeAction;

	private float waitTime;

	private float speed = 1.5f;

	private bool direct;

	private bool foundInCup;

	private int room;

	private bool dead;

	private bool onUp = true;

	internal bool attackMode;

	private void Start()
	{
		status = 1;
		timeAction = 0f;
		witchDead.SetActive(value: false);
		anPos.Init();
		Main.game.arrBreakObj.Add(this);
		Singleton<ManagerFunctions>.use.addFunction(ClassicActions);
	}

	public void BreakObj()
	{
		Singleton<ManagerFunctions>.use.removeFunction(GoToHero);
		Singleton<ManagerFunctions>.use.removeFunction(Angry);
		Singleton<ManagerFunctions>.use.removeFunction(GoToHero);
		Singleton<ManagerFunctions>.use.removeFunction(Attack);
		Singleton<ManagerFunctions>.use.addFunction(ClassicActions);
		status = 1;
		timeAction = 0f;
		onUp = true;
		attackMode = false;
		foundInCup = false;
		speed = 1.5f;
		sun.intensity = 0.7f;
		potionAdded = false;
		poisonCollider2D.enabled = true;
	}

	private void ClassicActions()
	{
		if (status == 1)
		{
			base.transform.position = new Vector2(5.54f, 0.52f);
			SetAn("Stir");
			status = 2;
			timeAction = 0f;
			room = 3;
			HeartSound();
			if (sunHero.activeSelf)
			{
				Singleton<Sounds>.use.So("stirWitch");
			}
		}
		else if (status == 2)
		{
			timeAction += Time.deltaTime;
			if (timeAction > 2f)
			{
				SetAn("Try");
				status = 3;
				timeAction = 0f;
				if (sunHero.activeSelf)
				{
					Singleton<Sounds>.use.So("chew2_so");
				}
			}
		}
		else if (status == 3)
		{
			timeAction += Time.deltaTime;
			if (timeAction > 2f)
			{
				if (potionAdded)
				{
					timeAction = 0f;
					SetDirect(n: true);
					SetAn("Dead");
					Singleton<ManagerFunctions>.use.removeFunction(ClassicActions);
					Singleton<ManagerFunctions>.use.addFunction(Dead);
					Singleton<Sounds>.use.So("tryBadPoison");
					return;
				}
				SetDirect(n: false);
				SetAn("Think");
				status = 4;
				timeAction = 0f;
			}
		}
		else if (status == 4)
		{
			if (AnCompleted())
			{
				SetAn("Run");
				status = 5;
			}
		}
		else if (status == 5)
		{
			if (base.transform.position.x < 1.48f)
			{
				room = 2;
				HeartSound();
			}
			Move();
			if (base.transform.position.x <= 0f)
			{
				base.transform.position = new Vector2(0f, base.transform.position.y);
				SetDirect(n: true);
				SetAn("Try");
				status = 6;
				timeAction = 0f;
				if (sunHero.activeSelf)
				{
					Singleton<Sounds>.use.So("rummage");
				}
			}
		}
		else if (status == 6)
		{
			timeAction += Time.deltaTime;
			if (timeAction > 1.5f)
			{
				SetDirect(n: true);
				SetAn("Run");
				status = 7;
			}
		}
		else if (status == 7)
		{
			if (base.transform.position.x > 1.87f)
			{
				room = 3;
				HeartSound();
			}
			Move();
			if (base.transform.position.x >= 3.42f)
			{
				base.transform.position = new Vector2(3.42f, base.transform.position.y);
				SetAn("Cook");
				status = 8;
				timeAction = 0f;
				if (sunHero.activeSelf)
				{
					Singleton<Sounds>.use.So("cutting");
				}
			}
		}
		else if (status == 8)
		{
			timeAction += Time.deltaTime;
			if (timeAction > 3f)
			{
				SetAn("Think");
				status = 9;
				timeAction = 0f;
			}
		}
		else if (status == 9)
		{
			if (AnCompleted())
			{
				SetDirect(n: false);
				SetAn("Run");
				status = 10;
				status2 = 1;
			}
		}
		else if (status == 10)
		{
			if (status2 == 1)
			{
				if (base.transform.position.x < -4.2f)
				{
					room = 1;
					HeartSound();
				}
				else if (base.transform.position.x < 1.48f)
				{
					room = 2;
					HeartSound();
				}
				Move();
				if (base.transform.position.x <= -5.13f)
				{
					base.transform.position = new Vector2(-5.13f, base.transform.position.y);
					if (cabOpen.isOpen)
					{
						timeAction = 0f;
						SetAn("See");
						Singleton<ManagerFunctions>.use.addFunction(Angry);
						Singleton<ManagerFunctions>.use.removeFunction(ClassicActions);
						return;
					}
					SetAn("Think");
					status2 = 2;
					timeAction = 0f;
				}
			}
			else if (status2 == 2)
			{
				timeAction += Time.deltaTime;
				if (AnCompleted())
				{
					Debug.Log("Box");
					SetAn("Box");
					status = 11;
					timeAction = 0f;
					if (sunHero.activeSelf)
					{
						Singleton<Sounds>.use.So("rummage");
					}
				}
			}
		}
		else if (status == 11)
		{
			if (AnCompleted())
			{
				SetDirect(n: true);
				SetAn("Run");
				status = 12;
			}
		}
		else if (status == 12)
		{
			if (base.transform.position.x > 1.87f)
			{
				room = 3;
				HeartSound();
			}
			else if (base.transform.position.x > -3.87f)
			{
				room = 2;
				HeartSound();
			}
			Move();
			if (base.transform.position.x >= 3.42f)
			{
				base.transform.position = new Vector2(3.42f, base.transform.position.y);
				SetAn("Cook");
				status = 13;
				timeAction = 0f;
				if (sunHero.activeSelf)
				{
					Singleton<Sounds>.use.So("cutting");
				}
			}
		}
		else if (status == 13)
		{
			timeAction += Time.deltaTime;
			if (timeAction > 3f)
			{
				SetDirect(n: true);
				SetAn("Run");
				status = 14;
				timeAction = 0f;
			}
		}
		else if (status == 14)
		{
			Move();
			if (base.transform.position.x >= 5.54f)
			{
				status = 1;
				timeAction = 0f;
			}
		}
		if (!sunHero.activeSelf)
		{
			return;
		}
		if (room == 1)
		{
			if (Main.hero.heroT.position.x <= -4f)
			{
				SetToHero();
			}
		}
		else if (room == 2)
		{
			if (Main.hero.heroT.position.x >= -4f && Main.hero.heroT.position.x <= 1.6f)
			{
				SetToHero();
			}
		}
		else if (room == 3 && Main.hero.heroT.position.x >= 1.6f)
		{
			SetToHero();
		}
	}

	private void Angry()
	{
		if ((timeAction += 1f) > 5f && AnCompleted())
		{
			SetGoToHero();
			Singleton<ManagerFunctions>.use.removeFunction(Angry);
		}
	}

	private void Dead()
	{
		timeAction += Time.deltaTime;
		if (!dead && timeAction > 2f)
		{
			Singleton<Sounds>.use.So("deadWitch");
			dead = true;
		}
		if (timeAction > 1f && AnCompleted())
		{
			Singleton<SteamF>.use.AddAch(11);
			Singleton<Sounds>.use.So2("fallWitch", 0.5f);
			base.gameObject.SetActive(value: false);
			witchDead.SetActive(value: true);
			Singleton<ManagerFunctions>.use.removeFunction(Dead);
		}
	}

	private void GoToHero()
	{
		if (status == 1)
		{
			Move();
			if (direct)
			{
				if (cupboard.hideHero && base.transform.position.x > -4f)
				{
					heartSo.SetActive(value: true);
				}
				if (base.transform.position.x > 0f)
				{
					if (cupboard.hideHero)
					{
						status = 2;
						SetAn("Think");
					}
					else
					{
						status = 30;
					}
				}
			}
			else if (cupboard.hideHero)
			{
				cupboard.WitchBlock();
				status = 3;
			}
		}
		else if (status == 2)
		{
			if (AnCompleted())
			{
				status = 3;
				SetDirect(n: false);
				SetAn("Run2");
			}
		}
		else if (status == 3)
		{
			Move();
			if (base.transform.position.x < -1.37f)
			{
				status = 4;
				SetAn("Door");
				cupboard.WitchBlock();
				foundInCup = true;
			}
		}
		else if (status == 4)
		{
			if (an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
			{
				cupboard.ToOpenWitch();
				status = 5;
			}
		}
		else if (status == 5)
		{
			if (AnCompleted())
			{
				timeAction = 0f;
				SetAn("See");
				status = 6;
			}
		}
		else if (status == 6)
		{
			timeAction += 1f;
			if (timeAction > 5f && AnCompleted())
			{
				ToCatch();
			}
		}
		else if (status == 30)
		{
			Move();
		}
		if (onUp && base.transform.position.x > 7.67f)
		{
			onUp = false;
			base.transform.position = new Vector2(5.66f, -4.77f);
			SetDirectToHero();
			if (!fireplace.fireMade)
			{
				sun.intensity = 0.2f;
			}
		}
		if (!foundInCup && Main.hero.gameObject.activeSelf && Mathf.Abs(base.transform.position.x - Main.hero.heroT.position.x) < 1f && Mathf.Abs(base.transform.position.y - Main.hero.heroT.position.y) < 2f)
		{
			cupboard.WitchBlock();
			ToCatch();
		}
	}

	private void Attack()
	{
		waitTime += Time.deltaTime;
		if (waitTime > 0.1f)
		{
			Main.hero.gameObject.SetActive(value: false);
		}
		if (waitTime > 1f)
		{
			Singleton<ManagerFunctions>.use.removeFunction(Attack);
			Main.game.ReLoadScene();
		}
	}

	private void HeartSound()
	{
		if (room == 2)
		{
			if (cupboard.hideHero)
			{
				heartSo.SetActive(value: true);
			}
			else
			{
				heartSo.SetActive(value: false);
			}
		}
		else
		{
			heartSo.SetActive(value: false);
		}
	}

	private void ToCatch()
	{
		SetAn("Attack");
		waitTime = 0f;
		Singleton<Sounds>.use.So("isDead");
		Singleton<InputChecker>.use.SetVibro(1f);
		Singleton<ManagerFunctions>.use.removeFunction(GoToHero);
		Singleton<ManagerFunctions>.use.addFunction(Attack);
	}

	private void SetDirectToHero()
	{
		if (sunHero.activeSelf && onUp)
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
		else if (!sunHero.activeSelf && !onUp)
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
		else
		{
			SetDirect(n: true);
		}
	}

	private void SetToHero()
	{
		if (Main.hero.gameObject.activeSelf && OnSight())
		{
			SetGoToHero();
		}
	}

	private void SetGoToHero()
	{
		SetDirectToHero();
		mushGame.EndGame();
		status = 1;
		speed = 3f;
		SetAn("Run2");
		attackMode = true;
		Singleton<ManagerFunctions>.use.addFunction(GoToHero);
		Singleton<ManagerFunctions>.use.removeFunction(ClassicActions);
		Singleton<Sounds>.use.So("witchAngry");
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
}
