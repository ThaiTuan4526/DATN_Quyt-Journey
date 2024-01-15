using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(GameObj))]
public class Door6_2Amber : ActiveObj
{
	public GameObject doorOutside;

	public GameObject amber;

	public Transform door;

	private int moves;

	private bool direct = true;

	private float waitTime;

	private bool toGo;

	private void Start()
	{
		Init();
		doorOutside.SetActive(value: true);
		amber.SetActive(value: false);
	}

	public override void DoAction()
	{
		if (Main.game.inv.HaveItem(Items.amber2))
		{
			Main.hero.goHero.GoTo(base.transform.position.x, this);
		}
	}

	public override void DoActionOnPos()
	{
		Main.game.inv.RemoveItem(Items.amber2);
		Main.hero.SetDirect(n: true);
		Main.hero.SetAn("Idle");
		moves = 0;
		waitTime = 0f;
		direct = true;
		Singleton<ManagerFunctions>.use.addFunction(GoDown);
		amber.SetActive(value: true);
		toGo = false;
		Singleton<Sounds>.use.So("insertAmber");
	}

	private void GoDown()
	{
		if (toGo)
		{
			if (direct)
			{
				door.transform.Translate(Time.deltaTime * 0.25f * Vector3.left);
				moves++;
				if (moves > 2)
				{
					moves = 0;
					direct = false;
				}
			}
			else
			{
				door.transform.Translate(Time.deltaTime * 0.25f * Vector3.right);
				moves++;
				if (moves > 2)
				{
					moves = 0;
					direct = true;
				}
			}
			door.transform.Translate(Vector3.down * Time.deltaTime);
			if (door.transform.localPosition.y < -2f)
			{
				doorOutside.SetActive(value: false);
				base.gameObject.SetActive(value: false);
				Singleton<ManagerFunctions>.use.removeFunction(GoDown);
				Main.hero.ReturnToGame();
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
