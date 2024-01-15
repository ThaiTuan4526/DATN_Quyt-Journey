using UnityEngine;

public class ButterflyGame : ActiveObj
{
	public GameObject board;

	public GameObject hand;

	public GameObject abacus1;

	public GameObject abacus2;

	public DoorButterfly door;

	public Transform[] stones1;

	public Transform[] stones2;

	public SpriteRenderer[] sp;

	private bool moveMode;

	private int status;

	private Vector2[] defPos1;

	private Vector2[] defPos2;

	private Vector2[] defPos1_1;

	private Vector2[] defPos2_2;

	private int stepUp;

	private int stepDown;

	private int stepGame;

	private bool upperMove;

	private Vector2 pos1;

	private Vector2 pos2;

	private float lerpF;

	private Transform camT;

	private bool showMode;

	private bool defeatMode;

	private void Start()
	{
		board.SetActive(value: false);
		abacus1.SetActive(value: true);
		abacus2.SetActive(value: false);
		for (int i = 0; i < sp.Length; i++)
		{
			sp[i].color = new Color(1f, 1f, 1f, 0f);
		}
		defPos1 = new Vector2[stones1.Length];
		defPos1_1 = new Vector2[stones1.Length];
		defPos2 = new Vector2[stones2.Length];
		defPos2_2 = new Vector2[stones2.Length];
		for (int j = 0; j < stones1.Length; j++)
		{
			defPos1[j] = stones1[j].localPosition;
		}
		for (int k = 0; k < stones2.Length; k++)
		{
			defPos2[k] = stones2[k].localPosition;
		}
		Init();
		stepGame = 0;
		stepUp = 0;
		stepDown = 0;
	}

	public override void DoAction()
	{
		if (stepGame < 3)
		{
			Main.hero.goHero.GoTo(base.transform.position.x, this);
		}
	}

	public override void DoActionOnPos()
	{
		camT = Main.game.cam.transform;
		board.transform.position = new Vector3(camT.position.x, camT.position.y, 0f);
		hand.transform.localPosition = new Vector2(0f, -3.43f);
		moveMode = false;
		board.SetActive(value: true);
		Main.hero.SetAn("Idle");
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
	}

	private void SetDefeat()
	{
		Singleton<Sounds>.use.So("defeatBut");
		defeatMode = true;
		stepDown = 0;
		stepUp = 0;
		stepGame = 0;
		lerpF = 0f;
		for (int i = 0; i < stones1.Length; i++)
		{
			defPos1_1[i] = stones1[i].localPosition;
		}
		for (int j = 0; j < stones2.Length; j++)
		{
			defPos2_2[j] = stones2[j].localPosition;
		}
	}

	private void RuntimeAction()
	{
		board.transform.position = new Vector3(camT.position.x, camT.position.y, 0f);
		if (moveMode)
		{
			if (status == 1)
			{
				lerpF += Time.deltaTime * 7f;
				hand.transform.localPosition = new Vector2(Mathf.Lerp(pos1.x, pos2.x, lerpF), Mathf.Lerp(pos1.y, pos2.y, lerpF));
				if (lerpF >= 1f)
				{
					status = 2;
					lerpF = 0f;
					pos1 = hand.transform.localPosition;
					pos2 = hand.transform.localPosition;
					Singleton<Sounds>.use.So("stoneGo");
					if (upperMove)
					{
						pos2.x += 2.3f;
					}
					else
					{
						pos2.x += 3.3f;
					}
				}
			}
			else
			{
				if (status != 2)
				{
					return;
				}
				lerpF += Time.deltaTime * 5f;
				hand.transform.localPosition = new Vector2(Mathf.Lerp(pos1.x, pos2.x, lerpF), Mathf.Lerp(pos1.y, pos2.y, lerpF));
				if (upperMove)
				{
					stones1[stepUp].localPosition = hand.transform.localPosition;
				}
				else
				{
					stones2[stepDown].localPosition = hand.transform.localPosition;
				}
				if (!(lerpF >= 1f))
				{
					return;
				}
				if (upperMove)
				{
					stepUp++;
				}
				else
				{
					stepDown++;
				}
				moveMode = false;
				if (!upperMove && ((stepGame == 0 && stepUp == 3 && stepDown == 1) || ((stepGame == 1 && stepUp == 2 && stepDown == 2) | (stepGame == 2 && stepUp == 4 && stepDown == 3))))
				{
					showMode = true;
					lerpF = 0f;
					stepUp = 0;
					status = 1;
					for (int i = 0; i < stones1.Length; i++)
					{
						defPos1_1[i] = stones1[i].localPosition;
					}
					Singleton<Sounds>.use.So("butterflyGame");
				}
				else if (upperMove)
				{
					if (stepGame == 0)
					{
						if (stepUp >= 4)
						{
							SetDefeat();
						}
					}
					else if (stepGame == 1)
					{
						if (stepUp >= 3)
						{
							SetDefeat();
						}
					}
					else if (stepGame == 2 && stepUp >= 5)
					{
						SetDefeat();
					}
				}
				else
				{
					SetDefeat();
				}
			}
			return;
		}
		if (showMode)
		{
			if (status == 1)
			{
				lerpF += Time.deltaTime * 1f;
				sp[stepGame].color = new Color(1f, 1f, 1f, lerpF);
				if (stepGame < 2)
				{
					for (int j = 0; j < stones1.Length; j++)
					{
						stones1[j].localPosition = new Vector2(Mathf.Lerp(defPos1_1[j].x, defPos1[j].x, lerpF), Mathf.Lerp(defPos1_1[j].y, defPos1[j].y, lerpF));
					}
				}
				if (lerpF >= 1f)
				{
					if (++stepGame >= 3)
					{
						status = 2;
						lerpF = 0f;
					}
					else
					{
						showMode = false;
					}
				}
			}
			else if (status == 2)
			{
				lerpF += Time.deltaTime * 1f;
				sp[stepGame].color = new Color(1f, 1f, 1f, lerpF);
				if (lerpF >= 1f)
				{
					door.ToOpen();
					Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
					Main.hero.ReturnToGame();
					board.SetActive(value: false);
					abacus2.SetActive(value: true);
					abacus1.SetActive(value: false);
					Singleton<Sounds>.use.So("openDoorB");
				}
			}
			return;
		}
		if (defeatMode)
		{
			lerpF += Time.deltaTime * 8f;
			for (int k = 0; k < stones1.Length; k++)
			{
				stones1[k].localPosition = new Vector2(Mathf.Lerp(defPos1_1[k].x, defPos1[k].x, lerpF), Mathf.Lerp(defPos1_1[k].y, defPos1[k].y, lerpF));
			}
			for (int l = 0; l < stones2.Length; l++)
			{
				stones2[l].localPosition = new Vector2(Mathf.Lerp(defPos2_2[l].x, defPos2[l].x, lerpF), Mathf.Lerp(defPos2_2[l].y, defPos2[l].y, lerpF));
			}
			for (int m = 0; m < sp.Length; m++)
			{
				if (sp[m].color.a > 0f)
				{
					sp[m].color = new Color(1f, 1f, 1f, 1f - lerpF);
				}
			}
			if (lerpF >= 1f)
			{
				defeatMode = false;
			}
			return;
		}
		float x = Input.GetAxis("Horizontal") + Input.GetAxis("HorizontalJoy");
		float axis = Input.GetAxis("Vertical");
		Vector2 zero = Vector2.zero;
		zero.x = x;
		zero.y = axis;
		zero *= 0.075f;
		hand.transform.Translate(zero * 60f * Time.deltaTime);
		if (hand.transform.localPosition.x > 4.4f)
		{
			hand.transform.localPosition = new Vector2(4.4f, hand.transform.localPosition.y);
		}
		if (hand.transform.localPosition.x < -4.4f)
		{
			hand.transform.localPosition = new Vector2(-4.4f, hand.transform.localPosition.y);
		}
		if (hand.transform.localPosition.y > 1.5f)
		{
			hand.transform.localPosition = new Vector2(hand.transform.localPosition.x, 1.5f);
		}
		if (hand.transform.localPosition.y < -4.3f)
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			Main.hero.ReturnToGame();
			board.SetActive(value: false);
		}
		else if (Input.GetButtonDown("Action"))
		{
			if (Mathf.Abs(-0.5f - hand.transform.localPosition.y) < 0.75f)
			{
				status = 1;
				lerpF = 0f;
				pos1 = hand.transform.localPosition;
				pos2 = stones1[stepUp].localPosition;
				upperMove = true;
				moveMode = true;
			}
			else if (Mathf.Abs(-2.7f - hand.transform.localPosition.y) < 0.75f)
			{
				status = 1;
				lerpF = 0f;
				pos1 = hand.transform.localPosition;
				pos2 = stones2[stepDown].localPosition;
				upperMove = false;
				moveMode = true;
			}
		}
	}
}
