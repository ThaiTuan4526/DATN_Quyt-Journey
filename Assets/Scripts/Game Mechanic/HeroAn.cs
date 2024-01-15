using UnityEngine;

public class HeroAn : MonoBehaviour
{
	internal string step1;

	internal string step2;

	public void Step1()
	{
		Singleton<Sounds>.use.So2(step1, 0.5f + 0.5f * Random.value);
	}

	public void Step2()
	{
		Singleton<Sounds>.use.So2(step2, 0.5f + 0.5f * Random.value);
	}
}
