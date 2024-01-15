using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(GameObj))]
public class Bed : ActiveObj
{
	public GameObject dreamBg;

	public Animator an;

	public PipeAby pipeAby;

	public GameObject[] dreams;

	public GameObject bed1;

	public GameObject bed2;

	public CatchDream catchDream;

	private float timeDream;

	private int status;

	private bool fear;

	private float timeSleep;

	private bool actionDone;

	private bool[] seeDream;

	private void Start()
	{
		Init();
		seeDream = new bool[5];
		seeDream[0] = false;
		seeDream[1] = false;
		seeDream[2] = false;
		seeDream[3] = false;
		seeDream[4] = false;
		an.Play("Show2", -1, 0f);
		dreamBg.SetActive(value: false);
		GameObject[] array = dreams;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: false);
		}
		bed2.SetActive(value: false);
		bed1.SetActive(value: true);
	}

	public override void DoAction()
	{
		Main.hero.goHero.GoTo(base.transform.position.x, this);
	}

	public override void DoActionOnPos()
	{
		bed1.SetActive(value: false);
		bed2.SetActive(value: true);
		Main.hero.SetDirect(n: true);
		Main.hero.SetAn("Sleep");
		Singleton<ManagerFunctions>.use.addFunction(ToSee);
		GetComponent<BoxCollider2D>().enabled = false;
		actionDone = false;
	}

	private void AllIsSee()
	{
		for (int i = 0; i < seeDream.Length; i++)
		{
			if (!seeDream[i])
			{
				return;
			}
		}
		Singleton<SteamF>.use.AddAch(9);
	}

	private void ToSee()
	{
		if (!actionDone && Main.hero.AnFrame(0.5f))
		{
			actionDone = true;
			Singleton<Sounds>.use.So("toBed");
		}
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.addFunction(Dream);
			Singleton<ManagerFunctions>.use.removeFunction(ToSee);
			status = 1;
			timeDream = 0f;
		}
	}

	private void Dream()
	{
		timeDream += Time.deltaTime;
		if (status == 1)
		{
			if (timeDream > 1f)
			{
				status = 2;
				GameObject[] array = dreams;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].SetActive(value: false);
				}
				dreamBg.SetActive(value: true);
				an.Play("Show", -1, 0f);
				Singleton<Sounds>.use.So("showDream");
			}
		}
		else if (status == 2)
		{
			if (an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
			{
				status = 3;
				timeDream = 0f;
			}
		}
		else if (status == 3)
		{
			fear = false;
			if (catchDream.dream == 0)
			{
				dreams[0].SetActive(value: true);
				timeSleep = 2f;
				seeDream[4] = true;
			}
			else if (catchDream.dream == 1 || catchDream.dream == 3 || catchDream.dream == 7)
			{
				dreams[1].SetActive(value: true);
				fear = true;
				timeSleep = 2f;
				seeDream[0] = true;
			}
			else if (catchDream.dream == 5 || catchDream.dream == 9 || catchDream.dream == 6)
			{
				dreams[3].SetActive(value: true);
				dreams[3].GetComponent<Animator>().Play("Dream", -1, 0f);
				timeSleep = 3f;
				seeDream[1] = true;
			}
			else if (catchDream.dream == 8)
			{
				dreams[2].SetActive(value: true);
				dreams[2].GetComponent<Animator>().Play("Dream", -1, 0f);
				pipeAby.goodMusic = true;
				timeSleep = 2.1f;
				seeDream[2] = true;
				Singleton<Sounds>.use.So2("pipeGood", 0.3f);
			}
			else if (catchDream.dream == 4 || catchDream.dream == 2 || catchDream.dream == 10)
			{
				dreams[4].SetActive(value: true);
				fear = true;
				timeSleep = 2f;
				seeDream[3] = true;
			}
			AllIsSee();
			status = 4;
			timeDream = 0f;
		}
		else if (status == 4)
		{
			if (timeDream > timeSleep)
			{
				Singleton<Sounds>.use.So("toWakeUp");
				status = 5;
				an.Play("Show2", -1, 0f);
				dreamBg.SetActive(value: false);
				if (fear)
				{
					Main.hero.SetAn("WakeUpFear");
				}
				else
				{
					Main.hero.SetAn("WakeUp");
				}
			}
		}
		else if (status == 5 && Main.hero.AnCompleted())
		{
			GetComponent<BoxCollider2D>().enabled = true;
			bed2.SetActive(value: false);
			bed1.SetActive(value: true);
			Main.hero.ReturnToGame();
			Singleton<ManagerFunctions>.use.removeFunction(Dream);
		}
	}
}
