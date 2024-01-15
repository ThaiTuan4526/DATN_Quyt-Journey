using UnityEngine;

public class GirlAn1 : MonoBehaviour
{
	public CutSceneLevel7 cutSceneLevel7;

	public void Step1()
	{
		Singleton<Sounds>.use.So2("stoneStep1", 0.4f);
	}

	public void Step2()
	{
		Singleton<Sounds>.use.So2("stoneStep2", 0.4f);
	}

	public void ShowButterfly()
	{
		cutSceneLevel7.startTrans = true;
	}

	public void OpenBank()
	{
		Singleton<Sounds>.use.So2("openBank", 0.5f);
	}
}
