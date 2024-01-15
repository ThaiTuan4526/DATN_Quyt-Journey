using UnityEngine;

public class EndLevel3 : MonoBehaviour
{
	private bool catchHero;

	private bool fAdded;

	private void OnTriggerEnter2D(Collider2D n)
	{
		if (!fAdded && n.gameObject.CompareTag("Hero"))
		{
			fAdded = true;
			Singleton<ManagerFunctions>.use.addFunction(FinalRun);
		}
	}

	private void FinalRun()
	{
		if (catchHero)
		{
			Main.hero.heroT.Translate(Main.hero.maxSpeed * Vector3.right * 60f * Time.deltaTime);
			if (Main.hero.heroT.position.x > 134f)
			{
				Singleton<ManagerFunctions>.use.removeFunction(FinalRun);
				Singleton<LoaderBg>.use.SetLoader();
				Singleton<Saves>.use.keep("level4", 1);
				Main.location = 4;
				Singleton<Saves>.use.SaveData();
				Main.game.InvokeNewLoc();
			}
		}
		else if (Main.hero.CanUse())
		{
			Main.hero.rb.isKinematic = true;
			Main.hero.rb.velocity = Vector2.zero;
			Main.hero.stopActions = true;
			Main.hero.SetAn("Run");
			Main.game.cam.BreakFollow();
			catchHero = true;
		}
	}
}
