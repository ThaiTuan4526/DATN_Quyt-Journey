public class BasketFallen : ActiveObj
{
	private bool actionDone;

	private void Start()
	{
		Init();
	}

	public override void DoAction()
	{
		if (!Main.game.inv.HaveItem(Items.mushroom))
		{
			Main.hero.goHero.GoTo(base.transform.position.x, this);
		}
	}

	public override void DoActionOnPos()
	{
		actionDone = false;
		Main.hero.SetAn("Take");
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
	}

	private void RuntimeAction()
	{
		if (!actionDone && Main.hero.AnFrame(0.4f))
		{
			actionDone = true;
			Singleton<Sounds>.use.So("takeMush");
			Main.game.inv.AddItem(Items.mushroom);
		}
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			Main.hero.ReturnToGame();
		}
	}
}
