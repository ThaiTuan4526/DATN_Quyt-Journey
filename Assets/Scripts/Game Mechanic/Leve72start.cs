using UnityEngine;

public class Leve72start : ActiveObj
{
	public Lift lift;

	public GameObject level1;

	public GameObject level2;

	public PlatsGame liftGame;

	private bool actionDone;

	private void Start()
	{
		SetBack();
		Init();
	}

	public override void DoAction()
	{
		if (!lift.downPos)
		{
			Main.hero.goHero.GoTo(base.transform.position.x, this);
		}
	}

	public override void DoActionOnPos()
	{
		Main.hero.SetDirect(n: false);
		Main.hero.SetAn("Take2");
		actionDone = false;
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
	}

	public void SetBack()
	{
		level2.SetActive(value: false);
		level1.SetActive(value: true);
	}

	private void RuntimeAction()
	{
		if (!actionDone && Main.hero.AnFrame(0.4f))
		{
			Singleton<Sounds>.use.So("levelUse");
			liftGame.BreakGame();
			actionDone = true;
			level1.SetActive(value: false);
			level2.SetActive(value: true);
			lift.canUse = true;
			lift.ToGo(n: false);
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
		}
	}
}
