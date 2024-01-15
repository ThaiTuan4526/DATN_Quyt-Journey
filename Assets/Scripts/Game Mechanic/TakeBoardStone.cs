using UnityEngine;

public class TakeBoardStone : ActiveObj
{
	private bool actionDone;

	public int stoneID;

	public bool anDown;

	private void Start()
	{
		Init();
	}

	public override void DoAction()
	{
		if (Main.hero.CanUse())
		{
			Main.hero.goHero.GoTo(base.transform.position.x, this);
			Main.hero.rb.velocity = Vector2.zero;
			Main.hero.rb.isKinematic = true;
			Main.hero.runAnyway = true;
		}
	}

	public override void DoActionOnPos()
	{
		if (anDown)
		{
			Main.hero.SetDirect(n: false);
			Main.hero.SetAn("Take");
		}
		else
		{
			if (stoneID == 2)
			{
				Main.hero.SetDirect(n: true);
			}
			else
			{
				Main.hero.SetDirect(n: false);
			}
			Main.hero.SetAn("Take2");
		}
		actionDone = false;
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
	}

	private void RuntimeAction()
	{
		if (!actionDone && Main.hero.AnFrame(0.4f))
		{
			actionDone = true;
			switch (stoneID)
			{
			case 1:
				Main.game.inv.AddItem(Items.stone1);
				break;
			case 2:
				Main.game.inv.AddItem(Items.stone2);
				break;
			case 3:
				Main.game.inv.AddItem(Items.stone3);
				break;
			case 4:
				Main.game.inv.AddItem(Items.stone4);
				break;
			case 5:
				Main.game.inv.AddItem(Items.stone5);
				break;
			}
			base.gameObject.transform.GetChild(0).gameObject.SetActive(value: false);
			Singleton<Sounds>.use.So("newItem");
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
