using UnityEngine;

public class DoorHome : ActiveObj
{
	public GameObject doorClosed;

	public GameObject doorOpened;

	public Monster1 monster;

	private bool actionDone;

	internal bool windowEnd;

	private void Start()
	{
		Init();
	}

	public override void DoAction()
	{
		if (windowEnd)
		{
			if (monster.inside)
			{
				Main.hero.goHero.GoTo(base.transform.position.x, this);
			}
			else if (monster.goOutside)
			{
				Main.hero.goHero.GoTo(base.transform.position.x, this);
			}
		}
	}

	public override void DoActionOnPos()
	{
		if (monster.inside)
		{
			actionDone = false;
			Main.hero.SetDirect(n: true);
			Main.hero.SetAn("Knock");
			Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
		}
		else if (monster.goOutside)
		{
			Singleton<ManagerFunctions>.use.addFunction(RuntimeAction2);
			Main.hero.sp.sortingLayerName = "gameobj";
			Main.hero.sp.sortingOrder = 2;
			Main.hero.SetDirect(n: true);
			Main.hero.rb.velocity = Vector2.zero;
			Main.hero.rb.isKinematic = true;
			Main.hero.SetAn(Main.hero.runAn);
			monster.RemoveAllFunctions();
		}
	}

	private void RuntimeAction()
	{
		if (!actionDone && Main.hero.AnFrame(0.2f))
		{
			actionDone = true;
			Singleton<Sounds>.use.So2("knock2", 0.5f);
		}
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			Main.hero.ReturnToGame();
			monster.SetGoOutside();
		}
	}

	private void RuntimeAction2()
	{
		Main.hero.heroT.position = new Vector2(Main.hero.heroT.position.x + 1.8f * Time.deltaTime, Main.hero.heroT.position.y + 0.59999996f * Time.deltaTime);
		if (Main.hero.heroT.position.x > 63.85f)
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction2);
			Singleton<LoaderBg>.use.SetLoader();
			Singleton<Saves>.use.keep("level2", 1);
			Main.location = 2;
			Singleton<Saves>.use.SaveData();
			Main.game.InvokeNewLoc();
		}
	}

	public void SetOpen(bool n)
	{
		doorOpened.SetActive(n);
		doorClosed.SetActive(!n);
	}
}
