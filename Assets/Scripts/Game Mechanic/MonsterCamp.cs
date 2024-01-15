using UnityEngine;

public class MonsterCamp : MonoBehaviour
{
	public GameObject skin;

	public GameObject inWindow;

	private SpriteRenderer sp;

	private Animator an;

	private bool onDown = true;

	private int status = 1;

	private float timeGoDown;

	private bool intoBed;

	private bool goToSee;

	private bool goBack;

	private bool peep;

	private bool AnCompleted()
	{
		return an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;
	}

	public void SetGoUp()
	{
		an = skin.GetComponent<Animator>();
		sp = skin.GetComponent<SpriteRenderer>();
		Singleton<ManagerFunctions>.use.removeFunction(InWindow);
		Singleton<ManagerFunctions>.use.removeFunction(GoUp);
		Singleton<ManagerFunctions>.use.removeFunction(See);
		if (onDown)
		{
			status = 1;
			timeGoDown = 0f;
			onDown = false;
			inWindow.SetActive(value: false);
			Singleton<ManagerFunctions>.use.addFunction(GoUp);
			sp.flipX = false;
			an.Play("Idle");
			base.gameObject.transform.position = new Vector3(-0.95f, -2.41f, 0f);
		}
		else
		{
			base.gameObject.transform.position = new Vector3(-5.17f, 0.33f, 0f);
			sp.flipX = false;
			an.Play("Tremble");
			Singleton<ManagerFunctions>.use.addFunction(See);
			intoBed = true;
		}
	}

	public void SetGoDown()
	{
		Singleton<ManagerFunctions>.use.removeFunction(InWindow);
		Singleton<ManagerFunctions>.use.removeFunction(GoUp);
		Singleton<ManagerFunctions>.use.removeFunction(See);
		Singleton<ManagerFunctions>.use.addFunction(InWindow);
		timeGoDown = 0f;
	}

	private void GoUp()
	{
		if (status == 1)
		{
			if (AnCompleted())
			{
				sp.flipX = true;
				an.Play("Jump");
				status = 2;
			}
		}
		else if (status == 2)
		{
			if (AnCompleted())
			{
				base.gameObject.transform.position = new Vector3(-2.202f, 0.33f, 0f);
				an.Play("Crawl");
				status = 3;
			}
		}
		else if (status == 3)
		{
			base.gameObject.transform.Translate(2f * Vector3.left * Time.deltaTime);
			if (base.gameObject.transform.position.x < -5.17f)
			{
				base.gameObject.transform.position = new Vector3(-5.17f, 0.33f, 0f);
				an.Play("Turn");
				status = 4;
			}
		}
		else if (status == 4 && AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(GoUp);
			Singleton<ManagerFunctions>.use.addFunction(See);
			intoBed = true;
			sp.flipX = false;
			an.Play("Tremble");
		}
	}

	private void See()
	{
		if (goToSee)
		{
			base.gameObject.transform.Translate(2f * Vector3.right * Time.deltaTime);
			if (base.gameObject.transform.position.x > -2.4f)
			{
				base.gameObject.transform.position = new Vector3(-2.4f, 0.33f, 0f);
				an.Play("Peep");
				goToSee = false;
			}
		}
		if (goBack)
		{
			if (status == 1)
			{
				if (AnCompleted())
				{
					sp.flipX = true;
					an.Play("Crawl");
					status = 2;
				}
			}
			else if (status == 2)
			{
				base.gameObject.transform.Translate(2.5f * Vector3.left * Time.deltaTime);
				if (base.gameObject.transform.position.x < -5.17f)
				{
					base.gameObject.transform.position = new Vector3(-5.17f, 0.33f, 0f);
					an.Play("Turn");
					status = 3;
				}
			}
			else if (status == 3 && AnCompleted())
			{
				sp.flipX = false;
				an.Play("Tremble");
				goBack = false;
				intoBed = true;
				timeGoDown = 0f;
			}
		}
		if (peep && ((Main.hero.heroT.position.x > -1.6f && Main.hero.heroT.position.y > -2.3f) || (Main.hero.heroT.position.x > -1.5f && Main.hero.heroT.position.x < 0f && Main.hero.heroT.position.y > -2.9f)))
		{
			goToSee = false;
			goBack = true;
			peep = false;
			status = 1;
			an.Play("Turn");
		}
		if (!intoBed)
		{
			return;
		}
		timeGoDown += Time.deltaTime;
		if (timeGoDown > 2f)
		{
			if (Main.hero.heroT.position.y < -3f)
			{
				goToSee = true;
				peep = true;
				intoBed = false;
				an.Play("Crawl");
			}
			else
			{
				timeGoDown = 0f;
			}
		}
	}

	private void InWindow()
	{
		if (!onDown)
		{
			timeGoDown += Time.deltaTime;
			if (timeGoDown > 5f)
			{
				onDown = true;
				inWindow.transform.position = new Vector3(5.8f, 0.47f, 0f);
				inWindow.SetActive(value: true);
			}
		}
		else
		{
			inWindow.transform.Translate(0.5f * Time.deltaTime * Vector3.left);
			if (inWindow.transform.position.x < 5.33f)
			{
				inWindow.transform.position = new Vector3(5.33f, 0.47f, 0f);
				Singleton<ManagerFunctions>.use.removeFunction(InWindow);
			}
		}
	}
}
