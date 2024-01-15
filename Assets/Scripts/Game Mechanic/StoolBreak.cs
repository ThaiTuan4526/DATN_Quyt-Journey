using UnityEngine;

public class StoolBreak : ActiveObj
{
	public GameObject stoolBox;

	public Monster2 monster2;

	private void Start()
	{
		Init();
	}

	public override void DoAction()
	{
		if (monster2.isSleep)
		{
			if (Main.game.inv.HaveItem(Items.stoolpart))
			{
				Main.game.inv.RemoveItem(Items.stoolpart);
				Main.hero.goHero.GoTo(base.transform.position.x, this);
			}
		}
		else
		{
			monster2.waitTime = 10f;
		}
	}

	public override void DoActionOnPos()
	{
		Main.hero.SetDirect(n: true);
		Main.hero.ReturnToGame();
		Singleton<Sounds>.use.So("insertStool");
		stoolBox.SetActive(value: true);
		DeActivated();
		base.gameObject.SetActive(value: false);
	}
}
