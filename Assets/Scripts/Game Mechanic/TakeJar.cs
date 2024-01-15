using UnityEngine;

public class TakeJar : ActiveObj
{
	public CabOpen cab;

	public Curtain curtain;

	private bool actionDone;

	public BoxCollider2D cupCollider2D;

	private void Start()
	{
		Init();
	}

	public override void DoAction()
	{
		if (cab.isOpen && curtain.isOpen && Main.hero.goHero.GoTo(base.transform.position.x, this))
		{
			OverObj(n: false);
		}
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
			Main.game.inv.AddItem(Items.jar);
			base.gameObject.transform.GetChild(0).gameObject.SetActive(value: false);
			Singleton<Sounds>.use.So("newItem");
		}
		if (Main.hero.AnCompleted())
		{
			cupCollider2D.enabled = true;
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			Main.hero.ReturnToGame();
			DeActivated();
			base.gameObject.SetActive(value: false);
		}
	}
}
