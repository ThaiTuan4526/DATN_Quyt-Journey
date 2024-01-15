using UnityEngine;

public class Raven : ActiveObj
{
	private bool actionDone;

	private bool actionDone2;

	private bool stoneInTooth = true;

	public Animator ravenAn;

	public Animator stoneAn;

	public BoxCollider2D stoneOnFall;

	private void Start()
	{
		Init();
		stoneOnFall.enabled = false;
	}

	public override void DoAction()
	{
		if (stoneInTooth && Main.game.inv.HaveItem(Items.mask))
		{
			Main.hero.goHero.GoTo(base.transform.position.x + 1f, this);
		}
	}

	public override void DoActionOnPos()
	{
		Main.hero.SetDirect(n: false);
		Main.hero.SetAn("ToScary");
		actionDone = false;
		actionDone2 = false;
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
	}

	private void RuntimeAction()
	{
		if (!actionDone2 && Main.hero.AnFrame(0.28f))
		{
			Singleton<Sounds>.use.So("boo");
			actionDone2 = true;
		}
		if (!actionDone && Main.hero.AnFrame(0.5f))
		{
			Singleton<Sounds>.use.So("raven");
			actionDone = true;
			ravenAn.Play("crow2");
			stoneAn.Play("drop");
			stoneInTooth = false;
			stoneOnFall.enabled = true;
			Main.game.inv.RemoveItem(Items.mask);
		}
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			Main.hero.ReturnToGame();
		}
	}
}
