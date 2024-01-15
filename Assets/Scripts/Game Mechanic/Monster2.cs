using UnityEngine;

public class Monster2 : BaseMonster1, InterfaceBreakObj
{
	internal bool isSleep;

	public Chest chest;

	public GameObject door;

	public Butterfly butterfly;

	public GameObject viewMonster;

	internal bool goToHero;

	private void Start()
	{
		Init();
		Main.game.arrBreakObj.Add(this);
		BreakObj();
	}

	public void BreakObj()
	{
		viewMonster.SetActive(value: false);
		isSleep = false;
		goToHero = false;
		waitTime = 0f;
		status = 1;
		door.SetActive(value: false);
		base.gameObject.SetActive(value: false);
		Singleton<ManagerFunctions>.use.addFunction(GoToHome);
	}

	private void GoToHome()
	{
		if (status == 1)
		{
			if (Main.hero.heroT.position.x > -5f)
			{
				waitTime += Time.deltaTime;
				if (waitTime > 6f)
				{
					status = 2;
					base.gameObject.SetActive(value: true);
					skinT.position = new Vector2(-5.83f, -3.117f);
					SetRun(n: true);
					Singleton<Sounds>.use.So("monsterComing");
				}
			}
		}
		else if (status == 2)
		{
			CheckView();
			skinT.position = new Vector2(skinT.position.x + 1.8f * Time.deltaTime, skinT.position.y);
			if (skinT.position.x >= -0.2f)
			{
				rb.bodyType = RigidbodyType2D.Kinematic;
				skinT.position = new Vector2(-0.2f, -3f);
				status = 3;
				SetAn("Seat");
				door.SetActive(value: true);
				Singleton<Sounds>.use.So("monsterGoSleep");
			}
		}
		else if (status == 3 && AnCompleted())
		{
			butterfly.Spawn();
			isSleep = true;
			viewMonster.SetActive(value: true);
			base.gameObject.GetComponent<BoxCollider2D>().enabled = false;
			base.gameObject.GetComponent<CircleCollider2D>().enabled = false;
			Singleton<ManagerFunctions>.use.removeFunction(GoToHome);
		}
	}

	private void GoToHero()
	{
		Run();
		CheckGround();
		Main.hero.SetFear(skinT.position.x);
		if (Mathf.Abs(skinT.position.x - Main.hero.heroT.position.x) < 1f)
		{
			SetAn("Attack");
			status = 1;
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
				Main.hero.gameObject.SetActive(value: false);
				status = 2;
				waitTime = 0f;
				Singleton<Sounds>.use.So("isDead");
				Singleton<InputChecker>.use.SetVibro(1f);
				if (chest.insideChest)
				{
					Singleton<SteamF>.use.AddAch(2);
				}
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

	private bool CheckView()
	{
		if (!chest.heroHide)
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
		goToHero = true;
		SetRun(n: true);
		Singleton<Sounds>.use.So("monsterGo");
		Singleton<ManagerFunctions>.use.removeFunction(GoToHome);
		Singleton<ManagerFunctions>.use.addFunction(GoToHero);
	}
}
