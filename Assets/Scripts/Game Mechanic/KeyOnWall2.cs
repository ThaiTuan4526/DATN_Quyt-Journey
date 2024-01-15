using UnityEngine;

public class KeyOnWall2 : ActiveObj
{
	private bool actionDone;

	public GameObject key2;

	private void Start()
	{
		Init();
	}

	public override void DoAction()
	{
		if (Main.hero.CanUse())
		{
			DoActionOnPos();
		}
	}

	public override void DoActionOnPos()
	{
		Main.hero.stopActions = true;
		Main.hero.SetAn("Bell");
		actionDone = false;
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
	}

	private void RuntimeAction()
	{
		if (!actionDone && Main.hero.AnFrame(0.55f))
		{
			actionDone = true;
			Main.game.inv.AddItem(Items.key1);
			base.gameObject.SetActive(value: false);
			key2.SetActive(value: false);
			Singleton<Sounds>.use.So("newItem");
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
