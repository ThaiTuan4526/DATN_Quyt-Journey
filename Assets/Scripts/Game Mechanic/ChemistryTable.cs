using System.Collections.Generic;
using UnityEngine;

public class ChemistryTable : ActiveObj
{
	public GameObject board;

	public GameObject hand;

	public GameObject handChose;

	public GameObject handTaked;

	public GameObject handAdd;

	public GameObject handFire;

	public GameObject lever;

	public Animator addAn;

	public Animator cookAn;

	public Animator fireAn;

	public GameObject[] beakers;

	public GameObject[] beakersAtHand;

	public GameObject[] beakersAtHandAdd;

	public SpriteRenderer bowlerAquaSp;

	public BowlerChemistry bowlerChemistry;

	public ParticleSystem steamPS;

	private SpriteRenderer addSp;

	private SpriteRenderer cookSp;

	private int takeType;

	private bool taked;

	private bool leverMode;

	private int status;

	private bool isGame;

	private Transform camT;

	private Vector2 pos1;

	private Vector2 pos2;

	private float lerpF;

	private List<int> recipe = new List<int>();

	private List<Color> recipeColor = new List<Color>();

	private Color[] colors;

	private float savedmixR;

	private float savedmixG;

	private float savedmixB;

	private float mixR;

	private float mixG;

	private float mixB;

	private void Start()
	{
		board.SetActive(value: false);
		handChose.SetActive(value: true);
		handTaked.SetActive(value: false);
		handAdd.SetActive(value: false);
		handFire.SetActive(value: false);
		fireAn.gameObject.SetActive(value: false);
		bowlerAquaSp.gameObject.SetActive(value: false);
		cookAn.gameObject.SetActive(value: false);
		colors = new Color[5];
		colors[0] = new Color(0.59f, 0.6f, 0.317f);
		colors[1] = new Color(0.65f, 0.486f, 0.321f);
		colors[2] = new Color(0.513f, 0.39f, 0.537f);
		colors[3] = new Color(0.431f, 0.6f, 0.447f);
		colors[4] = new Color(0.65f, 0.39f, 0.32f);
		addSp = addAn.gameObject.GetComponent<SpriteRenderer>();
		cookSp = cookAn.gameObject.GetComponent<SpriteRenderer>();
		Init();
		steamPS.Stop();
		pos2 = new Vector2(-0.84f, 3.58f);
		taked = false;
		isGame = false;
		leverMode = false;
	}

	public override void DoAction()
	{
		Main.hero.goHero.GoTo(base.transform.position.x, this);
	}

	public override void DoActionOnPos()
	{
		camT = Main.game.cam.transform;
		board.transform.position = new Vector3(camT.position.x, camT.position.y, 0f);
		hand.transform.localPosition = new Vector2(-0.86f, -3.04f);
		handFire.transform.localEulerAngles = Vector3.zero;
		lever.transform.localEulerAngles = new Vector3(0f, 0f, 32.53f);
		fireAn.gameObject.SetActive(value: false);
		recipe.Clear();
		recipeColor.Clear();
		cookAn.Play("Idle", -1, 0f);
		cookAn.gameObject.SetActive(value: false);
		fireAn.gameObject.SetActive(value: false);
		Main.hero.SetAn("Idle");
		isGame = true;
		board.SetActive(value: true);
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
	}

	private void EndSteam()
	{
		lerpF += Time.deltaTime;
		if (lerpF >= 5f)
		{
			steamPS.Stop();
			Singleton<ManagerFunctions>.use.removeFunction(EndSteam);
		}
	}

	private void RuntimeAction()
	{
		board.transform.position = new Vector3(camT.position.x, camT.position.y, 0f);
		if (leverMode)
		{
			if (status == 1)
			{
				lerpF += Time.deltaTime * 1f;
				handFire.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(0f, -32f, lerpF));
				lever.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(32.53f, 0f, lerpF));
				if (lerpF >= 1f)
				{
					lerpF = 0f;
					status = 2;
					fireAn.gameObject.SetActive(value: true);
				}
			}
			else if (status == 2)
			{
				lerpF += Time.deltaTime * 1f;
				if (lerpF >= 2f)
				{
					cookAn.Play("Cook", -1, 0f);
					status = 3;
					Singleton<Sounds>.use.So("Swamp");
					Singleton<Sounds>.use.So("createMix");
				}
				if (lerpF >= 0.25f)
				{
					if (handFire.activeSelf)
					{
						Singleton<Sounds>.use.So("rotateMeat");
					}
					handFire.SetActive(value: false);
					handChose.SetActive(value: true);
					hand.transform.Translate(3.5f * Vector3.down * Time.deltaTime);
				}
			}
			else
			{
				if (status != 3 || !(cookAn.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f))
				{
					return;
				}
				bowlerChemistry.isBorn = false;
				bowlerChemistry.isPower = false;
				if (!bowlerAquaSp.gameObject.activeSelf)
				{
					if (recipe.Count == 3)
					{
						if (HaveType(0) && HaveType(3) && HaveType(4))
						{
							bowlerChemistry.isBorn = true;
						}
						if (HaveType(1) && HaveType(2) && HaveType(3))
						{
							bowlerChemistry.isPower = true;
						}
					}
					bowlerAquaSp.gameObject.SetActive(value: true);
					bowlerAquaSp.color = cookSp.color;
				}
				else
				{
					lerpF = 0f;
					bowlerAquaSp.gameObject.SetActive(value: false);
					steamPS.Play();
					Singleton<ManagerFunctions>.use.addFunction(EndSteam);
				}
				leverMode = false;
				Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
				Main.hero.ReturnToGame();
				board.SetActive(value: false);
				isGame = false;
			}
			return;
		}
		if (taked)
		{
			if (status == 1)
			{
				lerpF += Time.deltaTime * 1f;
				hand.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(0f, -32.87f, lerpF));
				hand.transform.localPosition = new Vector2(Mathf.Lerp(pos1.x, pos2.x, lerpF), Mathf.Lerp(pos1.y, pos2.y, lerpF));
				if (!(lerpF >= 1f))
				{
					return;
				}
				status = 2;
				handTaked.SetActive(value: false);
				handAdd.SetActive(value: true);
				Singleton<Sounds>.use.So("addMix");
				addAn.Play("Add", -1, 0f);
				addSp.color = colors[takeType];
				recipe.Add(takeType);
				recipeColor.Add(colors[takeType]);
				savedmixR = cookSp.color.r;
				savedmixG = cookSp.color.g;
				savedmixB = cookSp.color.b;
				mixR = 0f;
				mixG = 0f;
				mixB = 0f;
				foreach (Color item in recipeColor)
				{
					mixR += item.r;
					mixG += item.g;
					mixB += item.b;
				}
				mixR /= recipeColor.Count;
				mixG /= recipeColor.Count;
				mixB /= recipeColor.Count;
				if (recipeColor.Count > 1)
				{
					Color.RGBToHSV(new Color(mixR, mixG, mixB), out var H, out var S, out var V);
					S *= 2.5f;
					Color color = Color.HSVToRGB(H, S, V);
					mixR = color.r;
					mixG = color.g;
					mixB = color.b;
				}
			}
			else if (status == 2)
			{
				float normalizedTime = addAn.GetCurrentAnimatorStateInfo(0).normalizedTime;
				cookSp.color = new Color(Mathf.Lerp(savedmixR, mixR, normalizedTime), Mathf.Lerp(savedmixG, mixG, normalizedTime), Mathf.Lerp(savedmixB, mixB, normalizedTime));
				if (normalizedTime >= 1f)
				{
					status = 3;
					cookAn.gameObject.SetActive(value: true);
					handTaked.SetActive(value: true);
					handAdd.SetActive(value: false);
					lerpF = 0f;
				}
			}
			else if (status == 3)
			{
				lerpF += Time.deltaTime * 1f;
				hand.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(-32.87f, 0f, lerpF));
				hand.transform.localPosition = new Vector2(Mathf.Lerp(pos2.x, pos1.x, lerpF), Mathf.Lerp(pos2.y, pos1.y, lerpF));
				if (lerpF >= 1f)
				{
					status = 2;
					beakers[takeType].SetActive(value: true);
					handTaked.SetActive(value: false);
					handChose.SetActive(value: true);
					taked = false;
				}
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
		if (hand.transform.localPosition.x > 5.4f)
		{
			hand.transform.localPosition = new Vector2(5.4f, hand.transform.localPosition.y);
		}
		if (hand.transform.localPosition.x < -5.4f)
		{
			hand.transform.localPosition = new Vector2(-5.4f, hand.transform.localPosition.y);
		}
		if (hand.transform.localPosition.y > -1.57f)
		{
			hand.transform.localPosition = new Vector2(hand.transform.localPosition.x, -1.57f);
		}
		if (hand.transform.localPosition.y < -4.3f)
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			Main.hero.ReturnToGame();
			board.SetActive(value: false);
			isGame = false;
			return;
		}
		bool flag = false;
		SetDefPos();
		for (int i = 0; i < beakers.Length; i++)
		{
			if (Mathf.Abs(beakers[i].transform.localPosition.x - hand.transform.localPosition.x) < 0.25f && Mathf.Abs(beakers[i].transform.localPosition.y - hand.transform.localPosition.y) < 1.2f)
			{
				beakers[i].transform.localPosition = new Vector2(beakers[i].transform.localPosition.x, -0.97f);
				if (takeType != i)
				{
					Singleton<Sounds>.use.So2("choseMixBoatle", 0.5f);
				}
				flag = true;
				takeType = i;
				break;
			}
		}
		if (!flag)
		{
			takeType = -1;
		}
		if (!Input.GetButtonDown("Action"))
		{
			return;
		}
		if (Mathf.Abs(lever.transform.localPosition.x - hand.transform.localPosition.x) < 1f && Mathf.Abs(lever.transform.localPosition.y - hand.transform.localPosition.y) < 1.5f && recipe.Count > 0)
		{
			hand.transform.localPosition = new Vector2(0.31f, -2.74f);
			handChose.SetActive(value: false);
			handFire.SetActive(value: true);
			leverMode = true;
			status = 1;
			lerpF = 0f;
			Singleton<Sounds>.use.So("startFireCh");
		}
		else if (takeType >= 0)
		{
			Singleton<Sounds>.use.So("takeMix");
			taked = true;
			handChose.SetActive(value: false);
			handTaked.SetActive(value: true);
			GameObject[] array = beakersAtHand;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].SetActive(value: false);
			}
			array = beakersAtHandAdd;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].SetActive(value: false);
			}
			status = 1;
			beakers[takeType].SetActive(value: false);
			beakersAtHand[takeType].SetActive(value: true);
			beakersAtHandAdd[takeType].SetActive(value: true);
			lerpF = 0f;
			pos1 = hand.transform.localPosition;
		}
	}

	private bool HaveType(int n)
	{
		for (int i = 0; i < recipe.Count; i++)
		{
			if (recipe[i] == n)
			{
				return true;
			}
		}
		return false;
	}

	private void SetDefPos()
	{
		GameObject[] array = beakers;
		foreach (GameObject gameObject in array)
		{
			gameObject.transform.localPosition = new Vector2(gameObject.transform.localPosition.x, -1.35f);
		}
	}
}
