using UnityEngine;

public class LeverLift8 : ActiveObj
{
	public LiftLevel8 lift;

	public BoxCollider2D keyBox;

	private bool actionDone;

	private void Start()
	{
		Init();
	}

	public override void DoAction()
	{
		if (!Main.game.inv.HaveItem(Items.key3))
		{
			Main.hero.goHero.GoTo(-7.531f, this);
		}
	}

	public override void DoActionOnPos()
	{
		Main.hero.SetDirect(n: true);
		Main.hero.SetAn("Lever");
		actionDone = false;
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
	}

	private void RuntimeAction()
	{
		if (!actionDone && Main.hero.AnFrame(0.4f))
		{
			Singleton<Sounds>.use.So("levelUse");
			if (lift.canUse)
			{
				keyBox.enabled = false;
				GetComponent<BoxCollider2D>().enabled = false;
			}
			actionDone = true;
			lift.ToGo();
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
		}
	}
}
