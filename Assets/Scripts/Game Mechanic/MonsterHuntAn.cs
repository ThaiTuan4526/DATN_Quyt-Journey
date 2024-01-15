using UnityEngine;

public class MonsterHuntAn : MonoBehaviour
{
	public void Roar()
	{
		Singleton<Sounds>.use.So("roarCreate");
	}

	public void Rope()
	{
		Singleton<Sounds>.use.So2("ropeCrash", 0.4f);
	}

	public void Fall()
	{
		Singleton<Sounds>.use.So("fallMonster");
	}
}
