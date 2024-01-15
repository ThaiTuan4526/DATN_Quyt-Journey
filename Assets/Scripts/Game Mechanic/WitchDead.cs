using UnityEngine;

public class WitchDead : ActiveObj
{
	private bool actionDone;

	public GameObject w1;

	public GameObject w2;

	private void Start()
	{
		Init();
		w1.SetActive(value: true);
		w2.SetActive(value: false);
	}

	public override void DoActionOnPos()
	{
		Main.hero.SetDirect(n: false);
		Main.hero.SetAn("Take");
		actionDone = false;
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
	}

	private void RuntimeAction()
	{
		if (!actionDone && Main.hero.AnFrame(0.4f))
		{
			actionDone = true;
			Main.game.inv.AddItem(Items.keywitch);
			w2.SetActive(value: true);
			w1.SetActive(value: false);
			Singleton<Sounds>.use.So("newItem");
		}
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			Main.hero.ReturnToGame();
			DeActivated();
		}
	}
}
