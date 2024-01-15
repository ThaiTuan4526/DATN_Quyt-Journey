using UnityEngine;

public class AddFish : ActiveObj
{
	public Animator an;

	public GameObject fish;

	private bool actionDone;

	public Winch winch;

	private void Start()
	{
		Init();
		fish.SetActive(value: false);
		UpdateIcon();
	}

	public override void DoAction()
	{
		if (Main.game.inv.HaveItem(Items.fish))
		{
			float normalizedTime = an.GetCurrentAnimatorStateInfo(0).normalizedTime;
			if ((!winch.directAnDrag && normalizedTime > 0.15f && normalizedTime < 0.4f) || (winch.directAnDrag && normalizedTime > 0.6f && normalizedTime < 0.83f))
			{
				Main.hero.goHero.GoTo(base.transform.position.x, this);
			}
		}
	}

	public void UpdateIcon()
	{
		float normalizedTime = an.GetCurrentAnimatorStateInfo(0).normalizedTime;
		if ((!winch.directAnDrag && normalizedTime > 0.15f && normalizedTime < 0.4f) || (winch.directAnDrag && normalizedTime > 0.6f && normalizedTime < 0.83f))
		{
			icon.transform.localPosition = new Vector2(-1f, 2f);
		}
		else
		{
			icon.transform.localPosition = new Vector2(0f, -50f);
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
			Singleton<Sounds>.use.So("placeMeat");
			fish.SetActive(value: true);
			Main.game.inv.RemoveItem(Items.fish);
		}
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			Main.hero.ReturnToGame();
		}
	}
}
