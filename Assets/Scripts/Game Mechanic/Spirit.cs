using UnityEngine;

public class Spirit : MonoBehaviour
{
	public bool direct;

	public int type;

	public SpriteRenderer balcony;

	public Vector2 offset;

	private float lerpF;

	private bool taked;

	private float waitTime;

	private bool toHide;

	private Vector2 savedPos;

	private SpriteRenderer sp;

	private float rotateHero;

	private void Start()
	{
		if (!direct)
		{
			base.transform.Translate(1f * Vector3.right);
		}
		else
		{
			base.transform.Translate(1f * Vector3.left);
		}
		sp = base.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
		waitTime = 0f;
		base.gameObject.SetActive(value: false);
	}

	public void ShowSpirit()
	{
		base.gameObject.SetActive(value: true);
		Singleton<ManagerFunctions>.use.addFunction(ShowRuntime);
	}

	public void HelpSpirit()
	{
		Singleton<ManagerFunctions>.use.removeFunction(ShowRuntime);
		Singleton<ManagerFunctions>.use.addFunction(HelpRuntime);
		lerpF = 0f;
		taked = false;
		Main.hero.rb.gravityScale = 0.1f;
		Main.hero.rb.isKinematic = true;
	}

	private void ShowRuntime()
	{
		if (direct)
		{
			base.transform.Translate(0.25f * Vector3.right * Time.deltaTime);
		}
		else
		{
			base.transform.Translate(0.25f * Vector3.left * Time.deltaTime);
		}
		waitTime += 0.25f * Time.deltaTime;
		if (waitTime >= 1f)
		{
			Singleton<ManagerFunctions>.use.removeFunction(ShowRuntime);
		}
	}

	private void GoToTower()
	{
		Main.hero.heroT.Translate(1.5f * Vector3.right * Time.deltaTime);
		if (Main.hero.heroT.position.x > 38.9f)
		{
			Singleton<ManagerFunctions>.use.removeFunction(GoToTower);
			Singleton<LoaderBg>.use.SetLoader();
			Singleton<Saves>.use.keep("level8", 1);
			Main.location = 8;
			Singleton<Saves>.use.SaveData();
			Main.game.InvokeNewLoc();
		}
	}

	private void HelpRuntime()
	{
		if (toHide)
		{
			if (type == 1)
			{
				for (int i = 0; i < 3; i++)
				{
					if (rotateHero > 0f)
					{
						Main.hero.heroT.Rotate(100f * Vector3.back * Time.deltaTime);
						rotateHero -= 100f * Time.deltaTime;
					}
				}
			}
			if (type == 1)
			{
				base.transform.Translate(Vector3.right * Time.deltaTime);
				base.transform.Translate(Vector3.up * Time.deltaTime);
			}
			else if (type == 2)
			{
				base.transform.Translate(Vector3.right * Time.deltaTime);
				base.transform.Translate(Vector3.down * Time.deltaTime);
			}
			else
			{
				base.transform.Translate(Vector3.left * Time.deltaTime);
				base.transform.Translate(Vector3.up * Time.deltaTime);
			}
			sp.color = new Color(255f, 255f, 255f, lerpF);
			lerpF -= 0.25f * Time.deltaTime;
			if (Main.hero.heroT.position.y < 7.3f && type == 1)
			{
				Main.hero.SetDirect(n: false);
				Main.hero.SetAn("Watch");
			}
			if (lerpF <= 0f)
			{
				Main.hero.heroT.eulerAngles = Vector3.zero;
				base.gameObject.SetActive(value: false);
				Singleton<ManagerFunctions>.use.removeFunction(HelpRuntime);
				if (type == 1)
				{
					Singleton<ManagerFunctions>.use.addFunction(GoToTower);
					Main.hero.SetDirect(n: true);
					Main.hero.SetAn("Run");
				}
			}
			return;
		}
		if (!taked)
		{
			Main.game.cam.checkYpos = 3f;
			if (type == 1)
			{
				if (rotateHero < 64f)
				{
					Main.hero.heroT.Rotate(Vector3.forward * Time.deltaTime * 100f);
					rotateHero += 100f * Time.deltaTime;
				}
				else
				{
					Main.hero.SetAn("Fly");
				}
			}
			base.transform.position = new Vector2(Mathf.Lerp(base.transform.position.x, Main.hero.heroT.position.x + offset.x, lerpF), Mathf.Lerp(base.transform.position.y, Main.hero.heroT.position.y + offset.y, lerpF));
			lerpF += Time.deltaTime * 1f;
			if (lerpF >= 1f || Vector2.Distance(base.transform.position, Main.hero.heroT.position) < 0.1f)
			{
				lerpF = 0f;
				taked = true;
				waitTime = 0f;
				if (type == 1)
				{
					Main.hero.rb.velocity = Vector2.zero;
					Main.hero.rb.isKinematic = true;
					Main.hero.speedX = 0f;
					Main.hero.SetAn("Fly");
					Main.game.cam.SwitchOffsetY(2f);
				}
				savedPos = base.transform.position;
			}
			return;
		}
		Main.game.cam.checkYpos = 3f;
		if (waitTime >= 2f)
		{
			base.transform.position = new Vector2(Mathf.Lerp(savedPos.x, 37.93f + offset.x, lerpF), Mathf.Lerp(savedPos.y, 9f + offset.y, lerpF));
			Main.hero.heroT.position = new Vector2(base.transform.position.x - offset.x, base.transform.position.y - offset.y);
			lerpF += Time.deltaTime * 0.5f;
			if (lerpF >= 1f)
			{
				toHide = true;
				lerpF = 1f;
				if (type == 1)
				{
					balcony.sortingLayerName = "overHero";
					Main.hero.rb.isKinematic = false;
					Main.hero.rb.gravityScale = 1.5f;
					Main.hero.SetAn("Landing");
				}
			}
		}
		else
		{
			waitTime += Time.deltaTime;
			if (waitTime >= 2f)
			{
				Singleton<Sounds>.use.So2("ghostTakeHelp", 0.5f);
			}
		}
	}
}
