using UnityEngine;

public class DoorTreeLeft : ActiveObj
{
	public Transform door;

	public GameObject block;

	public GameObject amber;

	private float waitTime;

	private bool toGo;

	private void Start()
	{
		Init();
		amber.SetActive(value: false);
	}

	public override void DoAction()
	{
		if (Main.game.inv.HaveItem(Items.amber1))
		{
			Main.hero.goHero.GoTo(base.transform.position.x, this);
		}
	}

	public override void DoActionOnPos()
	{
		Main.game.inv.RemoveItem(Items.amber1);
		Main.hero.SetDirect(n: true);
		Main.hero.SetAn("Idle");
		block.SetActive(value: false);
		amber.SetActive(value: true);
		waitTime = 0f;
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
		Main.hero.stopActions = true;
		toGo = false;
		Singleton<Sounds>.use.So("insertAmber");
	}

	private void RuntimeAction()
	{
		if (toGo)
		{
			door.Translate(Vector3.down * Time.deltaTime);
			if (door.transform.localPosition.y < -2.5f)
			{
				Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
				Main.hero.ReturnToGame();
				DeActivated();
				base.gameObject.SetActive(value: false);
			}
		}
		else
		{
			waitTime += Time.deltaTime;
			if (waitTime > 1f)
			{
				Singleton<Sounds>.use.So("openDoorAmber");
				toGo = true;
			}
		}
	}
}
