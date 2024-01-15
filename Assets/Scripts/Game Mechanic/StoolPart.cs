public class StoolPart : ActiveObj, InterfaceBreakObj
{
	private bool actionDone;

	private void Start()
	{
		Init();
		Main.game.arrBreakObj.Add(this);
	}

	public void BreakObj()
	{
		Main.game.inv.RemoveItem(Items.stoolpart);
		base.gameObject.SetActive(value: true);
		base.gameObject.transform.GetChild(0).gameObject.SetActive(value: true);
		Activated();
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
			Singleton<Sounds>.use.So("newItem");
			Main.game.inv.AddItem(Items.stoolpart);
			base.gameObject.transform.GetChild(0).gameObject.SetActive(value: false);
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
