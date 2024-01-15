using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TakeBro : ActiveObj
{
	public GameObject broInCage;

	public Bro2 bro;

	public GameObject worm;

	public SpriteRenderer cageAn;

	private int status;

	private float waitTime;

	private Image fade;

	private bool actionDone;

	private void Start()
	{
		Init();
		bro.gameObject.SetActive(value: false);
		worm.SetActive(value: false);
	}

	public override void DoAction()
	{
		Main.hero.goHero.GoTo(17.779f, this);
	}

	public override void DoActionOnPos()
	{
		Main.hero.transform.position = new Vector2(17.779f, Main.hero.transform.position.y);
		Main.hero.SetDirect(n: true);
		Main.hero.SetAn("Lower");
		broInCage.SetActive(value: false);
		GetComponent<BoxCollider2D>().enabled = false;
		cageAn.sortingOrder = 100;
		status = 1;
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
		actionDone = false;
	}

	private void RuntimeAction()
	{
		if (status == 1)
		{
			if (!actionDone && Main.hero.AnFrame(0.25f))
			{
				actionDone = true;
				Singleton<Sounds>.use.So2("broTake", 0.6f);
			}
			if (Main.hero.AnCompleted())
			{
				Main.hero.transform.Translate(Vector3.left * 0.466f);
				Main.hero.SetAn("Watch");
				bro.gameObject.SetActive(value: true);
				bro.SetAn("Swing");
				status = 2;
				waitTime = 0f;
				cageAn.sortingOrder = -2;
			}
		}
		else if (status == 2)
		{
			waitTime += Time.deltaTime;
			if (waitTime >= 3f)
			{
				status = 3;
				bro.SetAn("Puke");
			}
		}
		else if (status == 3)
		{
			if (bro.AnCompleted())
			{
				status = 4;
				bro.SetAn("Breath");
			}
		}
		else if (status == 4)
		{
			worm.transform.Translate(Time.deltaTime * 0.19f * Vector3.left);
			if (worm.transform.position.x < 17.8f)
			{
				Main.hero.SetAn("KillWorm");
				status = 5;
			}
		}
		else if (status == 5)
		{
			worm.transform.Translate(Time.deltaTime * 0.19f * Vector3.left);
			if (worm.activeSelf && Main.hero.AnFrame(0.45f))
			{
				Singleton<Sounds>.use.So2("killWorm", 0.5f);
				worm.SetActive(value: false);
			}
			if (Main.hero.AnCompleted())
			{
				status = 6;
				waitTime = 0f;
				Main.hero.SetAn("Idle");
			}
		}
		else if (status == 6)
		{
			waitTime += Time.deltaTime;
			if (waitTime >= 0.1f)
			{
				status = 7;
				bro.gameObject.SetActive(value: false);
				Main.hero.SetAn("HugBro");
				waitTime = 0f;
				Singleton<Music>.use.MusicStart("finalMusic");
				actionDone = false;
			}
		}
		else if (status == 7)
		{
			if (!actionDone && waitTime > 0.5f)
			{
				Singleton<Sounds>.use.So2("hugBro", 0.5f);
				actionDone = true;
			}
			waitTime += Time.deltaTime;
			if (waitTime >= 5f)
			{
				status = 8;
				GameObject gameObject = Object.Instantiate(Resources.Load("Prefabs/Other/EndFade"), Main.game.canvasT) as GameObject;
				fade = gameObject.GetComponent<Image>();
				waitTime = 0f;
			}
		}
		else if (status == 8)
		{
			waitTime += Time.deltaTime * 0.2f;
			fade.color = new Color(0f, 0f, 0f, waitTime);
			if (waitTime >= 1f)
			{
				Singleton<SteamF>.use.AddAch(3);
				Main.location = 0;
				Singleton<Sounds>.use.StopAllSounds();
				Singleton<ManagerFunctions>.use.ClearAll();
				Singleton<InputChecker>.use.ClearObj();
				Singleton<LoaderBg>.use.SetLoader();
				SceneManager.LoadScene("Credits");
			}
		}
	}
}
