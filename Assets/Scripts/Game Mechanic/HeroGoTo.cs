public class HeroGoTo
{
	private bool direct;

	private float pos;

	private Hero hero;

	private GameObjInterface gameObjInterface;

	public void Init(Hero h)
	{
		hero = h;
	}

	public bool GoTo(float n, GameObjInterface g)
	{
		if (hero.CanUse())
		{
			if (hero.heroT.position.x > n)
			{
				direct = false;
				hero.goX = -1f;
			}
			else
			{
				direct = true;
				hero.goX = 1f;
			}
			pos = n;
			hero.stopActions = true;
			Singleton<ManagerFunctions>.use.addFunction(Go);
			gameObjInterface = g;
			return true;
		}
		return false;
	}

	private void Go()
	{
		hero.Run();
		if ((direct && hero.heroT.position.x > pos) || (!direct && hero.heroT.position.x < pos))
		{
			Singleton<ManagerFunctions>.use.removeFunction(Go);
			gameObjInterface.DoActionOnPos();
		}
	}
}
