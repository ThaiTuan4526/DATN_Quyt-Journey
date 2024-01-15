using UnityEngine;

public class Stump : ActiveObj
{
	public GameObject mush;

	private bool actionDone;

	private void Start()
	{
		Init();
		mush.SetActive(value: false);
	}

	public override void DoAction()
	{
		if (!mush.activeSelf && Main.game.inv.HaveItem(Items.mushroom) && Main.hero.goHero.GoTo(base.transform.position.x, this))
		{
			OverObj(n: false);
		}
	}

	public override void DoActionOnPos()
	{
		Main.hero.SetDirect(n: false);
		Main.hero.SetAn("PutMush");
		Main.game.inv.RemoveItem(Items.mushroom);
		actionDone = false;
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
	}

	private void RuntimeAction()
	{
		if (!actionDone && Main.hero.AnFrame(0.5f))
		{
			actionDone = true;
			mush.SetActive(value: true);
			Singleton<Sounds>.use.So("takeMush");
		}
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			Main.hero.ReturnToGame();
		}
	}
}
