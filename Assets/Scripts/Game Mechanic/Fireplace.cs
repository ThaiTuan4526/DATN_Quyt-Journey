using UnityEngine;

public class Fireplace : ActiveObj
{
	public GameObject lightObj;

	public GameObject lightFiery;

	public GameObject bowler;

	public GameObject bowler2;

	public GameObject bowler3;

	public GameObject bowler4;

	public GameObject sunFire;

	public bool fireMade;

	public bool bowlerAdded;

	public bool potionCreated;

	private bool kick;

	private int rowKicks;

	private float delayTime;

	private bool actionDone;

	public int typeMushWitch;

	private int stepRow;

	private int mushAdded;

	private void Start()
	{
		Init();
		bowler.SetActive(value: false);
		bowler2.SetActive(value: false);
		bowler3.SetActive(value: false);
		bowler4.SetActive(value: false);
		lightObj.SetActive(value: false);
		lightFiery.SetActive(value: false);
		sunFire.SetActive(value: false);
		stepRow = 0;
		mushAdded = 0;
	}

	public override void DoAction()
	{
		if ((Main.game.inv.HaveItem(Items.bowler) && fireMade) || (Main.game.inv.HaveItem(Items.fiery) && !fireMade))
		{
			Main.hero.goHero.GoTo(base.transform.position.x, this);
		}
		else if (Main.game.inv.HaveItem(Items.mushwitch) && bowlerAdded)
		{
			Main.hero.goHero.GoTo(-1.6f, this);
		}
		else if (Main.game.inv.HaveItem(Items.jar) && potionCreated)
		{
			Main.hero.goHero.GoTo(-1.467f, this);
		}
	}

	public override void DoActionOnPos()
	{
		Main.hero.SetDirect(n: false);
		actionDone = false;
		if (Main.game.inv.HaveItem(Items.bowler) && fireMade)
		{
			Main.game.inv.RemoveItem(Items.bowler);
			Main.hero.ReturnToGame();
			bowlerAdded = true;
			bowler.SetActive(value: true);
			Singleton<Sounds>.use.So("setBowler");
		}
		else if (Main.game.inv.HaveItem(Items.fiery) && !fireMade)
		{
			Singleton<ManagerFunctions>.use.addFunction(Fiery);
			Main.hero.SetDirect(n: false);
			Main.hero.SetAn("Kneel");
			actionDone = false;
		}
		else if (Main.game.inv.HaveItem(Items.mushwitch) && bowlerAdded)
		{
			actionDone = false;
			Singleton<ManagerFunctions>.use.addFunction(AddMush);
			Main.hero.SetDirect(n: false);
			Main.hero.SetAn("PutMushBowler");
		}
		else if (potionCreated)
		{
			actionDone = false;
			Singleton<ManagerFunctions>.use.addFunction(FillJar);
			Main.hero.SetDirect(n: false);
			Main.hero.SetAn("Bottle");
		}
	}

	private void AddMush()
	{
		if (!actionDone && Main.hero.AnFrame(0.5f))
		{
			actionDone = true;
			Singleton<Sounds>.use.So("addMushToBowler");
			if (++mushAdded == 10)
			{
				Singleton<SteamF>.use.AddAch(7);
			}
		}
		if (!Main.hero.AnCompleted())
		{
			return;
		}
		bowler.SetActive(value: false);
		bowler2.SetActive(value: false);
		bowler3.SetActive(value: false);
		bowler4.SetActive(value: false);
		if (stepRow == 0)
		{
			if (typeMushWitch == 3)
			{
				stepRow++;
			}
		}
		else if (stepRow == 1)
		{
			if (typeMushWitch == 6)
			{
				stepRow++;
			}
			else
			{
				stepRow = 0;
			}
		}
		else if (stepRow == 2)
		{
			if (typeMushWitch == 11)
			{
				stepRow++;
				potionCreated = true;
			}
			else
			{
				stepRow = 0;
			}
		}
		if (stepRow == 0)
		{
			bowler.SetActive(value: true);
			Singleton<Sounds>.use.So2("poisonCancel", 0.5f);
		}
		else if (stepRow == 1)
		{
			Singleton<Sounds>.use.So("addRightMush");
			bowler2.SetActive(value: true);
			bowler2.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Stop();
			bowler2.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
		}
		else if (stepRow == 2)
		{
			Singleton<Sounds>.use.So("addRightMush");
			bowler3.SetActive(value: true);
			bowler3.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Stop();
			bowler3.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
		}
		else
		{
			Singleton<Sounds>.use.So("poisonMade");
			bowler4.SetActive(value: true);
			bowler4.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Stop();
			bowler4.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
		}
		Main.game.inv.RemoveItem(Items.mushwitch);
		Main.hero.ReturnToGame();
		Singleton<ManagerFunctions>.use.removeFunction(AddMush);
	}

	private void FillJar()
	{
		if (!actionDone && Main.hero.AnFrame(0.5f))
		{
			actionDone = true;
			Singleton<Sounds>.use.So("takePoison");
		}
		if (Main.hero.AnCompleted())
		{
			Main.game.inv.RemoveItem(Items.jar);
			Main.game.inv.AddItem(Items.jarfilled);
			Main.hero.ReturnToGame();
			Singleton<ManagerFunctions>.use.removeFunction(FillJar);
		}
	}

	private void Fiery()
	{
		if (!actionDone)
		{
			if (Main.hero.AnCompleted())
			{
				actionDone = true;
				kick = false;
				delayTime = 0f;
				rowKicks = 0;
				Main.hero.SetAn("Kindle2");
			}
			return;
		}
		if (!kick)
		{
			if (Mathf.Abs(Input.GetAxis("Horizontal") + Input.GetAxis("HorizontalJoy")) > 0f || Input.GetButtonDown("Jump"))
			{
				Main.hero.ReturnToGame();
				Main.hero.SetSomeAn("Kneel2");
				Singleton<ManagerFunctions>.use.removeFunction(Fiery);
				return;
			}
			if (delayTime > 0.1f)
			{
				lightFiery.SetActive(value: false);
			}
			if (Input.GetButtonDown("Action"))
			{
				lightFiery.SetActive(value: false);
				kick = true;
				Main.hero.SetAn("Kindle");
			}
			delayTime += Time.deltaTime;
			if (delayTime > 0.7f)
			{
				rowKicks = 0;
				delayTime = 0f;
			}
			return;
		}
		if (Main.hero.AnFrame(0.7f) && !lightFiery.activeSelf)
		{
			lightFiery.SetActive(value: true);
			Singleton<Sounds>.use.So("fireKick");
		}
		if (Main.hero.AnCompleted())
		{
			kick = false;
			rowKicks++;
			if (rowKicks >= 3)
			{
				Singleton<Sounds>.use.So("toFire");
				lightFiery.SetActive(value: false);
				fireMade = true;
				Main.game.inv.RemoveItem(Items.fiery);
				lightObj.SetActive(value: true);
				sunFire.SetActive(value: true);
				Main.hero.ReturnToGame();
				Main.hero.SetSomeAn("Kneel2");
				Singleton<ManagerFunctions>.use.removeFunction(Fiery);
			}
			else
			{
				lightFiery.SetActive(value: false);
				Main.hero.SetAn("Kindle2");
			}
		}
	}
}
