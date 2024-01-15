using UnityEngine;

public class ElevatorKey : ActiveObj
{
	public LiftLevel8 lift;

	public GameObject key;

	private int step;

	private float timeGo;

	private void Start()
	{
		Init();
		key.SetActive(value: false);
	}

	public override void DoAction()
	{
		if (Main.game.inv.HaveItem(Items.key3))
		{
			Main.hero.goHero.GoTo(base.transform.position.x, this);
		}
	}

	public override void DoActionOnPos()
	{
		Singleton<Sounds>.use.So("handleCoins");
		lift.canUse = true;
		key.SetActive(value: true);
		Main.game.inv.RemoveItem(Items.key3);
		Main.hero.ReturnToGame();
		Main.hero.SetDirect(n: false);
		DeActivated();
	}
}
