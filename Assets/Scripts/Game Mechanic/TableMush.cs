using UnityEngine;

public class TableMush : ActiveObj
{
	public Fireplace fireplace;

	public GameObject table;

	public GameObject hand;

	public GameObject hand1;

	public GameObject hand2;

	public GameObject cat;

	public bool taked;

	public int type;

	public GameObject mushsTable;

	public GameObject[] mushs;

	private GameObject mushTake;

	private Vector2 posMushTaked;

	public Witch witch;

	private bool isGame;

	private Vector3[] savedMushRotate;

	private void Start()
	{
		Init();
		savedMushRotate = new Vector3[mushs.Length];
		for (int i = 0; i < mushs.Length; i++)
		{
			savedMushRotate[i] = mushs[i].transform.localEulerAngles;
		}
		table.SetActive(value: false);
	}

	public override void DoAction()
	{
		if (!witch.attackMode && !fireplace.potionCreated && fireplace.fireMade && Main.hero.goHero.GoTo(base.transform.position.x, this))
		{
			OverObj(n: false);
		}
	}

	public override void DoActionOnPos()
	{
		hand.transform.position = new Vector2(1.73f, -2.55f);
		if (taked)
		{
			taked = false;
			mushTake.GetComponent<SpriteRenderer>().sortingOrder = 1;
			mushTake.transform.SetParent(mushsTable.transform);
			mushTake.transform.localEulerAngles = Vector3.zero;
			mushTake.transform.position = posMushTaked;
		}
		isGame = true;
		SetTake(n: false);
		table.SetActive(value: true);
		cat.SetActive(value: false);
		Main.hero.SetAn("Idle");
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
	}

	private void SetTake(bool n)
	{
		hand1.SetActive(!n);
		hand2.SetActive(n);
	}

	public void EndGame()
	{
		if (isGame)
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			Main.hero.ReturnToGame();
			cat.SetActive(value: true);
			if (taked)
			{
				Main.game.inv.RemoveItem(Items.mushwitch);
				Main.game.inv.AddItem(Items.mushwitch, type);
				fireplace.typeMushWitch = type;
			}
			table.SetActive(value: false);
			isGame = false;
		}
	}

	private void RuntimeAction()
	{
		float x = Input.GetAxis("Horizontal") + Input.GetAxis("HorizontalJoy");
		float axis = Input.GetAxis("Vertical");
		Vector2 zero = Vector2.zero;
		zero.x = x;
		zero.y = axis;
		zero *= 0.15f;
		hand.transform.Translate(zero * 60f * Time.deltaTime);
		if (hand.transform.position.x > 5.5f)
		{
			hand.transform.position = new Vector2(5.5f, hand.transform.position.y);
		}
		if (hand.transform.position.x < -5.5f)
		{
			hand.transform.position = new Vector2(-5.5f, hand.transform.position.y);
		}
		if (hand.transform.position.y > 2.16f)
		{
			hand.transform.position = new Vector2(hand.transform.position.x, 2.16f);
		}
		if (hand.transform.position.y < -4.6f)
		{
			EndGame();
		}
		else
		{
			if (!Input.GetButtonDown("Action"))
			{
				return;
			}
			if (!taked)
			{
				for (int i = 0; i < mushs.Length; i++)
				{
					mushTake = mushs[i];
					if (Mathf.Abs(mushTake.transform.position.x - hand.transform.position.x) < 0.975f && Mathf.Abs(mushTake.transform.position.y - hand.transform.position.y) < 0.975f)
					{
						taked = true;
						type = i + 1;
						SetTake(n: true);
						posMushTaked = mushTake.transform.position;
						mushTake.transform.SetParent(hand.transform);
						mushTake.GetComponent<SpriteRenderer>().sortingOrder = 2;
						mushTake.transform.localPosition = new Vector2(-0.476f, -0.301f);
						mushTake.transform.localEulerAngles = new Vector3(0f, 0f, 100f);
						Singleton<Sounds>.use.So("tableTake1");
						break;
					}
				}
			}
			else
			{
				taked = false;
				mushTake.transform.SetParent(mushsTable.transform);
				mushTake.transform.localEulerAngles = savedMushRotate[type - 1];
				mushTake.GetComponent<SpriteRenderer>().sortingOrder = 1;
				SetTake(n: false);
				Singleton<Sounds>.use.So("tableTake1");
			}
		}
	}
}
