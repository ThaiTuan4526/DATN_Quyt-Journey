using UnityEngine;

public class StartRunLev3 : MonoBehaviour
{
	public GameObject door1;

	public GameObject door2;

	private float waitTime;

	private Vector2 posGo;

	private Vector2 posHero;

	private float timeGo;

	private int status = 1;

	private void Start()
	{
		door1.SetActive(value: false);
		door2.SetActive(value: true);
	}

	private void Update()
	{
		if (status == 1)
		{
			if ((waitTime += 1f) > 2f)
			{
				status = 2;
				timeGo = 0f;
				posHero = Main.hero.heroT.position;
				posGo = new Vector2(-1f, -1.987383f);
			}
			return;
		}
		timeGo += 1.5f * Time.deltaTime;
		Main.hero.heroT.position = Vector2.Lerp(posHero, posGo, timeGo);
		if (timeGo >= 1f)
		{
			Singleton<Sounds>.use.So("closeDoor1");
			door2.SetActive(value: false);
			door1.SetActive(value: true);
			Main.hero.ReturnToGame();
			Main.hero.SetAn(Main.hero.idleAn);
			Object.Destroy(base.gameObject);
		}
	}
}
