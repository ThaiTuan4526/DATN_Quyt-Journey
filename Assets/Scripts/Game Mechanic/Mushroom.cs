using UnityEngine;

public class Mushroom : ActiveObj
{
	public GameObject mush;

	public CollectMushrooms mushS;

	private bool actionDone;

	private float posX;

	private void Start()
	{
		Init();
	}

	public override void DoAction()
	{
		if (mush.activeSelf)
		{
			if (base.transform.position.x > Main.hero.heroT.position.x)
			{
				posX = base.transform.position.x - 0.65f;
			}
			else
			{
				posX = base.transform.position.x + 0.65f;
			}
			if (Main.hero.goHero.GoTo(posX, this))
			{
				OverObj(n: false);
			}
		}
	}

	public override void DoActionOnPos()
	{
		Main.hero.SetDirect(base.transform.position.x > Main.hero.heroT.position.x);
		Main.hero.SetAn("CollectMush");
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
	}

	private void RuntimeAction()
	{
		if (!actionDone && Main.hero.AnFrame(0.3f))
		{
			actionDone = true;
			mushS.AddMush();
			mush.SetActive(value: false);
			Singleton<Sounds>.use.So("takeMush");
		}
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			Main.hero.ReturnToGame();
			DeActivated();
			OverObj(n: false);
		}
	}
}
