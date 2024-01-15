using UnityEngine;

public class ButtonTake : ActiveObj
{
	public int buttonID;

	private bool actionDone;

	private void Start()
	{
		Init(isOver: false);
		if (Singleton<Saves>.use.save("button" + buttonID) == 1)
		{
			DeActivated();
			base.gameObject.SetActive(value: false);
		}
	}

	public override void DoActionOnPos()
	{
		Main.hero.SetDirect(n: true);
		Main.hero.SetAn("Take");
		actionDone = false;
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
	}

	private void RuntimeAction()
	{
		if (!actionDone)
		{
			if (Main.hero.AnFrame(0.4f))
			{
				actionDone = true;
				Singleton<Sounds>.use.So("takeButton");
				base.gameObject.transform.GetChild(0).gameObject.SetActive(value: false);
			}
		}
		else
		{
			if (!Main.hero.AnCompleted())
			{
				return;
			}
			Singleton<Saves>.use.keep("button" + buttonID, 1);
			bool flag = true;
			for (int i = 1; i <= 3; i++)
			{
				if (Singleton<Saves>.use.save("button" + i) == 0)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				Singleton<SteamF>.use.AddAch(1);
			}
			Singleton<Sounds>.use.So("cloudButton");
			(Object.Instantiate(Resources.Load("Prefabs/GameObj/Cloud"), Main.hero.heroT, instantiateInWorldSpace: false) as GameObject).transform.localPosition = Vector3.zero;
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			DeActivated();
			base.gameObject.SetActive(value: false);
		}
	}
}
