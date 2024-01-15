using UnityEngine;

public class Door4 : ActiveObj
{
	public GameObject d1;

	public GameObject d2;

	private bool toOpen;

	private bool go;

	public MonsterCamp monsterCamp;

	private void Start()
	{
		Init();
		d2.SetActive(value: false);
		d1.SetActive(value: true);
	}

	public override void DoAction()
	{
		if (Main.hero.CanUse())
		{
			Main.hero.goHero.GoTo(base.transform.position.x, this);
		}
	}

	public override void DoActionOnPos()
	{
		Main.hero.SetDirect(n: false);
		Main.hero.SetAn("OpenDoor");
		Singleton<ManagerFunctions>.use.addFunction(OpenDoor);
		toOpen = true;
		go = false;
	}

	public void CloseDoor()
	{
		d2.SetActive(value: false);
		d1.SetActive(value: true);
	}

	private void OpenDoor()
	{
		if (go)
		{
			Main.hero.heroT.position = new Vector2(Main.hero.heroT.position.x - 3f * Time.deltaTime, Main.hero.heroT.position.y + 0.59999996f * Time.deltaTime);
			if (Main.hero.heroT.position.x < 9.6f)
			{
				Singleton<ManagerFunctions>.use.removeFunction(OpenDoor);
				Main.hero.ReturnToGame();
				Main.hero.heroT.position = new Vector3(5.67f, -3.607383f, 0f);
				Main.hero.SetDirect(n: false);
				Main.hero.SetStepsNames(2);
				Main.game.ShowScene("camphouse");
				Singleton<Music>.use.MusicStart("houseBones");
				Main.game.cam.BreakFollow();
				Main.game.cam.cameraT.position = new Vector3(0f, 0f, -10f);
				monsterCamp.SetGoUp();
				Main.hero.sp.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
			}
		}
		else if (toOpen)
		{
			if (Main.hero.AnCompleted())
			{
				d1.SetActive(value: false);
				d2.SetActive(value: true);
				Main.hero.SetAn("OpenDoor2");
				toOpen = false;
				Singleton<Sounds>.use.So2("door_open_01", 0.5f);
			}
		}
		else if (Main.hero.AnCompleted())
		{
			go = true;
			Main.hero.rb.isKinematic = true;
			Main.hero.SetAn("Run");
		}
	}
}
