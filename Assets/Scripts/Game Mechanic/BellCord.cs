using UnityEngine;

public class BellCord : ActiveObj
{
	public SetBell bell;

	public Animator an;

	public Monster7 monster7;

	public int id;

	private bool actionDone;

	private void Start()
	{
		Init();
	}

	public override void DoAction()
	{
		if (Main.hero.CanUse())
		{
			Main.hero.goHero.GoTo(base.transform.position.x + 0.41f, this);
			Main.hero.rb.velocity = Vector2.zero;
			Main.hero.rb.isKinematic = true;
			Main.hero.runAnyway = true;
		}
	}

	public override void DoActionOnPos()
	{
		Main.hero.SetDirect(n: false);
		Main.hero.SetAn("Cord");
		actionDone = false;
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
	}

	private void RuntimeAction()
	{
		if (!actionDone && Main.hero.AnFrame(0.55f))
		{
			actionDone = true;
			an.Play("Call");
			bell.an.Play("Call");
			Singleton<Sounds>.use.So2("cordPull", 0.7f);
			if (bell.bell.activeSelf)
			{
				monster7.SetBell(id);
				Singleton<Sounds>.use.So2("bell", 0.5f);
			}
		}
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			Main.hero.ReturnToGame();
		}
	}
}
