using UnityEngine;

public class AddPotion : ActiveObj
{
	public Witch witch;

	private bool actionDone;

	private void Start()
	{
		Init();
	}

	public override void DoAction()
	{
		if (!witch.potionAdded && Main.game.inv.HaveItem(Items.jarfilled))
		{
			Main.hero.goHero.GoTo(base.transform.position.x, this);
		}
	}

	public override void DoActionOnPos()
	{
		Main.hero.SetDirect(n: false);
		Main.hero.SetAn("Poison");
		actionDone = false;
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
	}

	private void RuntimeAction()
	{
		if (!actionDone && Main.hero.AnFrame(0.4f))
		{
			Singleton<Sounds>.use.So("addPoison");
			actionDone = true;
			base.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Stop();
			base.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
		}
		if (Main.hero.AnCompleted())
		{
			GetComponent<BoxCollider2D>().enabled = false;
			witch.potionAdded = true;
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			Main.hero.ReturnToGame();
		}
	}
}
