using UnityEngine;

public class Chest : ActiveObj, InterfaceBreakObj
{
	public Monster2 monster2;

	public GameObject cheshOpen;

	public GameObject cheshClose;

	public GameObject eyeHero;

	public bool isOpen;

	public bool heroHide;

	internal bool insideChest;

	private void Start()
	{
		Init();
		SetOpen(n: false);
		eyeHero.SetActive(value: false);
		Main.game.arrBreakObj.Add(this);
	}

	public void BreakObj()
	{
		insideChest = false;
		heroHide = false;
		SetOpen(n: false);
		eyeHero.SetActive(value: false);
		Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction2);
		Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction3);
		Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction4);
		Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction5);
	}

	public override void DoActionOnPos()
	{
		if (isOpen)
		{
			if (!heroHide && !monster2.goToHero)
			{
				Main.hero.SetDirect(n: true);
				Main.hero.SetAn("ToChest");
				Singleton<Sounds>.use.So("toChest");
				Main.hero.rb.isKinematic = true;
				Main.hero.heroT.position = new Vector2(Main.hero.heroT.position.x, -2.684f);
				Singleton<ManagerFunctions>.use.addFunction(RuntimeAction2);
			}
		}
		else
		{
			Singleton<Sounds>.use.So("openChest");
			Main.hero.ReturnToGame();
			SetOpen(n: true);
		}
	}

	private void SetOpen(bool n)
	{
		cheshOpen.SetActive(n);
		cheshClose.SetActive(!n);
		isOpen = n;
	}

	private void RuntimeAction2()
	{
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction2);
			Singleton<ManagerFunctions>.use.addFunction(RuntimeAction3);
			insideChest = true;
		}
	}

	private void RuntimeAction3()
	{
		if (!monster2.goToHero && Input.GetButtonDown("Action"))
		{
			Main.hero.gameObject.SetActive(value: false);
			heroHide = true;
			monster2.waitTime = 5f;
			SetOpen(n: false);
			eyeHero.SetActive(value: true);
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction3);
			Singleton<ManagerFunctions>.use.addFunction(RuntimeAction4);
			Singleton<Sounds>.use.So("closeChest");
		}
	}

	private void RuntimeAction4()
	{
		if (monster2.isSleep && Input.GetButtonDown("Action"))
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction4);
			Singleton<ManagerFunctions>.use.addFunction(RuntimeAction5);
			eyeHero.SetActive(value: false);
			SetOpen(n: true);
			Main.hero.gameObject.SetActive(value: true);
			Main.hero.SetAn("OutChest");
			Singleton<Sounds>.use.So("openChest");
		}
	}

	private void RuntimeAction5()
	{
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction5);
			DeActivated();
			Main.hero.rb.isKinematic = false;
			Main.hero.heroT.position = new Vector2(Main.hero.heroT.position.x, -3.117f);
			Main.hero.SetDirect(n: false);
			Main.hero.ReturnToGame();
		}
	}
}
