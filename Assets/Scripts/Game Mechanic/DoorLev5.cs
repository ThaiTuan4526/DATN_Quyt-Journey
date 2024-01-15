using UnityEngine;

public class DoorLev5 : ActiveObj
{
	public Animator lockAn;

	public GameObject door;

	private void Start()
	{
		Init();
	}

	public override void DoAction()
	{
		if (Main.hero.CanUse())
		{
			Main.hero.stopActions = true;
			DoActionOnPos();
		}
	}

	public override void DoActionOnPos()
	{
		if (Main.game.inv.HaveItem(Items.keywitch))
		{
			Main.game.inv.RemoveItem(Items.keywitch);
			lockAn.Play("Open");
			Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
			Main.hero.SetDirect(n: true);
			Main.hero.SetDirectAn("Idle");
			Singleton<Sounds>.use.So("openLock");
		}
		else
		{
			Main.hero.ReturnToGame();
			Singleton<Sounds>.use.So("lock");
		}
	}

	private void RuntimeAction()
	{
		if (lockAn.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			door.SetActive(value: false);
			Singleton<Sounds>.use.So("door_open_zvuk");
			Singleton<ManagerFunctions>.use.addFunction(RuntimeAction2);
			Main.hero.SetAn("Run");
			Main.hero.sp.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
		}
	}

	private void RuntimeAction2()
	{
		Main.hero.heroT.Translate(Vector3.right * Time.deltaTime * 2f);
		if (Main.hero.heroT.position.x > 9.5f)
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction2);
			Main.hero.sp.maskInteraction = SpriteMaskInteraction.None;
			Singleton<LoaderBg>.use.SetLoader();
			Singleton<Saves>.use.keep("level6", 1);
			Main.location = 6;
			Singleton<Saves>.use.SaveData();
			Main.game.InvokeNewLoc();
		}
	}
}
