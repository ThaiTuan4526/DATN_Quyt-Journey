using UnityEngine;

public class BridgeLever : ActiveObj, InterfaceBreakObj
{
	public Animator bridge;

	public Animator bridgeMech;

	public GameObject plat;

	public GameObject lever;

	private bool isSet;

	internal bool isDown;

	private bool actionDone;

	private void Start()
	{
		Init();
		Main.game.arrBreakObj.Add(this);
		lever.SetActive(value: false);
		isDown = false;
	}

	public override void DoAction()
	{
		if (!isSet)
		{
			if (Main.game.inv.HaveItem(Items.lever1))
			{
				Main.hero.goHero.GoTo(base.transform.position.x, this);
			}
		}
		else if (!isDown)
		{
			Main.hero.goHero.GoTo(97.256f, this);
		}
		else
		{
			Main.hero.goHero.GoTo(97.70599f, this);
		}
	}

	public override void DoActionOnPos()
	{
		if (!isSet)
		{
			Main.hero.ReturnToGame();
			isSet = true;
			Main.game.inv.RemoveItem(Items.lever1);
			lever.SetActive(value: true);
			Singleton<Sounds>.use.So("switch_zvuk");
			return;
		}
		if (!isDown)
		{
			Main.hero.SetDirect(n: true);
		}
		else
		{
			Main.hero.SetDirect(n: false);
		}
		isDown = !isDown;
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
			if (isDown)
			{
				plat.SetActive(value: false);
				bridge.Play("Open");
				bridgeMech.Play("Open");
			}
			else
			{
				plat.SetActive(value: true);
				bridge.Play("Close");
				bridgeMech.Play("Close");
			}
		}
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			Main.hero.ReturnToGame();
		}
	}

	public void StopF()
	{
		Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
	}

	public void BreakObj()
	{
		plat.SetActive(value: true);
		bridge.Play("Idle");
		bridgeMech.Play("Idle");
		isDown = false;
	}
}
