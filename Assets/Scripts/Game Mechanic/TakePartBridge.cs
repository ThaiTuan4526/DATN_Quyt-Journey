using UnityEngine;

public class TakePartBridge : ActiveObj, InterfaceBreakObj
{
	public BridgeParts parts;

	public Transform monsterBridge;

	public int pos;

	private bool actionDone;

	private void Start()
	{
		Init();
		Main.game.arrBreakObj.Add(this);
	}

	public void BreakObj()
	{
		if (Main.locationStep != 3)
		{
			Main.game.inv.RemoveItem(Items.bridgepart);
		}
	}

	public override void DoAction()
	{
		if (!(monsterBridge.position.x > 80f) || !Main.hero.CanUse() || (Main.game.inv.HaveItem(Items.bridgepart) && ((!Main.hero.direct && pos <= 0) || (Main.hero.direct && pos >= 7))))
		{
			return;
		}
		if (!Main.game.inv.HaveItem(Items.bridgepart))
		{
			if ((!Main.hero.direct && pos <= 0) || (Main.hero.direct && pos >= 7))
			{
				return;
			}
			int num = pos;
			num = ((!Main.hero.direct) ? (num - 1) : (num + 1));
			if (!parts.parts[num].activeSelf)
			{
				return;
			}
		}
		DoActionOnPos();
	}

	public void UpdateCollider()
	{
		if (!parts.parts[pos].activeSelf)
		{
			GetComponent<BoxCollider2D>().enabled = false;
		}
		else
		{
			GetComponent<BoxCollider2D>().enabled = true;
		}
	}

	public override void DoActionOnPos()
	{
		Main.hero.SetAn("Take");
		Main.hero.stopActions = true;
		actionDone = false;
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
	}

	private void RuntimeAction()
	{
		if (!actionDone && Main.hero.AnFrame(0.4f))
		{
			actionDone = true;
			if (Main.game.inv.HaveItem(Items.bridgepart))
			{
				Singleton<Sounds>.use.So("placePartBridge");
				Main.game.inv.RemoveItem(Items.bridgepart);
				parts.SetNextPart(pos, Main.hero.direct);
			}
			else
			{
				Singleton<Sounds>.use.So("newItem");
				Main.game.inv.AddItem(Items.bridgepart);
				parts.TakePart(pos, Main.hero.direct);
			}
		}
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			Main.hero.ReturnToGame();
		}
	}
}
