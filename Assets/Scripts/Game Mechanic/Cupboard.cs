using UnityEngine;

public class Cupboard : ActiveObj, InterfaceBreakObj
{
	public GameObject door1;

	public GameObject door2;

	public GameObject dark;

	private bool actionDone;

	internal bool hideHero;

	private void Start()
	{
		Init();
		Main.game.arrBreakObj.Add(this);
		BreakObj();
	}

	public void BreakObj()
	{
		hideHero = false;
		door2.SetActive(value: false);
		dark.SetActive(value: false);
		door1.SetActive(value: true);
	}

	public void WitchBlock()
	{
		Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
		Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction2);
		Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction3);
	}

	public void ToOpenWitch()
	{
		WitchBlock();
		Main.hero.gameObject.SetActive(value: true);
		Main.hero.SetDirect(n: true);
		door1.SetActive(value: false);
		dark.SetActive(value: false);
		door2.SetActive(value: true);
		Main.hero.SetAn("Fear");
		Main.hero.heroObj.transform.localPosition = new Vector2(-0.694f, 0.609f);
		Singleton<Sounds>.use.So2("openCupboard", 0.5f);
	}

	public override void DoActionOnPos()
	{
		Main.hero.SetDirect(n: false);
		Main.hero.SetAn("HideCup");
		actionDone = false;
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
	}

	private void RuntimeAction()
	{
		if (!actionDone && Main.hero.AnFrame(0.3f))
		{
			actionDone = true;
			door1.SetActive(value: false);
			door2.SetActive(value: true);
			Singleton<Sounds>.use.So2("openCupboard", 0.5f);
		}
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			Singleton<ManagerFunctions>.use.addFunction(RuntimeAction2);
			door2.SetActive(value: false);
			door1.SetActive(value: true);
			dark.SetActive(value: true);
			Main.hero.gameObject.SetActive(value: false);
			hideHero = true;
			Singleton<Sounds>.use.So("closeChest");
		}
	}

	private void RuntimeAction2()
	{
		if (Input.GetButtonDown("Action"))
		{
			Singleton<Sounds>.use.So2("openCupboard", 0.5f);
			Main.hero.gameObject.SetActive(value: true);
			Main.hero.SetDirect(n: true);
			Main.hero.SetAn("OutCup");
			door1.SetActive(value: false);
			dark.SetActive(value: false);
			door2.SetActive(value: true);
			hideHero = false;
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction2);
			Singleton<ManagerFunctions>.use.addFunction(RuntimeAction3);
		}
	}

	private void RuntimeAction3()
	{
		if (Main.hero.AnCompleted())
		{
			Singleton<Sounds>.use.So("closeChest");
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction3);
			door2.SetActive(value: false);
			door1.SetActive(value: true);
			Main.hero.ReturnToGame();
		}
	}
}
