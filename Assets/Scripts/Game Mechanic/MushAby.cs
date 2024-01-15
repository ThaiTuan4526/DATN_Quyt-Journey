using UnityEngine;
using UnityEngine.UI;

public class MushAby : MonoBehaviour, InterfaceBreakObj, GameObjInterface
{
	public MonsterUnder monster;

	private GameObject mushPlace;

	private bool actionDone;

	private GameObject abyUI;

	private Image icon;

	private void Start()
	{
		Invoke("Init", 0.1f);
	}

	private void Init()
	{
		Main.game.arrBreakObj.Add(this);
		abyUI = Object.Instantiate(Resources.Load("Prefabs/UI/AbyMushUI"), Main.game.canvasT, instantiateInWorldSpace: false) as GameObject;
		abyUI.transform.localPosition = new Vector3(Main.lCornerX, Main.downCornerY);
		icon = abyUI.transform.GetChild(1).gameObject.GetComponent<Image>();
		Singleton<InputChecker>.use.AddObj2(this);
		BreakObj();
	}

	public void SetSprite(Sprite n)
	{
		icon.sprite = n;
	}

	public void BreakObj()
	{
		base.gameObject.SetActive(value: false);
		abyUI.SetActive(value: false);
	}

	public void SetEnable(GameObject n)
	{
		base.gameObject.SetActive(value: true);
		abyUI.SetActive(value: true);
		mushPlace = n;
	}

	private void Update()
	{
		if (Input.GetButtonDown("Aby") && (monster.isSleep || monster.dead) && !Main.hero.stopActions && Main.hero.onPlatform && !Main.hero.catchMode && !Main.hero.jump && !Main.hero.fall)
		{
			Main.hero.stopActions = true;
			Singleton<ManagerFunctions>.use.addFunction(PlaceMush);
			if (Main.hero.transform.position.x > 52f)
			{
				Main.hero.SetDirect(n: false);
			}
			if (Main.hero.transform.position.x < 16f)
			{
				Main.hero.SetDirect(n: true);
			}
			Main.hero.SetAn("Take");
			actionDone = false;
		}
	}

	private void PlaceMush()
	{
		if (!actionDone && Main.hero.AnFrame(0.4f))
		{
			actionDone = true;
			float x = Main.hero.transform.position.x;
			if (Main.hero.direct)
			{
				x += 0.65f;
				if (Main.hero.transform.position.x < 16f)
				{
					x += 0.4f;
				}
			}
			else
			{
				x -= 0.65f;
				if (Main.hero.transform.position.x > 53f)
				{
					x -= 0.4f;
				}
			}
			Singleton<Sounds>.use.So("takeMush");
			mushPlace.SetActive(value: true);
			mushPlace.transform.position = new Vector3(x, Main.hero.transform.position.y, 0f);
			base.gameObject.SetActive(value: false);
			abyUI.SetActive(value: false);
		}
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(PlaceMush);
			Main.hero.ReturnToGame();
		}
	}

	public virtual void DoAction()
	{
	}

	public virtual void DoActionOnPos()
	{
	}
}
