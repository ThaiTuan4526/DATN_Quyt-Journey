using NaughtyAttributes;
using UnityEngine;

public class BowlerChemistry : ActiveObj
{
	public GameObject mixture;

	[ReadOnly]
	public bool isPower;

	[ReadOnly]
	public bool isBorn;

	public bool setPower;

	public bool setBorn;

	private bool actionDone;

	private void Start()
	{
		Init();
	}

	public override void DoAction()
	{
		if (mixture.activeSelf)
		{
			Main.hero.goHero.GoTo(base.transform.position.x, this);
		}
	}

	public override void DoActionOnPos()
	{
		if (Main.game.inv.HaveItem(Items.vegetable))
		{
			Main.hero.SetDirect(n: false);
			Main.hero.SetAn("Vege");
			actionDone = false;
			Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
		}
		else if (Main.game.inv.HaveItem(Items.butterfly))
		{
			Main.hero.SetDirect(n: false);
			Main.hero.SetAn("Butterfly");
			actionDone = false;
			Singleton<ManagerFunctions>.use.addFunction(RuntimeAction2);
		}
		else
		{
			Main.hero.ReturnToGame();
			Main.hero.SetSomeAn("NoActions");
		}
	}

	private void RuntimeAction()
	{
		if (!actionDone && Main.hero.AnFrame(0.4f))
		{
			Singleton<Sounds>.use.So("toMix");
			actionDone = true;
			Main.game.inv.RemoveItem(Items.vegetable);
			Main.game.inv.AddItem(Items.vegetableMix);
			if (isPower)
			{
				setPower = true;
			}
			else
			{
				setPower = false;
			}
		}
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			Main.hero.ReturnToGame();
		}
	}

	private void RuntimeAction2()
	{
		if (!actionDone && Main.hero.AnFrame(0.4f))
		{
			Singleton<Sounds>.use.So("toMix");
			actionDone = true;
			Main.game.inv.RemoveItem(Items.butterfly);
			if (isBorn)
			{
				setBorn = true;
				Main.game.inv.AddItem(Items.butterflyMix2);
			}
			else
			{
				setBorn = false;
				Main.game.inv.AddItem(Items.butterflyMix);
			}
		}
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction2);
			Main.hero.ReturnToGame();
		}
	}
}
