public class ViewObj : ActiveObj
{
	public bool sideView;

	public string anName;

	private void Start()
	{
		Init();
	}

	public override void DoAction()
	{
		Main.hero.goHero.GoTo(base.transform.position.x, this);
	}

	public override void DoActionOnPos()
	{
		Main.hero.SetDirect(sideView);
		Main.hero.SetAn(anName);
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
	}

	private void RuntimeAction()
	{
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			Main.hero.ReturnToGame();
		}
	}
}
