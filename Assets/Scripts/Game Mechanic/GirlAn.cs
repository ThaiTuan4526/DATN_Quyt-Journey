using UnityEngine;

public class GirlAn : MonoBehaviour
{
	public GameObject girl;

	public GameObject girlJump;

	public CutSceneLevel7 cutSceneLevel7;

	public void Step1()
	{
		Singleton<Sounds>.use.So("stepGrass1");
	}

	public void Step2()
	{
		Singleton<Sounds>.use.So("stepGrass2");
	}

	public void Sniff()
	{
		Singleton<Sounds>.use.So2("sniffing2", 0.5f);
	}

	public void OpenCab()
	{
		Singleton<Sounds>.use.So("openCab");
	}

	public void ShowGirl()
	{
		girlJump.SetActive(value: true);
	}

	public void HideGirl()
	{
		girl.SetActive(value: false);
	}

	public void ShowButterfly()
	{
		cutSceneLevel7.startTrans = true;
	}
}
