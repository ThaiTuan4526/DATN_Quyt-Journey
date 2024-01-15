using UnityEngine;

public class KeyWhole : ActiveObj
{
	public StonePlat[] plats;

	public GameObject key;

	private bool actionDone;

	private bool isShow;

	private int step;

	private float timeGo;

	private void Start()
	{
		Init();
		StonePlat[] array = plats;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetHide();
		}
		key.SetActive(value: false);
	}

	public override void DoAction()
	{
		if (Main.game.inv.HaveItem(Items.key2))
		{
			Main.hero.goHero.GoTo(base.transform.position.x, this);
		}
	}

	public void CheckShow()
	{
		if (isShow)
		{
			StonePlat[] array = plats;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetShow();
			}
		}
	}

	public override void DoActionOnPos()
	{
		Singleton<Sounds>.use.So("handleCoins");
		key.SetActive(value: true);
		base.gameObject.GetComponent<Animator>().Play("Go");
		isShow = true;
		actionDone = false;
		Main.game.inv.RemoveItem(Items.key2);
		Main.hero.ReturnToGame();
		Main.hero.SetDirect(n: true);
		timeGo = -1f;
		step = 0;
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
	}

	private void RuntimeAction()
	{
		timeGo += Time.deltaTime;
		if (timeGo > 0.5f)
		{
			Singleton<Sounds>.use.So("stoneGo");
			plats[step].ToShow();
			step++;
			timeGo = 0f;
			if (step >= plats.Length)
			{
				Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			}
		}
	}
}
