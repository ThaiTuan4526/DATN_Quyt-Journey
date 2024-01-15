using UnityEngine;

public class BroAnScene1 : MonoBehaviour
{
	public GameObject mushroom;

	public void Step1()
	{
		Singleton<Sounds>.use.So("stepGrass1");
	}

	public void Step2()
	{
		Singleton<Sounds>.use.So("stepGrass2");
	}

	public void TakeMush()
	{
		mushroom.SetActive(value: false);
		Singleton<Sounds>.use.So("takeMush");
	}
}
