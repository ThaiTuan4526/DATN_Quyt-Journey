using UnityEngine;

public class DoorLevel2 : ActiveObj
{
	public Animator lockAn;

	public GameObject door1;

	public GameObject door2;

	private void Start()
	{
		door1.SetActive(value: true);
		door2.SetActive(value: false);
		Init();
	}

	public override void DoAction()
	{
		if (Main.hero.CanUse())
		{
			Main.hero.goHero.GoTo(base.transform.position.x, this);
			Main.hero.rb.isKinematic = true;
			Main.hero.runAnyway = true;
		}
	}

	public override void DoActionOnPos()
	{
		if (door2.activeSelf)
		{
			Singleton<LoaderBg>.use.SetLoader();
			Main.hero.gameObject.SetActive(value: false);
			Singleton<Saves>.use.keep("level3", 1);
			Main.location = 3;
			Singleton<Saves>.use.SaveData();
			Main.game.InvokeNewLoc();
		}
		else if (Main.game.inv.HaveItem(Items.key1))
		{
			Main.game.inv.RemoveItem(Items.key1);
			lockAn.Play("Open");
			Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
			Main.hero.SetDirect(n: false);
			Main.hero.SetIdle();
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
			Main.hero.ReturnToGame();
			door2.SetActive(value: true);
			door1.SetActive(value: false);
			Singleton<Sounds>.use.So("door_open_zvuk");
		}
	}
}
