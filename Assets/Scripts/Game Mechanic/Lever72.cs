public class Lever72 : ActiveObj
{
	public Lift lift;

	private bool actionDone;

	private void Start()
	{
		Init();
	}

	public override void DoAction()
	{
		Main.hero.goHero.GoTo(6.2f, this);
	}

	public override void DoActionOnPos()
	{
		Main.hero.SetDirect(n: true);
		Main.hero.SetAn("Lever");
		actionDone = false;
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
	}

	private void RuntimeAction()
	{
		if (!actionDone && Main.hero.AnFrame(0.4f))
		{
			Singleton<Sounds>.use.So("levelUse");
			actionDone = true;
			lift.ToGo();
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
		}
	}
}
