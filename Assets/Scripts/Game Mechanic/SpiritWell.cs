using UnityEngine;

public class SpiritWell : MonoBehaviour
{
	public Animator an;

	private int status;

	private GameObject ghost;

	private float waitTime;

	private float speed;

	private float posX;

	private void Start()
	{
		ghost = base.gameObject.transform.GetChild(0).gameObject;
		ghost.SetActive(value: false);
		Singleton<ManagerFunctions>.use.addFunction(Go);
		waitTime = 2f;
		status = 1;
		posX = base.gameObject.transform.position.x;
		an.speed = 0f;
	}

	private void Go()
	{
		if (status == 1)
		{
			waitTime -= Time.deltaTime;
			if (waitTime < 0f)
			{
				status = 2;
				speed = 1f;
				ghost.SetActive(value: true);
				ghost.transform.localPosition = new Vector2(0f, -1.51f);
				an.gameObject.GetComponent<SpriteRenderer>().flipX = Main.hero.heroT.position.x > posX;
			}
		}
		else if (status == 2)
		{
			ghost.transform.localPosition = new Vector2(0f, ghost.transform.localPosition.y + speed * Time.deltaTime);
			if (ghost.transform.localPosition.y > 0.75f)
			{
				status = 3;
				waitTime = 0f;
				an.speed = 1f;
			}
			HeroNear();
		}
		else if (status == 3)
		{
			HeroNear();
		}
		else if (status == 4)
		{
			ghost.transform.localPosition = new Vector2(0f, ghost.transform.localPosition.y - speed * Time.deltaTime);
			if (ghost.transform.localPosition.y < -1.51f)
			{
				ghost.SetActive(value: false);
				status = 1;
				waitTime = 10f;
			}
		}
	}

	private void HeroNear()
	{
		if (Mathf.Abs(Main.hero.heroT.position.x - posX) < 3.5f)
		{
			speed = 5f;
			status = 4;
			an.speed = 0f;
		}
	}
}
