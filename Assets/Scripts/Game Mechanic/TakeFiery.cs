public class TakeFiery : ActiveObj
{
	private bool actionDone;

	private void Start()
	{
		Init();
	}

	public override void DoActionOnPos()
	{
		Main.hero.SetDirect(n: false);
		Main.hero.SetAn("Take2");
		actionDone = false;
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
	}

	private void RuntimeAction()
	{
		if (!actionDone && Main.hero.AnFrame(0.4f))
		{
			actionDone = true;
			Main.game.inv.AddItem(Items.fiery);
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
