using UnityEngine;

public class SetBell : ActiveObj
{
	public GameObject bell;

	public Animator an;

	private bool actionDone;

	private void Start()
	{
		Init();
		bell.SetActive(value: false);
	}

	public override void DoAction()
	{
		if ((bell.activeSelf || Main.game.inv.HaveItem(Items.bell)) && Main.hero.CanUse())
		{
			Main.hero.goHero.GoTo(base.transform.position.x + 0.35f, this);
			Main.hero.rb.velocity = Vector2.zero;
			Main.hero.rb.isKinematic = true;
			Main.hero.runAnyway = true;
		}
	}

	public override void DoActionOnPos()
	{
		Main.hero.SetDirect(n: false);
		Main.hero.SetAn("Bell");
		actionDone = false;
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
	}

	private void RuntimeAction()
	{
		if (!actionDone && Main.hero.AnFrame(0.52f))
		{
			actionDone = true;
			Singleton<Sounds>.use.So("takeBell");
			if (bell.activeSelf)
			{
				bell.SetActive(value: false);
				Main.game.inv.AddItem(Items.bell);
			}
			else
			{
				bell.SetActive(value: true);
				Main.game.inv.RemoveItem(Items.bell);
			}
		}
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			Main.hero.ReturnToGame();
		}
	}
}
