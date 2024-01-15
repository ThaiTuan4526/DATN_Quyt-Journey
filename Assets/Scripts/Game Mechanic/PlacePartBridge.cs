public class PlacePartBridge : ActiveObj, InterfaceBreakObj
{
	public BridgeParts parts;

	private bool actionDone;

	private void Start()
	{
		Init();
		Main.game.arrBreakObj.Add(this);
	}

	public void BreakObj()
	{
		base.gameObject.SetActive(value: true);
		Activated();
	}

	public override void DoAction()
	{
	}

	public override void DoActionOnPos()
	{
		Main.hero.SetAn("Take");
		Main.hero.stopActions = true;
		actionDone = false;
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
	}

	private void RuntimeAction()
	{
		if (!actionDone && Main.hero.AnFrame(0.4f))
		{
			actionDone = true;
			Singleton<Sounds>.use.So("placePartBridge");
			Main.game.inv.RemoveItem(Items.bridgepart);
			_ = Main.hero.direct;
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
