public class TakeNeedle : ActiveObj
{
	private bool actionDone;

	private void Start()
	{
		Init();
	}

	public override void DoActionOnPos()
	{
		Main.hero.SetAn("Take");
		actionDone = false;
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
	}

	private void RuntimeAction()
	{
		if (!actionDone && Main.hero.AnFrame(0.4f))
		{
			actionDone = true;
			Main.game.inv.AddItem(Items.needle);
			base.gameObject.transform.GetChild(0).gameObject.SetActive(value: false);
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
