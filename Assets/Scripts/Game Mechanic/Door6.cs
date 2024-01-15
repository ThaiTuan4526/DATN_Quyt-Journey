using UnityEngine;

public class Door6 : MonoBehaviour
{
	public bool leftSide;

	private float speed;

	private void OnTriggerEnter2D(Collider2D n)
	{
		if (!Main.hero.stopActions && n.gameObject.CompareTag("Hero"))
		{
			if (leftSide)
			{
				Main.hero.heroT.position = new Vector3(-6.24f, -2.533f, 0f);
				Main.hero.SetDirect(n: true);
				speed = 0.03f;
			}
			else
			{
				Main.hero.heroT.position = new Vector3(6.24f, -2.533f, 0f);
				Main.hero.SetDirect(n: false);
				speed = -0.03f;
			}
			Main.hero.rb.velocity = Vector2.zero;
			Main.hero.jump = false;
			Main.hero.fall = false;
			Main.hero.SetStepsNames(5);
			Main.game.ShowScene("sleephome");
			Singleton<Music>.use.MusicStart("treeHouseMusic");
			Main.game.cam.BreakFollow();
			Main.game.cam.cameraT.position = new Vector3(0f, 0f, -10f);
			Main.game.cam.SetSize(4.9f);
			Main.hero.sp.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
			Main.hero.stopActions = true;
			Main.hero.SetAn("Run");
			Singleton<ManagerFunctions>.use.addFunction(GoTo);
		}
	}

	private void GoTo()
	{
		Main.hero.heroT.position = new Vector2(Main.hero.heroT.position.x + speed * 60f * Time.deltaTime, Main.hero.heroT.position.y);
		if ((Main.hero.direct && Main.hero.heroT.position.x >= -4.4f) || (!Main.hero.direct && Main.hero.heroT.position.x <= 4.4f))
		{
			Main.hero.ReturnToGame();
			Singleton<ManagerFunctions>.use.removeFunction(GoTo);
		}
	}
}
