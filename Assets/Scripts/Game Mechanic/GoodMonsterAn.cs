using UnityEngine;

public class GoodMonsterAn : MonoBehaviour
{
	public GoodMonster goodMonster;

	public void SetChain()
	{
		goodMonster.chainMode = true;
		goodMonster.rb.SetActive(value: false);
	}

	public void SetTransRb()
	{
		goodMonster.chainMode = true;
	}

	public void ShowMonsterDead()
	{
		goodMonster.ShowMonsterDead();
	}

	public void Step1()
	{
		Singleton<Sounds>.use.So2("stepMonster1", 0.5f);
	}

	public void Step2()
	{
		Singleton<Sounds>.use.So2("stepMonster2", 0.5f);
	}

	public void EatCarrot()
	{
		Singleton<Sounds>.use.So("eatCarrot");
	}

	public void EndAll()
	{
		Singleton<Sounds>.use.So("com4_zvuk");
	}

	public void Roar()
	{
		Singleton<Sounds>.use.So("roar");
	}

	public void Neck()
	{
		Singleton<Sounds>.use.So("neckCrack");
	}

	public void TakeCarrot()
	{
		Singleton<Sounds>.use.So("takeMushMonster");
	}
}
