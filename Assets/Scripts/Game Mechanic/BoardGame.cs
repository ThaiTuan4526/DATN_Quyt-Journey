using UnityEngine;

public class BoardGame : ActiveObj
{
	private bool actionDone;

	public GameObject board;

	public Monster7 monster7;

	public BoardStone[] stones;

	public GameObject[] stonesOnLoc;

	public GameObject hand;

	public GameObject hand1;

	public GameObject hand2;

	public GameObject hand3;

	public GameObject hand4;

	public GameObject key;

	internal bool isGame;

	private BoardStone stone;

	private bool stoneGo;

	private float waitTime;

	private bool win;

	private bool canPlay;

	private bool takeKey;

	private Transform camT;

	private SpriteRenderer keySp;

	private float myA;

	private GameObject ns;

	private Animator an;

	private bool cloudEx;

	private void Start()
	{
		Init();
		takeKey = false;
		keySp = key.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
		for (int i = 0; i < stonesOnLoc.Length; i++)
		{
			stonesOnLoc[i].SetActive(value: false);
			stones[i + 1].gameObject.SetActive(value: false);
		}
		stones[0].ToShow();
		board.SetActive(value: false);
		hand1.SetActive(value: true);
		hand2.SetActive(value: false);
		hand3.SetActive(value: false);
		hand4.SetActive(value: false);
		key.SetActive(value: false);
		isGame = false;
	}

	public override void DoAction()
	{
		if (takeKey)
		{
			return;
		}
		if (monster7.jump)
		{
			if (Main.hero.goHero.GoTo(base.transform.position.x, this))
			{
				OverObj(n: false);
			}
		}
		else if (!cloudEx)
		{
			ns = Object.Instantiate(Resources.Load("Prefabs/GameObj/Cloud4"), Main.hero.heroT, instantiateInWorldSpace: false) as GameObject;
			ns.transform.localPosition = Vector3.zero;
			an = ns.transform.GetChild(1).gameObject.GetComponent<Animator>();
			cloudEx = true;
			Singleton<ManagerFunctions>.use.addIndieFunction(CloudAnEnd);
		}
	}

	private void CloudAnEnd()
	{
		if (an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
		{
			cloudEx = false;
			Object.Destroy(ns);
			Singleton<ManagerFunctions>.use.removeIndieFunction(CloudAnEnd);
		}
	}

	public override void DoActionOnPos()
	{
		camT = Main.game.cam.transform;
		board.transform.position = new Vector3(camT.position.x, camT.position.y, 0f);
		hand.transform.localPosition = new Vector2(0f, -3.04f);
		if (Main.game.inv.HaveItem(Items.stone1))
		{
			stones[1].ToShow();
			stonesOnLoc[0].SetActive(value: true);
			Main.game.inv.RemoveItem(Items.stone1);
		}
		if (Main.game.inv.HaveItem(Items.stone2))
		{
			stones[2].ToShow();
			stonesOnLoc[1].SetActive(value: true);
			Main.game.inv.RemoveItem(Items.stone2);
		}
		if (Main.game.inv.HaveItem(Items.stone3))
		{
			stones[3].ToShow();
			stonesOnLoc[2].SetActive(value: true);
			Main.game.inv.RemoveItem(Items.stone3);
		}
		if (Main.game.inv.HaveItem(Items.stone4))
		{
			stones[4].ToShow();
			stonesOnLoc[3].SetActive(value: true);
			Main.game.inv.RemoveItem(Items.stone4);
		}
		if (Main.game.inv.HaveItem(Items.stone5))
		{
			stones[5].ToShow();
			stonesOnLoc[4].SetActive(value: true);
			Main.game.inv.RemoveItem(Items.stone5);
		}
		canPlay = true;
		for (int i = 0; i < stones.Length; i++)
		{
			if (!stones[i].showed)
			{
				canPlay = false;
			}
		}
		isGame = true;
		board.SetActive(value: true);
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
		Main.hero.SetAn("Idle");
	}

	private void ShowBoard()
	{
	}

	private void RuntimeAction()
	{
		if (stoneGo)
		{
			waitTime += Time.deltaTime;
			if (waitTime > 0.8f)
			{
				hand1.SetActive(value: true);
				hand2.SetActive(value: false);
			}
			return;
		}
		board.transform.position = new Vector3(camT.position.x, camT.position.y, 0f);
		float x = Input.GetAxis("Horizontal") + Input.GetAxis("HorizontalJoy");
		float axis = Input.GetAxis("Vertical");
		Vector2 zero = Vector2.zero;
		zero.x = x;
		zero.y = axis;
		zero *= 0.075f;
		hand.transform.Translate(zero * 60f * Time.deltaTime);
		if (hand.transform.localPosition.x > 3.9f)
		{
			hand.transform.localPosition = new Vector2(3.9f, hand.transform.localPosition.y);
		}
		if (hand.transform.localPosition.x < -3.9f)
		{
			hand.transform.localPosition = new Vector2(-3.9f, hand.transform.localPosition.y);
		}
		if (hand.transform.localPosition.y > 3f)
		{
			hand.transform.localPosition = new Vector2(hand.transform.localPosition.x, 3f);
		}
		if (hand.transform.localPosition.y < -3.9f)
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			Main.hero.ReturnToGame();
			board.SetActive(value: false);
			isGame = false;
			if (takeKey)
			{
				Main.game.inv.AddItem(Items.key2);
			}
			return;
		}
		if (!takeKey && win && Input.GetButtonDown("Action") && Mathf.Abs(key.transform.localPosition.x - hand.transform.localPosition.x) < 1.6f && Mathf.Abs(key.transform.localPosition.y - hand.transform.localPosition.y) < 1f)
		{
			hand3.SetActive(value: true);
			hand4.SetActive(value: false);
			key.SetActive(value: false);
			Singleton<Sounds>.use.So("takeKeyBoard");
			takeKey = true;
		}
		if (!canPlay || win || !Input.GetButtonDown("Action"))
		{
			return;
		}
		for (int i = 0; i < stones.Length; i++)
		{
			stone = stones[i];
			if (Mathf.Abs(stone.transform.localPosition.x - hand.transform.localPosition.x) < 0.7f && Mathf.Abs(stone.transform.localPosition.y - hand.transform.localPosition.y) < 0.7f && !stone.goBack)
			{
				waitTime = 0f;
				stoneGo = true;
				hand2.SetActive(value: true);
				hand1.SetActive(value: false);
				stone.Go();
				Singleton<Sounds>.use.So("sort_so");
				break;
			}
		}
	}

	private void ShowKey()
	{
		myA += Time.deltaTime * 0.5f;
		keySp.color = new Color(1f, 1f, 1f, myA);
		if (myA >= 1f)
		{
			Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
			Singleton<ManagerFunctions>.use.removeFunction(ShowKey);
			hand4.SetActive(value: true);
			hand1.SetActive(value: false);
			hand2.SetActive(value: false);
			hand3.SetActive(value: false);
		}
	}

	public void CheckMove()
	{
		stoneGo = false;
		hand1.SetActive(value: true);
		hand2.SetActive(value: false);
		int num = 1;
		int pos = stones[0].pos;
		for (int i = 0; i < stones.Length; i++)
		{
			if (stones[i].pos > num)
			{
				num = stones[i].pos;
			}
			if (stones[i].pos < pos)
			{
				pos = stones[i].pos;
			}
		}
		int[] array = new int[6];
		for (int j = 0; j < 6; j++)
		{
			array[j] = 0;
		}
		for (int k = pos; k < num; k++)
		{
			array[k - 1] = 1;
		}
		for (int l = 0; l < array.Length; l++)
		{
			if (array[l] != 1)
			{
				continue;
			}
			bool flag = false;
			for (int m = 0; m < stones.Length; m++)
			{
				if (stones[m].pos == l + 1)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				Singleton<Sounds>.use.So("defeatBoardGame");
				Singleton<Sounds>.use.So("spirit_appear_zvuk");
				for (int n = 0; n < stones.Length; n++)
				{
					stones[n].SetBack();
				}
				return;
			}
		}
		for (int num2 = 0; num2 < stones.Length; num2++)
		{
			if (stones[num2].pos != 6)
			{
				return;
			}
		}
		win = true;
		Singleton<Sounds>.use.So("take_medal_zvuk");
		for (int num3 = 0; num3 < stones.Length; num3++)
		{
			stones[num3].SetWin();
		}
		hand.transform.localPosition = new Vector2(0f, -3.04f);
		key.SetActive(value: true);
		myA = 0f;
		keySp.color = new Color(1f, 1f, 1f, myA);
		Singleton<ManagerFunctions>.use.addFunction(ShowKey);
		Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
	}

	public void EndMove()
	{
		stoneGo = false;
		hand1.SetActive(value: true);
		hand2.SetActive(value: false);
	}
}
