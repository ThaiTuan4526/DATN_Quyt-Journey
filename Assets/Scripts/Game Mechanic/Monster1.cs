using UnityEngine;

public class Monster1 : BaseMonster1, InterfaceBreakObj
{
	public WindowHome window;

	public DoorHome door;

	public Stump stump;

	internal bool inside = true;

	internal bool goOutside;

	private bool toChew;

	private void Start()
	{
		Init();
		Main.game.arrBreakObj.Add(this);
	}

	public void BreakObj()
	{
		MoveToHome();
	}

	public void SetGoOutside()
	{
		inside = false;
		waitTime = 0f;
		status = 1;
		Singleton<ManagerFunctions>.use.addFunction(GoOutside);
	}

	private bool CheckView()
	{
		if (Main.hero.heroT.position.x < 65f)
		{
			if (Mathf.Abs(skinT.position.x - Main.hero.heroT.position.x) < 1f)
			{
				SetGoToHero();
				return true;
			}
			if (direct)
			{
				if (skinT.position.x < Main.hero.heroT.position.x)
				{
					SetGoToHero();
					return true;
				}
			}
			else if (skinT.position.x > Main.hero.heroT.position.x)
			{
				SetGoToHero();
				return true;
			}
		}
		return false;
	}

	private void SetGoToHero()
	{
		if (skinT.position.x < Main.hero.heroT.position.x)
		{
			SetRun(n: true);
		}
		else
		{
			SetRun(n: false);
		}
		Singleton<Sounds>.use.So("monsterGo");
		SetAn("Run");
		door.SetOpen(n: false);
		Singleton<ManagerFunctions>.use.removeFunction(GoOutside);
		Singleton<ManagerFunctions>.use.addFunction(GoToHero);
	}

	private void MoveToHome()
	{
		door.SetOpen(n: false);
		base.gameObject.SetActive(value: false);
		inside = true;
		goOutside = false;
		RemoveAllFunctions();
	}

	public void RemoveAllFunctions()
	{
		Singleton<ManagerFunctions>.use.removeFunction(GoOutside);
		Singleton<ManagerFunctions>.use.removeFunction(GoToHero);
		Singleton<ManagerFunctions>.use.removeFunction(CatchHero);
	}

	private void GoToPoint()
	{
		Run();
		CheckGround();
		CheckView();
	}

	private void GoOutside()
	{
		if (status == 1)
		{
			waitTime += Time.deltaTime;
			if (waitTime > 1.5f)
			{
				door.SetOpen(n: true);
				status = 2;
				base.gameObject.SetActive(value: true);
				skinT.position = new Vector2(63.58f, -4.8f);
				waitTime = 0f;
				SetRun(n: false);
				Singleton<Sounds>.use.So2("openDoor", 0.7f);
			}
		}
		else if (status == 2)
		{
			skinT.position = new Vector2(skinT.position.x - 0.59999996f * Time.deltaTime, skinT.position.y);
			if (skinT.position.x < 63.1f)
			{
				status = 3;
				SetAn("See");
				if (!stump.mush.activeSelf)
				{
					Singleton<Sounds>.use.So("monsterNo");
					waitTime = 0f;
				}
				else
				{
					waitTime = 0f;
				}
			}
		}
		else if (status == 3)
		{
			waitTime += Time.deltaTime;
			if (!CheckView() && waitTime > 2f && !window.isSee)
			{
				if (!stump.mush.activeSelf)
				{
					status = 30;
					SetRun(n: true);
					SetAn("Run");
				}
				else
				{
					status = 4;
					SetRun(n: false);
					goOutside = true;
					Singleton<Sounds>.use.So("monsterSeeMush");
				}
			}
		}
		else if (status == 4)
		{
			GoToPoint();
			if (skinT.position.x < 55.2f)
			{
				SetAn("Eat");
				toChew = false;
				waitTime = 0f;
				status = 5;
			}
		}
		else if (status == 5)
		{
			if (!toChew && an.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.1f)
			{
				stump.mush.SetActive(value: false);
				Singleton<Sounds>.use.So("takeMushMonster");
				toChew = true;
			}
			if (an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
			{
				SetAn("Chew");
				status = 6;
				waitTime = 0f;
				Singleton<Sounds>.use.So("chew");
			}
		}
		else if (status == 6)
		{
			waitTime += Time.deltaTime;
			if (toChew && waitTime > 3f)
			{
				Singleton<Sounds>.use.So("chew");
				toChew = false;
			}
			if (!CheckView() && waitTime > 6f)
			{
				status = 7;
				SetRun(n: true);
				SetAn("Run");
			}
		}
		else if (status == 7)
		{
			GoToPoint();
			if (skinT.position.x > 63.5f)
			{
				MoveToHome();
				Singleton<Sounds>.use.So("closeDoor1");
			}
		}
		else if (status == 30)
		{
			skinT.position = new Vector2(skinT.position.x + 0.59999996f * Time.deltaTime, skinT.position.y);
			if (skinT.position.x > 63.5f)
			{
				MoveToHome();
				Singleton<Sounds>.use.So("closeDoor1");
			}
		}
	}

	private void GoToHero()
	{
		Run();
		CheckGround();
		Main.hero.SetFear(skinT.position.x);
		if (Mathf.Abs(skinT.position.x - Main.hero.heroT.position.x) < 0.75f)
		{
			status = 1;
			SetAn("Attack");
			Singleton<ManagerFunctions>.use.removeFunction(GoToHero);
			Singleton<ManagerFunctions>.use.addFunction(CatchHero);
		}
	}

	private void CatchHero()
	{
		if (status == 1)
		{
			if (an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
			{
				Singleton<InputChecker>.use.SetVibro(1f);
				Main.hero.gameObject.SetActive(value: false);
				status = 2;
				waitTime = 0f;
				Singleton<Sounds>.use.So("isDead");
			}
		}
		else
		{
			waitTime += Time.deltaTime;
			if (waitTime > 1f)
			{
				Singleton<ManagerFunctions>.use.removeFunction(CatchHero);
				Main.game.ReLoadScene();
			}
		}
	}
}
