using UnityEngine;

public class SwitchFloor : ActiveObj, InterfaceBreakObj
{
	private float timeGo;

	public Witch witch;

	public GameObject sun1;

	public GameObject sun2;

	public bool isKitchen;

	public int status;

	private Vector2 pos1;

	private Vector2 pos2;

	private Vector2 pos3;

	private Vector2 pos4;

	private Vector2 posGo;

	private Vector2 posHero;

	private float difGo;

	private void Start()
	{
		Init();
		pos1 = new Vector2(7.033f, 1.204f);
		pos2 = new Vector2(4.564f, -3f);
		pos3 = new Vector2(5.42f, -4.77f);
		pos4 = new Vector2(7.315f, 0.49f);
		Main.game.arrBreakObj.Add(this);
		BreakObj();
	}

	public void BreakObj()
	{
		sun1.SetActive(value: true);
		sun2.SetActive(value: false);
	}

	public override void DoAction()
	{
		if (!witch.attackMode)
		{
			Main.hero.goHero.GoTo(base.transform.position.x, this);
		}
	}

	public override void DoActionOnPos()
	{
		timeGo = 0f;
		status = 1;
		Main.hero.stopActions = true;
		if (isKitchen)
		{
			posGo = pos1;
			difGo = 2f;
		}
		else
		{
			posGo = pos2;
			difGo = 1f;
		}
		posHero = Main.hero.heroT.position;
		Main.hero.rb.isKinematic = true;
		Main.hero.SetDirect(n: false);
		Main.hero.SetAn(Main.hero.runAn);
		Main.hero.sp.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
	}

	private void RuntimeAction()
	{
		if (status == 1)
		{
			Main.hero.heroT.position = Vector2.Lerp(posHero, posGo, timeGo);
			timeGo += Time.deltaTime * difGo;
			if (timeGo >= 1f)
			{
				timeGo = 0f;
				status = 2;
				Main.hero.gameObject.SetActive(value: false);
			}
		}
		else if (status == 2)
		{
			timeGo += Time.deltaTime;
			if (timeGo > 1f)
			{
				status = 3;
				sun1.SetActive(!isKitchen);
				sun2.SetActive(isKitchen);
				Main.hero.SetDirect(n: true);
				Main.hero.gameObject.SetActive(value: true);
				Main.hero.SetDirectAn(Main.hero.runAn);
				timeGo = 0f;
				if (isKitchen)
				{
					Main.hero.heroT.position = new Vector2(4.564f, -3f);
					posGo = pos3;
					difGo = 1f;
				}
				else
				{
					Main.hero.heroT.position = new Vector2(6.9f, 1.27f);
					posGo = pos4;
					difGo = 2.5f;
				}
				posHero = Main.hero.heroT.position;
			}
		}
		else if (status == 3)
		{
			Main.hero.heroT.position = Vector2.Lerp(posHero, posGo, timeGo);
			timeGo += Time.deltaTime * difGo;
			if (timeGo >= 1f)
			{
				Main.hero.sp.maskInteraction = SpriteMaskInteraction.None;
				Main.hero.ReturnToGame();
				Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			}
		}
	}
}
