using UnityEngine;

public class Leve72startGame : ActiveObj
{
	public PlatsGame platsGame;

	private bool actionDone;

	private void Start()
	{
		base.gameObject.GetComponent<Animator>().keepAnimatorControllerStateOnDisable = true;
	}

	public override void DoAction()
	{
		if (!platsGame.isGame)
		{
			Main.hero.goHero.GoTo(-9.2f, this);
		}
	}

	public override void DoActionOnPos()
	{
		Main.hero.SetDirect(n: true);
		Main.hero.SetAn("Lever");
		actionDone = false;
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
	}

	public void SetBack()
	{
		base.gameObject.GetComponent<Animator>().Play("Back");
	}

	private void RuntimeAction()
	{
		if (!actionDone && Main.hero.AnFrame(0.4f))
		{
			Singleton<Sounds>.use.So("levelUse");
			actionDone = true;
			base.gameObject.GetComponent<Animator>().Play("Lever");
			platsGame.Init();
		}
		if (Main.hero.AnCompleted())
		{
			Main.hero.ReturnToGame();
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
		}
	}
}
