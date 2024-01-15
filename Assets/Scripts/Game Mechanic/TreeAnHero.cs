using UnityEngine;

public class TreeAnHero : MonoBehaviour
{
	public void HideHero()
	{
		Main.hero.gameObject.SetActive(value: false);
	}

	public void ShowHero()
	{
		Main.hero.gameObject.SetActive(value: true);
	}

	public void ToW()
	{
		Main.hero.SetDirect(n: false);
		Main.hero.SetAn("Watch");
	}

	public void SoThrow()
	{
		Singleton<Sounds>.use.So("throwHero");
	}

	public void Crack1()
	{
		Singleton<Sounds>.use.So("treeCrack");
	}

	public void Kick()
	{
		Singleton<Sounds>.use.So("kickToTree");
	}

	public void CrashPipe()
	{
		Singleton<Sounds>.use.So("crachPipe");
	}

	public void TakePipe()
	{
		Singleton<Sounds>.use.So("takeTreePipe");
	}

	public void FallPipe()
	{
		Singleton<Sounds>.use.So("pipeFall");
	}

	public void Shake()
	{
		Singleton<Sounds>.use.So("treeShakeHero");
	}
}
