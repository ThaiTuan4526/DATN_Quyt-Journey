using UnityEngine;

public class ExitTower : ActiveObj
{
	public bool tower1;

	public Monster7 monster7;

	public CutSceneLevel7 cutSceneLevel7;

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
		else if (Main.hero.CanUse())
		{
			Main.hero.goHero.GoTo(base.transform.position.x, this);
			Main.hero.rb.isKinematic = true;
			Main.hero.runAnyway = true;
		}
	}

	public override void DoActionOnPos()
	{
		Invoke("DoA", 0.1f);
	}

	public void DoA()
	{
		Singleton<Music>.use.MusicStart("forest");
		Singleton<Sounds>.use.So2("doorClose_3", 0.2f);
		Main.hero.ReturnToGame();
		if (tower1)
		{
			Main.hero.heroT.position = new Vector2(11.62f, -3.123f);
			monster7.BreakObj();
		}
		else
		{
			Main.hero.heroT.position = new Vector2(25.57f, -3.123f);
			Main.hero.sp.maskInteraction = SpriteMaskInteraction.None;
		}
		Main.game.SetDarkAlpha(0.65f);
		Main.hero.SetDirect(n: true);
		Main.hero.SetStepsNames(4);
		Main.game.ShowScene("castle");
		Main.game.cam.maxDownY = -50f;
		Main.game.cam.maxUpY = 50f;
		Main.game.cam.followY = false;
		Main.game.cam.isFollow = true;
		Main.game.cam.directX = Main.game.cam.maxX;
		Main.game.cam.SetDefaultSize();
		Main.game.cam.SwitchOffsetY(0f);
		Main.game.cam.calculateSmoothPos = Vector3.zero;
		Main.game.cam.SetPos(new Vector3(Main.hero.heroT.position.x + 1.5f, -0.5f, 0f));
		Main.game.cam.SetBorders(new Vector2(0f, 100f));
		if (!tower1 && Main.game.inv.HaveItem(Items.bell))
		{
			cutSceneLevel7.StartCutScene();
		}
	}
}
