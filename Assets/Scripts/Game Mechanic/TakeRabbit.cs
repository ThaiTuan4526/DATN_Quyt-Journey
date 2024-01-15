using UnityEngine;

public class TakeRabbit : ActiveObj
{
	private bool actionDone;

	public GameObject rabbit;

	private void Start()
	{
		Init();
	}

	public override void DoActionOnPos()
	{
		Main.hero.SetAn("Take2");
		actionDone = false;
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
	}

	private void RuntimeAction()
	{
		if (!actionDone && Main.hero.AnFrame(0.4f))
		{
			actionDone = true;
			Main.game.inv.AddItem(Items.meat);
			rabbit.SetActive(value: false);
			Singleton<Sounds>.use.So("takeMeat");
		}
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			Main.hero.ReturnToGame();
			DeActivated();
			base.gameObject.SetActive(value: false);
		}
	}
}
