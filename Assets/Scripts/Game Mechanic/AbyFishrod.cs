using UnityEngine;
using UnityEngine.UI;

public class AbyFishrod : MonoBehaviour, GameObjInterface
{
	public Transform posLever;

	public Transform posLadder;

	public Animator leverAn;

	public Animator ladderAn;

	public StonePlat plat;

	public GameObject crackedFishrog;

	public GameObject ladderObj;

	public Fish fish;

	private bool actionDone;

	private GameObject abyUI;

	private Image icon;

	private float playTime;

	private bool myselfCancel;

	private bool toLever;

	private bool levelEnd;

	private void Start()
	{
		Invoke("Init", 0.1f);
	}

	private void Init()
	{
		crackedFishrog.SetActive(value: false);
		ladderObj.SetActive(value: false);
		ladderAn.keepAnimatorControllerStateOnDisable = true;
		leverAn.keepAnimatorControllerStateOnDisable = true;
		abyUI = Object.Instantiate(Resources.Load("Prefabs/UI/AbyFishUI"), Main.game.canvasT, instantiateInWorldSpace: false) as GameObject;
		abyUI.transform.localPosition = new Vector3(Main.lCornerX, Main.downCornerY);
		icon = abyUI.transform.GetChild(1).gameObject.GetComponent<Image>();
		Singleton<InputChecker>.use.AddObj2(this);
		plat.SetHide();
		SetDisable();
		Main.game.objects.Add(base.gameObject);
	}

	public void SetSprite(Sprite n)
	{
		icon.sprite = n;
	}

	public void SetDisable()
	{
		base.gameObject.SetActive(value: false);
		abyUI.SetActive(value: false);
	}

	public void SetEnable()
	{
		base.gameObject.SetActive(value: true);
		abyUI.SetActive(value: true);
	}

	private void Update()
	{
		if (!Input.GetButtonDown("Aby") || Main.hero.stopActions || !Main.hero.onPlatform || Main.hero.catchMode || Main.hero.jump || Main.hero.fall)
		{
			return;
		}
		if (Main.game.currentSceneName == "library")
		{
			Main.hero.SetSomeAn("NoActions");
		}
		else if (Main.game.currentSceneName == "castle")
		{
			if (Main.hero.heroT.position.x > 4f)
			{
				Main.hero.stopActions = true;
				playTime = 0f;
				Main.hero.SetAn("Fishing");
				actionDone = false;
				Singleton<Sounds>.use.So("fishing");
				if (Main.hero.heroT.position.x > 49f)
				{
					fish.stopFish = true;
					Singleton<ManagerFunctions>.use.addFunction(FishingPrey);
				}
				else
				{
					Singleton<ManagerFunctions>.use.addFunction(FishingNothing);
				}
			}
			else
			{
				Main.hero.SetSomeAn("NoActions");
			}
		}
		else if (Main.game.currentSceneName == "chapel")
		{
			if (!levelEnd && Mathf.Abs(posLever.position.x - Main.hero.heroT.position.x) < 3f && Mathf.Abs(posLever.position.y - Main.hero.heroT.position.y) < 1f)
			{
				actionDone = false;
				toLever = true;
				Main.hero.goHero.GoTo(posLever.position.x, this);
			}
			else if (Mathf.Abs(posLadder.position.x - Main.hero.heroT.position.x) < 3f && Mathf.Abs(posLadder.position.y - Main.hero.heroT.position.y) < 1f)
			{
				actionDone = false;
				toLever = false;
				Main.hero.goHero.GoTo(posLadder.position.x, this);
			}
			else
			{
				Main.hero.SetSomeAn("NoActions");
			}
		}
	}

	private void FishingPrey()
	{
		if (!actionDone)
		{
			if (Input.GetButtonDown("Aby") || Input.GetButtonDown("Action"))
			{
				playTime = 10f;
			}
			playTime += Time.deltaTime;
			if (playTime > 2f)
			{
				playTime = 3f;
				Main.hero.SetAn("FishingPrey");
				actionDone = true;
				return;
			}
		}
		if (!actionDone)
		{
			return;
		}
		if (playTime == 3f && Main.hero.AnFrame(0.25f))
		{
			Singleton<Sounds>.use.So2("upFish", 0.5f);
			playTime = 0f;
		}
		if (playTime == 0f && Main.hero.AnFrame(0.42f))
		{
			Singleton<Sounds>.use.So("moveFishdog");
			playTime = 1f;
		}
		if (playTime == 1f && Main.hero.AnFrame(0.85f))
		{
			Singleton<Sounds>.use.So2("placeMeat", 0.5f);
			playTime = 2f;
		}
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(FishingPrey);
			Main.hero.ReturnToGame();
			if (!Main.game.inv.HaveItem(Items.fish))
			{
				Main.game.inv.AddItem(Items.fish);
			}
		}
	}

	private void FishingNothing()
	{
		if (!actionDone)
		{
			if (Input.GetButtonDown("Aby") || Input.GetButtonDown("Action"))
			{
				Singleton<Sounds>.use.So("endFishing");
				Main.hero.SetAn("FishingEnd");
				actionDone = true;
				myselfCancel = true;
				return;
			}
			playTime += Time.deltaTime;
			if (playTime > 10f)
			{
				Singleton<Sounds>.use.So("endFishing");
				Main.hero.SetAn("FishingEnd");
				actionDone = true;
				myselfCancel = false;
				Singleton<SteamF>.use.AddAch(13);
				return;
			}
		}
		if (actionDone && Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(FishingNothing);
			Main.hero.ReturnToGame();
			if (!myselfCancel)
			{
				Main.hero.SetSomeAn("NoActions");
			}
		}
	}

	private void CatchLever()
	{
		if (playTime == 3f && Main.hero.AnFrame(0.05f))
		{
			Singleton<Sounds>.use.So("throwHero");
			playTime = 0f;
		}
		if (playTime == 0f && Main.hero.AnFrame(0.23f))
		{
			Singleton<Sounds>.use.So("lad1");
			playTime = 1f;
		}
		if (playTime == 1f && Main.hero.AnFrame(0.67f))
		{
			playTime = 2f;
		}
		if (!actionDone && Main.hero.AnFrame(0.44f))
		{
			leverAn.Play("Go");
			plat.ToShow();
			levelEnd = true;
			actionDone = true;
			Singleton<Sounds>.use.So("stoneLever");
		}
		if (actionDone && Main.hero.AnCompleted())
		{
			Main.hero.ReturnToGame();
			Singleton<ManagerFunctions>.use.removeFunction(CatchLever);
		}
	}

	private void CatchLadder()
	{
		if (playTime == 0f && Main.hero.AnFrame(0.1f))
		{
			Singleton<Sounds>.use.So("throwHero");
			playTime = 1f;
		}
		if (!actionDone && Main.hero.AnFrame(0.67f))
		{
			ladderAn.Play("Go");
			actionDone = true;
		}
		if (actionDone && Main.hero.AnCompleted())
		{
			crackedFishrog.SetActive(value: true);
			ladderObj.SetActive(value: true);
			SetDisable();
			Main.hero.ReturnToGame();
			Singleton<ManagerFunctions>.use.removeFunction(CatchLadder);
		}
	}

	public virtual void DoAction()
	{
	}

	public virtual void DoActionOnPos()
	{
		Main.hero.SetDirect(n: false);
		if (toLever)
		{
			playTime = 3f;
			Main.hero.SetAn("FishingLever");
			Singleton<ManagerFunctions>.use.addFunction(CatchLever);
		}
		else
		{
			playTime = 0f;
			Main.hero.SetAn("FishingLadder");
			Singleton<ManagerFunctions>.use.addFunction(CatchLadder);
		}
	}
}
