using UnityEngine;

public class FireplaceLev4 : ActiveObj
{
	public GameObject fire;

	public GameObject[] coal;

	private bool actionDone;

	private int nextCoal;

	private float timeGo;

	public AudioSource audioSource;

	public ParticleSystem smokePS;

	public BoxCollider2D runeCollider2D;

	private void Start()
	{
		Init();
		nextCoal = 0;
		GameObject[] array = coal;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: false);
		}
		runeCollider2D.enabled = false;
	}

	public override void DoAction()
	{
		if (fire.activeSelf && Main.game.inv.HaveItem(Items.bucketwater))
		{
			Main.hero.goHero.GoTo(base.transform.position.x, this);
		}
		else if (!fire.activeSelf && Main.game.inv.HaveItem(Items.branch))
		{
			Main.hero.goHero.GoTo(base.transform.position.x, this);
		}
	}

	public override void DoActionOnPos()
	{
		if (fire.activeSelf)
		{
			Main.hero.SetDirect(n: false);
			Main.hero.SetAn("Water2");
			actionDone = false;
			Singleton<ManagerFunctions>.use.addFunction(Water);
		}
		else
		{
			runeCollider2D.enabled = true;
			GetComponent<BoxCollider2D>().enabled = false;
			Main.hero.SetDirect(n: false);
			Main.hero.SetAn("Rabbit");
			actionDone = false;
			Singleton<ManagerFunctions>.use.addFunction(Branch);
		}
	}

	private void Water()
	{
		if (!actionDone && Main.hero.AnFrame(0.4f))
		{
			actionDone = true;
			Main.game.inv.RemoveItem(Items.bucketwater);
			Main.game.inv.AddItem(Items.bucket);
			fire.SetActive(value: false);
			Singleton<Sounds>.use.So("fireEnd");
			audioSource.Stop();
			smokePS.Play();
			GameObject[] array = coal;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(value: true);
			}
		}
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(Water);
			Main.hero.ReturnToGame();
		}
	}

	private void Branch()
	{
		timeGo += Time.deltaTime;
		if (!actionDone)
		{
			if (timeGo > 0.35f)
			{
				actionDone = true;
				Singleton<Sounds>.use.So("coal");
			}
		}
		else if (timeGo > 0.05f && nextCoal < coal.Length)
		{
			timeGo = 0f;
			GameObject obj = coal[nextCoal];
			obj.GetComponent<Rigidbody2D>().isKinematic = false;
			Vector2 force = new Vector2
			{
				x = -2f + 2f * Random.value,
				y = 3f + 1f * Random.value
			};
			obj.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
			obj.GetComponent<Rigidbody2D>().AddTorque(force.x * 0.1f, ForceMode2D.Impulse);
			nextCoal++;
		}
		if (Main.hero.AnCompleted())
		{
			Main.game.inv.RemoveItem(Items.branch);
			Singleton<ManagerFunctions>.use.removeFunction(Branch);
			Main.hero.ReturnToGame();
		}
	}
}
