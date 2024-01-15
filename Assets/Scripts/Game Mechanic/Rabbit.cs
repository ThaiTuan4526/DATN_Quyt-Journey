using UnityEngine;

public class Rabbit : ActiveObj
{
	private bool actionDone;

	public Animator an;

	private bool direct = true;

	internal bool fryEnd;

	private void Start()
	{
		Init();
	}

	public override void DoAction()
	{
		if (!Main.game.inv.HaveItem(Items.meat) && Main.game.inv.HaveItem(Items.branch) && Main.hero.CanUse())
		{
			if (base.transform.position.x > Main.hero.heroT.position.x)
			{
				Main.hero.SetDirect(n: true);
			}
			else
			{
				Main.hero.SetDirect(n: false);
			}
			Main.hero.stopActions = true;
			Singleton<ManagerFunctions>.use.removeFunction(Fry);
			DoActionOnPos();
		}
	}

	public override void DoActionOnPos()
	{
		if (base.transform.position.x > Main.hero.heroT.position.x)
		{
			Main.hero.SetDirect(n: true);
		}
		else
		{
			Main.hero.SetDirect(n: false);
		}
		Main.hero.SetAn("Take2");
		actionDone = false;
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
	}

	private void RuntimeAction()
	{
		if (!actionDone && Main.hero.AnFrame(0.4f))
		{
			if (direct)
			{
				if (!fryEnd)
				{
					an.Play("go");
				}
				else
				{
					an.Play("go2");
				}
			}
			else if (!fryEnd)
			{
				an.Play("go1");
			}
			else
			{
				an.Play("go3");
			}
			Singleton<Sounds>.use.So("rotateMeat");
			direct = !direct;
			actionDone = true;
		}
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			Main.hero.ReturnToGame();
			if (!direct && !fryEnd)
			{
				Singleton<ManagerFunctions>.use.addFunction(Fry);
				an.Play("fry");
			}
		}
	}

	private void Fry()
	{
		if (an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
		{
			Singleton<Sounds>.use.So("fry");
			fryEnd = true;
			Singleton<ManagerFunctions>.use.removeFunction(Fry);
		}
	}
}
