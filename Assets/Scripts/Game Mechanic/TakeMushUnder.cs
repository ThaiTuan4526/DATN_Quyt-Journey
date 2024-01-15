using UnityEngine;

public class TakeMushUnder : ActiveObj, InterfaceBreakObj
{
	public MushAby aby;

	public MonsterUnder monster;

	private bool actionDone;

	private float posX;

	private Vector3 pos;

	private void Start()
	{
		Init();
		Main.game.arrBreakObj.Add(this);
		pos = base.gameObject.transform.position;
	}

	public void BreakObj()
	{
		base.gameObject.SetActive(value: true);
		base.gameObject.transform.position = pos;
		Activated();
	}

	public override void DoAction()
	{
		if ((monster.isSleep || monster.dead) && base.gameObject.transform.GetChild(0).gameObject.activeSelf)
		{
			if (base.transform.position.x > Main.hero.heroT.position.x)
			{
				posX = base.transform.position.x - 0.65f;
			}
			else if (base.transform.position.x + 0.65f > Main.hero.heroT.position.x)
			{
				Main.hero.stopActions = true;
				DoActionOnPos();
			}
			else
			{
				posX = base.transform.position.x + 0.65f;
			}
			if (Main.hero.goHero.GoTo(posX, this))
			{
				OverObj(n: false);
			}
		}
	}

	public override void DoActionOnPos()
	{
		Main.hero.SetDirect(base.transform.position.x > Main.hero.heroT.position.x);
		Main.hero.SetAn("Take");
		actionDone = false;
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
	}

	private void RuntimeAction()
	{
		if (!actionDone && Main.hero.AnFrame(0.5f))
		{
			actionDone = true;
			base.gameObject.SetActive(value: false);
			aby.SetEnable(base.gameObject);
			Singleton<Sounds>.use.So("takeMush");
		}
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			Main.hero.ReturnToGame();
		}
	}
}
