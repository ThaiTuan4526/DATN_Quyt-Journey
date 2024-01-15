using UnityEngine;

public class TreeDoor : MonoBehaviour
{
	public GameObject block;

	public GameObject symbols;

	private bool direct;

	private int moves;

	private float waitTime;

	private bool goDown;

	private SpriteRenderer sp;

	private void Start()
	{
		symbols.SetActive(value: false);
		sp = symbols.GetComponent<SpriteRenderer>();
	}

	public void ShowRunes()
	{
		symbols.SetActive(value: true);
		waitTime = 0f;
		sp.color = new Color(1f, 1f, 1f, waitTime);
		Singleton<ManagerFunctions>.use.addFunction(GoRunes);
	}

	public void Go()
	{
		block.SetActive(value: false);
		Singleton<ManagerFunctions>.use.addFunction(GoDown);
		moves = 0;
		direct = true;
		waitTime = 0f;
		goDown = false;
	}

	public void FailRunes()
	{
		Singleton<ManagerFunctions>.use.removeFunction(GoFail);
		Singleton<ManagerFunctions>.use.addFunction(GoFail);
		waitTime = 0f;
		direct = true;
		moves = 0;
		symbols.SetActive(value: true);
		sp.color = new Color(1f, 1f, 1f, waitTime);
	}

	private void GoRunes()
	{
		waitTime += Time.deltaTime / 2f;
		sp.color = new Color(1f, 1f, 1f, waitTime);
		if (waitTime >= 1f)
		{
			Singleton<ManagerFunctions>.use.removeFunction(GoRunes);
		}
	}

	private void GoFail()
	{
		if (direct)
		{
			waitTime += Time.deltaTime;
			if (waitTime >= 0.9f)
			{
				direct = false;
			}
		}
		else
		{
			waitTime -= Time.deltaTime;
			if (waitTime <= 0.1f)
			{
				if (++moves > 1)
				{
					symbols.SetActive(value: false);
					Singleton<ManagerFunctions>.use.removeFunction(GoFail);
				}
				else
				{
					direct = true;
				}
			}
		}
		sp.color = new Color(1f, 1f, 1f, waitTime);
	}

	private void GoDown()
	{
		if (!goDown)
		{
			waitTime += Time.deltaTime;
			if (waitTime > 2f)
			{
				Singleton<Sounds>.use.So("totem_zvuk");
				goDown = true;
				Singleton<InputChecker>.use.StartVibro();
			}
			return;
		}
		if (direct)
		{
			base.transform.Translate(Time.deltaTime * 0.5f * Vector3.left);
			moves++;
			if (moves > 2)
			{
				moves = 0;
				direct = false;
			}
		}
		else
		{
			base.transform.Translate(Time.deltaTime * 0.5f * Vector3.right);
			moves++;
			if (moves > 2)
			{
				moves = 0;
				direct = true;
			}
		}
		base.transform.Translate(Vector3.down * Time.deltaTime);
		if (base.transform.position.y < -3.15f)
		{
			Singleton<InputChecker>.use.StopVibro();
			Main.hero.ReturnToGame();
			Main.hero.SetSomeAn("Stand");
			Singleton<ManagerFunctions>.use.removeFunction(GoDown);
			base.transform.position = new Vector2(base.transform.position.x, -3.15f);
		}
	}
}
