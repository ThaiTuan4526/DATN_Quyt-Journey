using UnityEngine;

public class DoorLibrary : ActiveObj
{
	[Header("Куда убегаем")]
	public Vector2 pos1;

	[Header("Где появляемся")]
	public Vector2 posExit;

	[Header("Конечная точка")]
	public Vector2 pos2;

	public bool toUp;

	public bool toLeft;

	public HeroFloor heroFloor;

	private float timeGo;

	private int status;

	private Vector2 posGo;

	private Vector2 posHero;

	private float difGo;

	private void Start()
	{
		Init();
	}

	public override void DoAction()
	{
		if (Main.hero.CanUse())
		{
			Main.hero.goHero.GoTo(base.transform.position.x, this);
			Main.hero.rb.velocity = Vector2.zero;
			Main.hero.rb.isKinematic = true;
			Main.hero.runAnyway = true;
		}
	}

	public override void DoActionOnPos()
	{
		heroFloor.goTo = true;
		timeGo = 0f;
		status = 1;
		Main.hero.stopActions = true;
		posGo = pos1;
		difGo = 1.5f;
		posHero = Main.hero.heroT.position;
		Main.hero.rb.isKinematic = true;
		Main.hero.SetDirect(!toLeft);
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
			if (!(timeGo >= 1f))
			{
				return;
			}
			timeGo = 0f;
			status = 2;
			if (Main.location == 7)
			{
				if (toUp)
				{
					if (heroFloor.floor == 1)
					{
						Main.game.cam.GoToPos(new Vector3(Main.game.cam.transform.position.x, 5.57f, -10f), 0.02f);
					}
					else if (heroFloor.floor == 2)
					{
						Main.game.cam.GoToPos(new Vector3(Main.game.cam.transform.position.x, 11.45f, -10f), 0.02f);
					}
					else
					{
						Main.game.cam.GoToPos(new Vector3(Main.game.cam.transform.position.x, 12.65f, -10f), 0.02f);
					}
				}
				else if (heroFloor.floor == 2)
				{
					Main.game.cam.GoToPos(new Vector3(Main.game.cam.transform.position.x, -0.68f, -10f), 0.02f);
				}
				else if (heroFloor.floor == 3)
				{
					Main.game.cam.GoToPos(new Vector3(Main.game.cam.transform.position.x, 5.57f, -10f), 0.02f);
				}
				else
				{
					Main.game.cam.GoToPos(new Vector3(Main.game.cam.transform.position.x, 11.45f, -10f), 0.02f);
				}
			}
			else if (toUp)
			{
				if (heroFloor.floor == 2)
				{
					Main.game.cam.GoToPos(new Vector3(Main.game.cam.transform.position.x, 4.82f, -10f), 0.02f);
				}
				else if (heroFloor.floor == 1)
				{
					Main.game.cam.GoToPos(new Vector3(Main.game.cam.transform.position.x, 2.36f, -10f), 0.02f);
				}
			}
			else if (heroFloor.floor == 3)
			{
				Main.game.cam.GoToPos(new Vector3(Main.game.cam.transform.position.x, 2.36f, -10f), 0.02f);
			}
			else if (heroFloor.floor == 2)
			{
				Main.game.cam.GoToPos(new Vector3(Main.game.cam.transform.position.x, -3.66f, -10f), 0.02f);
			}
			Main.hero.heroT.position = posExit;
			Main.hero.gameObject.SetActive(value: false);
		}
		else if (status == 2)
		{
			timeGo += Time.deltaTime;
			if (timeGo > 0.2f)
			{
				status = 3;
				Main.hero.SetDirect(toLeft);
				Main.hero.gameObject.SetActive(value: true);
				Main.hero.SetDirectAn(Main.hero.runAn);
				timeGo = 0f;
				posGo = pos2;
				difGo = 1f;
				posHero = Main.hero.heroT.position;
			}
		}
		else
		{
			if (status != 3)
			{
				return;
			}
			Main.hero.heroT.position = Vector2.Lerp(posHero, posGo, timeGo);
			timeGo += Time.deltaTime * difGo;
			if (timeGo >= 1f)
			{
				if (toUp)
				{
					heroFloor.floor++;
				}
				else
				{
					heroFloor.floor--;
				}
				heroFloor.goTo = false;
				Main.hero.sp.maskInteraction = SpriteMaskInteraction.None;
				Main.hero.ReturnToGame();
				Main.game.cam.isFollow = true;
				Main.game.cam.followY = false;
				Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			}
		}
	}
}
