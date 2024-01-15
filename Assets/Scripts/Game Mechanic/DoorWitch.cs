using UnityEngine;

public class DoorWitch : MonoBehaviour
{
	public GameObject door1;

	public GameObject door2;

	private float waitTime;

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
				Main.hero.SetAn("Run");
			}
			return;
		}
		Main.hero.heroT.position = new Vector2(Main.hero.heroT.position.x + 1.8f * Time.deltaTime, Main.hero.heroT.position.y);
		if (Main.hero.heroT.position.x >= -8.55f)
		{
			Main.hero.sp.maskInteraction = SpriteMaskInteraction.None;
			Singleton<Sounds>.use.So("closeDoor1");
			door2.SetActive(value: false);
			door1.SetActive(value: true);
			Main.hero.ReturnToGame();
			Main.hero.SetAn(Main.hero.idleAn);
			Object.Destroy(base.gameObject);
		}
	}
}
