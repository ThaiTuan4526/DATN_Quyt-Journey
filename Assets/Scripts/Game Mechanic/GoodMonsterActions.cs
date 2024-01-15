using UnityEngine;

public class GoodMonsterActions : ActiveObj
{
	public GoodMonster monster;

	public BowlerChemistry bowlerChemistry;

	public Transform butterfly;

	public GameObject b1;

	public GameObject b2;

	public GameObject b3;

	private bool isTrans;

	private bool actionDone;

	private Vector2 pos1;

	private Vector2 pos2;

	private float lerpF;

	private int status;

	private Vector2 posB;

	private float waitTime;

	private void Start()
	{
		Init();
		butterfly.gameObject.SetActive(value: false);
		posB = new Vector2(13.329f, 5.066f);
	}

	public override void DoAction()
	{
		if (monster.transAn)
		{
			return;
		}
		if (!isTrans && (Main.game.inv.HaveItem(Items.butterfly) || Main.game.inv.HaveItem(Items.butterflyMix) || Main.game.inv.HaveItem(Items.butterflyMix2)))
		{
			Main.hero.goHero.GoTo(13f, this);
		}
		else if (Main.game.inv.HaveItem(Items.vegetable) || Main.game.inv.HaveItem(Items.vegetableMix))
		{
			if (isTrans)
			{
				Main.hero.goHero.GoTo(13.5f, this);
			}
			else
			{
				Main.hero.goHero.GoTo(13.8f, this);
			}
		}
		else
		{
			Main.hero.goHero.GoTo(base.transform.position.x, this);
		}
	}

	public override void DoActionOnPos()
	{
		if (!isTrans && (Main.game.inv.HaveItem(Items.butterfly) || Main.game.inv.HaveItem(Items.butterflyMix) || Main.game.inv.HaveItem(Items.butterflyMix2)))
		{
			Main.hero.SetDirect(n: true);
			Main.hero.SetAn("Bank");
			actionDone = false;
			Singleton<ManagerFunctions>.use.addFunction(ButterflyMix);
			monster.SetAn("Open");
			butterfly.gameObject.SetActive(value: true);
			if (Main.game.inv.HaveItem(Items.butterflyMix) || Main.game.inv.HaveItem(Items.butterflyMix2))
			{
				b1.SetActive(value: false);
				if (bowlerChemistry.setBorn)
				{
					b2.SetActive(value: true);
					b3.SetActive(value: false);
				}
				else
				{
					b3.SetActive(value: true);
					b2.SetActive(value: false);
				}
			}
			else
			{
				b1.SetActive(value: true);
				b2.SetActive(value: false);
				b3.SetActive(value: false);
			}
			butterfly.position = posB;
			pos1 = posB;
			pos2 = new Vector2(13.637001f, 5.5550003f);
			lerpF = 0f;
			status = 1;
		}
		else if (Main.game.inv.HaveItem(Items.vegetable) || Main.game.inv.HaveItem(Items.vegetableMix))
		{
			Main.hero.SetDirect(n: true);
			actionDone = false;
			if (isTrans)
			{
				Main.hero.SetAn("FeedMonster");
				Singleton<ManagerFunctions>.use.addFunction(FeedMonster);
			}
			else
			{
				Main.hero.SetAn("FeedFail");
				Singleton<ManagerFunctions>.use.addFunction(FeedFail);
			}
		}
		else if (!isTrans)
		{
			Main.hero.SetDirect(n: true);
			Main.hero.SetAn("CareMonster");
			actionDone = false;
			Singleton<ManagerFunctions>.use.addFunction(CareMonster);
		}
		else
		{
			Main.hero.ReturnToGame();
			Main.hero.SetAn("Idle");
		}
	}

	private void ButterflyMix()
	{
		if (status == 1)
		{
			lerpF += Time.deltaTime * 3f;
			butterfly.position = Vector2.Lerp(pos1, pos2, lerpF);
			if (lerpF >= 1f)
			{
				status = 2;
				pos1 = butterfly.position;
				pos2 = new Vector2(13.719f, 6.637f);
				lerpF = 0f;
			}
		}
		else if (status == 2)
		{
			lerpF += Time.deltaTime;
			butterfly.position = Vector2.Lerp(pos1, pos2, lerpF);
			if (lerpF >= 1f)
			{
				status = 3;
				pos1 = butterfly.position;
				pos2 = new Vector2(15.097f, 5.395f);
				lerpF = 0f;
				waitTime = 0f;
			}
		}
		else if (status == 3)
		{
			waitTime += Time.deltaTime;
			if (waitTime > 1f)
			{
				Singleton<Sounds>.use.So("flyFast");
				status = 4;
			}
		}
		else if (status == 4)
		{
			lerpF += Time.deltaTime * 3f;
			butterfly.position = Vector2.Lerp(pos1, pos2, lerpF);
			if (lerpF >= 1f)
			{
				butterfly.gameObject.SetActive(value: false);
				monster.SetTrans(bowlerChemistry.setBorn);
				if (bowlerChemistry.setBorn)
				{
					isTrans = true;
					status = 5;
					GetComponent<BoxCollider2D>().enabled = false;
				}
				else
				{
					Singleton<ManagerFunctions>.use.removeFunction(ButterflyMix);
					Main.hero.ReturnToGame();
				}
				Main.game.inv.RemoveItem(Items.butterfly);
				Main.game.inv.RemoveItem(Items.butterflyMix);
				Main.game.inv.RemoveItem(Items.butterflyMix2);
			}
		}
		else if (status == 5 && monster.AnCompleted())
		{
			GetComponent<BoxCollider2D>().enabled = true;
			icon.transform.localPosition = new Vector2(2.22f, 2.87f);
			Singleton<ManagerFunctions>.use.removeFunction(ButterflyMix);
			Main.hero.ReturnToGame();
		}
	}

	private void FeedMonster()
	{
		if (!actionDone && Main.hero.AnFrame(0.45f))
		{
			Main.game.inv.RemoveItem(Items.vegetable);
			Main.game.inv.RemoveItem(Items.vegetableMix);
			monster.Feed(bowlerChemistry.setPower);
		}
		if (Main.hero.AnCompleted())
		{
			if (!bowlerChemistry.setPower)
			{
				Main.hero.ReturnToGame();
			}
			else
			{
				DeActivated();
				base.gameObject.SetActive(value: false);
			}
			Singleton<ManagerFunctions>.use.removeFunction(FeedMonster);
		}
	}

	private void FeedFail()
	{
		if (Main.hero.AnCompleted())
		{
			monster.SetAn("Idle1");
			Singleton<ManagerFunctions>.use.removeFunction(FeedFail);
			Main.hero.ReturnToGame();
		}
	}

	private void CareMonster()
	{
		if (!actionDone && Main.hero.AnFrame(0.4f))
		{
			actionDone = true;
			monster.SetCare();
			Singleton<Sounds>.use.So("growl");
		}
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(CareMonster);
			Main.hero.ReturnToGame();
		}
	}
}
