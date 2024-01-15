public class TakeBucket : ActiveObj
{
	private bool actionDone;

	private void Start()
	{
		Init();
	}

	public override void DoAction()
	{
		if (Main.hero.CanUse())
		{
			Main.hero.goHero.GoTo(base.transform.position.x, this);
			Main.hero.rb.isKinematic = true;
			Main.hero.runAnyway = true;
		}
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
			Main.game.inv.AddItem(Items.bucket);
			base.gameObject.transform.GetChild(0).gameObject.SetActive(value: false);
			Singleton<Sounds>.use.So2("Setbucket", 0.7f);
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
