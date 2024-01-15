using UnityEngine;

public class Monster7Steps : MonoBehaviour
{
	public HeroFloor heroFloor;

	public Monster7 monster7;

	public BoardGame boardGame;

	public void Step1()
	{
		if (Mathf.Abs(heroFloor.floor - monster7.floor) <= 1)
		{
			Singleton<Sounds>.use.So2("stepMonster1", 0.15f);
		}
	}

	public void Step2()
	{
		if (Mathf.Abs(heroFloor.floor - monster7.floor) <= 1)
		{
			Singleton<Sounds>.use.So2("stepMonster2", 0.15f);
		}
	}

	public void Jump()
	{
		if (Mathf.Abs(heroFloor.floor - monster7.floor) <= 1 && !boardGame.isGame)
		{
			Singleton<Sounds>.use.So("jumpEnemy");
		}
	}

	public void Lick()
	{
		if (Mathf.Abs(heroFloor.floor - monster7.floor) <= 1 && !boardGame.isGame)
		{
			Singleton<Sounds>.use.So("lick");
		}
	}

	public void Eat()
	{
		if (Mathf.Abs(heroFloor.floor - monster7.floor) <= 1)
		{
			Singleton<Sounds>.use.So("eat3_so");
		}
	}
}
