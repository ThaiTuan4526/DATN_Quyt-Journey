using UnityEngine;

public class EnterTower : ActiveObj
{
	public bool tower1;

	public bool tower3;

	public KeyWhole keyWhole;

	private void Start()
	{
		Init();
	}

	public override void DoAction()
	{
		if (Mathf.Abs(base.transform.position.x - Main.hero.heroT.position.x) < 1f)
		{
			if (Main.hero.CanUse())
			{
				DoActionOnPos();
			}
		}
		else
		{
			Main.hero.goHero.GoTo(base.transform.position.x, this);
		}
	}

	public override void DoActionOnPos()
	{
		Invoke("DoA", 0.1f);
	}

	public void DoA()
	{
		if (tower3)
		{
			Singleton<Sounds>.use.So("lock");
			Main.hero.SetDirect(n: false);
			Main.hero.SetAn("View");
			Singleton<ManagerFunctions>.use.addFunction(RuntimeAction);
			return;
		}
		Main.hero.ReturnToGame();
		Singleton<Sounds>.use.So2("doorOpen_2", 0.4f);
		Main.hero.heroT.position = new Vector2(0f, -4.872f);
		if (tower1)
		{
			Singleton<Music>.use.MusicStart("shackMusic");
			Main.game.cam.followY = false;
			Main.game.ShowScene("library");
			Main.game.SetDarkAlpha(0.5f);
			Main.game.cam.SetBorders(new Vector2(0f, 0f));
			Main.game.cam.calculateSmoothPos.x = 0f;
			Main.game.cam.calculateSmoothPos.y = -0.68f;
		}
		else
		{
			Singleton<Music>.use.MusicStart("caveMusic");
			Main.game.ShowScene("chapel");
			Main.game.cam.SetBorders(new Vector2(-1f, 2f));
			Main.game.cam.calculateSmoothPos.x = 1.3f;
			Main.game.cam.calculateSmoothPos.y = 0f;
			Main.game.cam.maxUpY = 11f;
			Main.game.cam.maxDownY = 0f;
			Main.hero.sp.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
			Main.game.cam.followY = true;
			Main.game.cam.SetSize(7f);
			keyWhole.CheckShow();
		}
		Main.game.cam.cameraT.position = Main.game.cam.calculateSmoothPos;
		Main.game.cam.directX = Main.game.cam.maxX;
		Main.hero.SetStepsNames(4);
		Main.hero.SetDirect(n: true);
	}

	private void RuntimeAction()
	{
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(RuntimeAction);
			Main.hero.ReturnToGame();
		}
	}
}
