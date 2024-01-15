using UnityEngine;

public class LadderAn : MonoBehaviour
{
	public void Lad1()
	{
		Singleton<Sounds>.use.So("lad1");
	}

	public void Lad2()
	{
		Singleton<Sounds>.use.So("lad2");
	}

	public void Lad3()
	{
		Singleton<Sounds>.use.So("lad3");
	}
}
