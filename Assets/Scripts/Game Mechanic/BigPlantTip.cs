using UnityEngine;

public class BigPlantTip : ActiveObj
{
	private bool actionDone;

	private Animator an;

	private GameObject ns;

	private void Start()
	{
		Init();
		actionDone = false;
	}

	public override void DoAction()
	{
		if (!actionDone && Main.hero.CanUse())
		{
			DoActionOnPos();
		}
	}

	public override void DoActionOnPos()
	{
		actionDone = true;
		ns = Object.Instantiate(Resources.Load("Prefabs/GameObj/Cloud3"), Main.hero.heroT, instantiateInWorldSpace: false) as GameObject;
		ns.transform.localPosition = Vector3.zero;
		an = ns.transform.GetChild(1).gameObject.GetComponent<Animator>();
		Singleton<ManagerFunctions>.use.addIndieFunction(RuntimeAction);
	}

	private void RuntimeAction()
	{
		if (an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
		{
			actionDone = false;
			Object.Destroy(ns);
			Singleton<ManagerFunctions>.use.removeIndieFunction(RuntimeAction);
		}
	}
}
