using UnityEngine;

public class Altar : ActiveObj
{
	private bool actionDone;

	private bool actionDone2;

	public GameObject rabbit1;

	public GameObject symbols;

	public GameObject soundAmbience;

	public GameObject rune0;

	public GameObject rune;

	public GameObject viewObj;

	public TreeDoor door;

	public Animator lightBoltAn;

	private bool placedMeat;

	private bool needle;

	private bool ritual;

	private bool runeSet;

	private float timeWait;

	private int status;

	private void Start()
	{
		Init();
		rabbit1.SetActive(value: false);
		symbols.SetActive(value: false);
		rune.SetActive(value: false);
	}

	public override void DoAction()
	{
		if (Main.game.inv.HaveItem(Items.rune))
		{
			Main.hero.goHero.GoTo(base.transform.position.x, this);
		}
		else if (runeSet)
		{
			if (Main.game.inv.HaveItem(Items.needle))
			{
				Main.hero.goHero.GoTo(base.transform.position.x, this);
			}
			else if (Main.game.inv.HaveItem(Items.meat))
			{
				Main.hero.goHero.GoTo(base.transform.position.x, this);
			}
		}
	}

	public override void DoActionOnPos()
	{
		if (Main.game.inv.HaveItem(Items.rune))
		{
			Main.hero.SetDirect(n: true);
			Main.game.inv.RemoveItem(Items.rune);
			Singleton<Sounds>.use.So("insertRune");
			Main.hero.ReturnToGame();
			runeSet = true;
			rune.SetActive(value: true);
			rune0.SetActive(value: false);
			return;
		}
		if (Main.game.inv.HaveItem(Items.needle))
		{
			GetComponent<BoxCollider2D>().enabled = false;
			Main.game.cam.BreakFollow();
			Main.game.cam.cameraT.position = new Vector3(53.44f, 0f, -10f);
			Main.game.cam.SetSize(2f);
			Main.hero.SetDirect(n: true);
			Main.hero.SetAn("Needle");
			actionDone = false;
			actionDone2 = false;
			Singleton<ManagerFunctions>.use.addFunction(Needle);
			return;
		}
		Main.hero.SetDirect(n: true);
		Main.hero.SetAn("Watch");
		rabbit1.SetActive(value: true);
		Main.game.inv.RemoveItem(Items.meat);
		placedMeat = true;
		if (needle)
		{
			StartRitual();
		}
		else
		{
			door.FailRunes();
			Singleton<Sounds>.use.So("placeMeat");
		}
		if (!ritual)
		{
			Main.hero.ReturnToGame();
		}
	}

	private void StartRitual()
	{
		viewObj.SetActive(value: false);
		GetComponent<BoxCollider2D>().enabled = false;
		status = 1;
		timeWait = 0f;
		Singleton<ManagerFunctions>.use.addFunction(Ritual);
		ritual = true;
		Singleton<Sounds>.use.So("sacrifice");
		soundAmbience.SetActive(value: false);
		door.ShowRunes();
	}

	private void Ritual()
	{
		if (status == 1)
		{
			timeWait += Time.deltaTime;
			if (timeWait > 1.5f)
			{
				Singleton<Sounds>.use.So2("thunder", 1.5f);
				status = 2;
				timeWait = 0f;
			}
		}
		else if (status == 2)
		{
			timeWait += Time.deltaTime;
			if (timeWait > 1.1f)
			{
				rabbit1.SetActive(value: false);
				door.Go();
				lightBoltAn.Play("Go");
				Main.hero.SetAn("FallAss");
				Singleton<ManagerFunctions>.use.removeFunction(Ritual);
			}
		}
	}

	private void Needle()
	{
		if (!actionDone && Main.hero.AnFrame(0.26f))
		{
			actionDone = true;
			Main.game.inv.RemoveItem(Items.needle);
			Singleton<Sounds>.use.So("needleUse");
		}
		if (!actionDone2 && Main.hero.AnFrame(0.8f))
		{
			actionDone2 = true;
			symbols.SetActive(value: true);
			Singleton<Sounds>.use.So2("needleEffect", 0.5f);
		}
		if (Main.hero.AnCompleted())
		{
			needle = true;
			Main.game.cam.SetDefaultSize();
			Main.game.cam.cameraT.position = new Vector3(62f, 1.364668f, -10f);
			Main.game.cam.ToFollow(Main.hero.heroT);
			Singleton<ManagerFunctions>.use.removeFunction(Needle);
			GetComponent<BoxCollider2D>().enabled = true;
			if (placedMeat)
			{
				StartRitual();
			}
			if (!ritual)
			{
				Main.hero.ReturnToGame();
			}
		}
	}
}
