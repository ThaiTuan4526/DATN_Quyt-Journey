using UnityEngine;

public class Shack : MonoBehaviour
{
	public GameObject monster;

	public GameObject door1;

	public GameObject door2;

	public void SetMode(bool n, bool inside)
	{
		if (!n)
		{
			base.gameObject.SetActive(value: false);
			return;
		}
		base.gameObject.SetActive(value: true);
		base.transform.position = new Vector3(Main.game.cam.transform.position.x, Main.game.cam.transform.position.y, 0f);
		if (inside)
		{
			Singleton<Sounds>.use.So("monsterComing");
			monster.SetActive(value: true);
			door1.SetActive(value: true);
			door2.SetActive(value: false);
		}
		else
		{
			monster.SetActive(value: false);
			door1.SetActive(value: false);
			door2.SetActive(value: true);
		}
	}
}
