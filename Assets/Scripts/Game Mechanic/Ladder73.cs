using UnityEngine;

public class Ladder73 : ActiveObj
{
	private bool go;

	private int status = 1;

	public override void DoAction()
	{
		Main.hero.goHero.GoTo(base.transform.position.x, this);
	}

	public override void DoActionOnPos()
	{
		go = false;
		status = 1;
		Main.hero.SetDirect(n: false);
		Main.hero.rb.isKinematic = true;
		Main.hero.SetAn("Climb");
		Singleton<Sounds>.use.So("ladderDown");
		Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
	}

	private void RuntimeAction()
	{
		if (!go)
		{
			if (Main.hero.AnCompleted())
			{
				go = true;
				Main.hero.SetAn("Climb2");
			}
		}
		else if (status == 1)
		{
			Main.hero.heroT.Translate(2f * Vector3.up * Time.deltaTime);
			if (Main.hero.heroT.position.y >= 13.1f)
			{
				status = 2;
				Main.hero.ReturnToGame();
				Main.hero.rb.AddForce(Main.hero.heroT.up * Main.hero.jumpSpeed, ForceMode2D.Impulse);
				Main.hero.SetDirect(n: true);
				Main.hero.SetAn("Jump");
				Main.hero.onPlatform = false;
				Main.hero.jump = true;
				Main.hero.idle = false;
				Main.hero.run = false;
				Singleton<Sounds>.use.StopS();
				Singleton<Sounds>.use.So2("jump", 0.2f);
			}
		}
		else if (status == 2)
		{
			Main.hero.heroT.Translate(2.5f * Vector3.right * Time.deltaTime);
			if (Main.hero.onPlatform)
			{
				Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			}
		}
	}
}
