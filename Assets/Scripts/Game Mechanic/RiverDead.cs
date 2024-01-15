using UnityEngine;

public class RiverDead : MonoBehaviour
{
	public MonsterBridge monsterBridge;

	private float endTime;

	private void OnTriggerEnter2D(Collider2D n)
	{
		if (n.gameObject.CompareTag("Hero"))
		{
			Singleton<Sounds>.use.So("fallToWater2");
			endTime = 0f;
			Main.hero.SetAn("Dive");
			Main.hero.rb.velocity = Vector2.zero;
			Main.hero.rb.isKinematic = true;
			Main.hero.stopActions = true;
			Singleton<ManagerFunctions>.use.addFunction(EndRiver);
		}
	}

	private void EndRiver()
	{
		if (Main.hero.gameObject.activeSelf)
		{
			if (Main.hero.AnCompleted())
			{
				if (monsterBridge.dead)
				{
					Singleton<SteamF>.use.AddAch(6);
				}
				Main.hero.gameObject.SetActive(value: false);
			}
		}
		else
		{
			endTime += Time.deltaTime;
			if (endTime > 1f)
			{
				Singleton<ManagerFunctions>.use.removeFunction(EndRiver);
				Main.game.ReLoadScene();
			}
		}
	}
}
