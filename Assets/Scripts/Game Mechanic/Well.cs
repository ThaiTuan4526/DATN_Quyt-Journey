using UnityEngine;

public class Well : ActiveObj
{
	public Animator an;

	public AddBucket bucket;

	private bool actionDone;

	private float timeWait;

	private bool goBack;

	private void Start()
	{
		Init();
	}

	public override void DoAction()
	{
		Main.hero.goHero.GoTo(base.transform.position.x + 0.5f, this);
	}

	public override void DoActionOnPos()
	{
		Main.hero.SetDirect(n: false);
		Main.hero.SetAn("Well");
		Singleton<ManagerFunctions>.use.addFunction(Go);
		timeWait = 0f;
		goBack = false;
		actionDone = false;
	}

	private void Go()
	{
		if (goBack)
		{
			if (an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
			{
				Singleton<ManagerFunctions>.use.removeFunction(Go);
				Main.hero.ReturnToGame();
				an.Play("idle");
			}
			return;
		}
		if (!actionDone)
		{
			if (Main.hero.AnFrame(0.7f))
			{
				actionDone = true;
				an.Play("action");
				Singleton<Sounds>.use.So("well");
			}
			return;
		}
		timeWait += Time.deltaTime;
		if (timeWait > 1f)
		{
			an.Play("action2");
			goBack = true;
			bucket.AddWater();
			Main.hero.SetAn("Well2");
		}
	}
}
