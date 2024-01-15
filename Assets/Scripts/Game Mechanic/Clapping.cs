using UnityEngine;

public class Clapping : MonoBehaviour
{
	public MonsterAmberOnTree monster;

	public void SoClapping()
	{
		Singleton<Sounds>.use.So2("clapping", 0.5f);
	}

	public void Throw()
	{
		Singleton<Sounds>.use.So("throwAmber");
	}

	public void Pull()
	{
		Singleton<Sounds>.use.So("pullAmber");
	}

	public void Fall()
	{
		Singleton<Sounds>.use.So("fallAmber");
	}

	public void ToFly()
	{
		monster.SetFly();
	}
}
