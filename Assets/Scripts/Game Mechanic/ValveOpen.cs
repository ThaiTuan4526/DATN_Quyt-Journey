using UnityEngine;

public class ValveOpen : ActiveObj
{
	public Transform mixture;

	public Transform valve;

	private bool actionDone;

	private int countR;

	private void Start()
	{
		Init();
	}

	public override void DoAction()
	{
		Main.hero.goHero.GoTo(base.transform.position.x, this);
	}

	public override void DoActionOnPos()
	{
		Main.hero.SetDirect(n: true);
		Main.hero.SetAn("Idle");
		actionDone = false;
		countR = 0;
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
		Singleton<Sounds>.use.So("levelBowler");
	}

	private void RuntimeAction()
	{
		if (!actionDone)
		{
			valve.Rotate(500f * Time.deltaTime * Vector3.forward);
			if (++countR > 60)
			{
				if (mixture.gameObject.activeSelf)
				{
					actionDone = true;
					Singleton<Sounds>.use.So2("removeMix", 0.3f);
				}
				else
				{
					Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
					Main.hero.ReturnToGame();
				}
			}
		}
		else
		{
			mixture.Translate(Vector3.down * Time.deltaTime * 0.5f);
			if (mixture.localPosition.y < -0.05f)
			{
				Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
				Main.hero.ReturnToGame();
				mixture.localPosition = new Vector2(mixture.localPosition.x, 0.362f);
				mixture.gameObject.SetActive(value: false);
			}
		}
	}
}
