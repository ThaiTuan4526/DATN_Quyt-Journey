using UnityEngine;

public class StepsMonster6_2 : MonoBehaviour
{
	public void Step1()
	{
		Singleton<Sounds>.use.So("stepWood1");
	}

	public void Step2()
	{
		Singleton<Sounds>.use.So("stepWood2");
	}

	public void WakeUp()
	{
		Singleton<Sounds>.use.So("toWakeUp");
	}

	public void Blink()
	{
		Singleton<Sounds>.use.So("blink");
	}
}
