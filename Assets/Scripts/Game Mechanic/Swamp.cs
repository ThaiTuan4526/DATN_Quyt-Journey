using UnityEngine;

public class Swamp : MonoBehaviour, InterfaceBreakObj
{
	public Transform plat;

	public MonsterUnder monsterUnder;

	private Vector3 posPlat;

	private bool isActive;

	private void Start()
	{
		Main.game.arrBreakObj.Add(this);
		posPlat = plat.localPosition;
	}

	public void BreakObj()
	{
		plat.localPosition = posPlat;
		isActive = false;
		Singleton<ManagerFunctions>.use.removeFunction(GoDown);
	}

	private void GoDown()
	{
		plat.localPosition = new Vector3(plat.localPosition.x, plat.localPosition.y - 0.65999997f * Time.deltaTime, 0f);
		if (plat.localPosition.y < -2.5f)
		{
			if (monsterUnder.dead)
			{
				Singleton<SteamF>.use.AddAch(5);
			}
			Singleton<ManagerFunctions>.use.removeFunction(GoDown);
			Main.game.ReLoadScene();
		}
	}

	private void OnTriggerStay2D(Collider2D n)
	{
		if (!isActive && Main.hero.onPlatform && n.gameObject.CompareTag("Hero"))
		{
			Singleton<Sounds>.use.So("isDead");
			Main.hero.stopActions = true;
			Main.hero.SetSomeAn("Swamp");
			isActive = true;
			Singleton<ManagerFunctions>.use.addFunction(GoDown);
		}
	}
}
