using UnityEngine;

public class GrowMush : Box
{
	public GameObject mush;

	public GameObject mushLittle;

	public GameObject blockBox;

	private bool actionDone;

	private bool addMush;

	private bool addWater;

	private Box box;

	private void Start()
	{
		Init();
		mush.SetActive(value: false);
		mushLittle.SetActive(value: false);
		addMush = false;
		addWater = false;
		box = base.gameObject.GetComponent<Box>();
		blockBox.SetActive(value: false);
		InitBox();
	}

	public override void DoAction()
	{
		DoActionOnPos();
	}

	public override void DoActionOnPos()
	{
		if (!addMush && Main.game.inv.HaveItem(Items.mushroom2))
		{
			Main.hero.SetAn("Take2");
			Main.hero.stopActions = true;
			actionDone = false;
			Singleton<ManagerFunctions>.use.addFunction(Mush);
			return;
		}
		if (!addWater && addMush && Main.game.inv.HaveItem(Items.bucketwater))
		{
			Main.hero.SetAn("Water");
			Main.hero.stopActions = true;
			actionDone = false;
			Singleton<ManagerFunctions>.use.addFunction(Water);
			return;
		}
		box.SetCatch();
		if (isCatch)
		{
			blockBox.SetActive(value: true);
			Singleton<ManagerFunctions>.use.addFunction(BreakBox);
		}
	}

	private void BreakBox()
	{
		if (!isCatch)
		{
			blockBox.SetActive(value: false);
			Singleton<ManagerFunctions>.use.removeFunction(BreakBox);
		}
	}

	private void Water()
	{
		if (!actionDone)
		{
			if (Main.hero.AnFrame(0.4f))
			{
				actionDone = true;
				Main.game.inv.RemoveItem(Items.bucketwater);
				Main.game.inv.AddItem(Items.bucket);
				mushLittle.GetComponent<Animator>().Play("grow");
				Singleton<Sounds>.use.So("growMush");
			}
		}
		else if (mushLittle.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
		{
			addWater = true;
			mush.SetActive(value: true);
			mushLittle.SetActive(value: false);
			Singleton<ManagerFunctions>.use.removeFunction(Water);
			Main.hero.ReturnToGame();
		}
	}

	private void Mush()
	{
		if (!actionDone && Main.hero.AnFrame(0.4f))
		{
			actionDone = true;
			Main.game.inv.RemoveItem(Items.mushroom2);
			mushLittle.SetActive(value: true);
			addMush = true;
			Singleton<Sounds>.use.So("addMush");
		}
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(Mush);
			Main.hero.ReturnToGame();
		}
	}
}
