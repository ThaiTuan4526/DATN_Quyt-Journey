using UnityEngine;

public class Door6_2 : MonoBehaviour
{
	public bool sideLeft;

	private float speed;

	private void OnTriggerEnter2D(Collider2D n)
	{
		if (!Main.hero.stopActions && n.gameObject.CompareTag("Hero"))
		{
			if (sideLeft)
			{
				Main.hero.heroT.position = new Vector3(29.78f, -0.872f, 0f);
				Main.hero.SetDirect(n: false);
				speed = -0.03f;
			}
			else
			{
				Main.hero.heroT.position = new Vector3(32.471f, -2f, 0f);
				Main.hero.SetDirect(n: true);
				speed = 0.03f;
			}
			Main.hero.sp.maskInteraction = SpriteMaskInteraction.None;
			Main.hero.rb.velocity = Vector2.zero;
			Main.hero.jump = false;
			Main.hero.fall = false;
			Main.hero.SetStepsNames(1);
			Main.game.ShowScene("trees");
			Singleton<Music>.use.MusicStart("forest");
			Main.game.cam.BreakFollow();
			Main.game.cam.ToFollow(Main.hero.heroT);
			Main.game.cam.SetDefaultSize();
			Main.hero.stopActions = true;
			Main.hero.SetAn("Run");
			if (sideLeft)
			{
				Main.game.cam.SetFollowPos(new Vector3(27.51467f, 1.99f, -10f));
			}
			else
			{
				Main.game.cam.SetFollowPos(new Vector3(35.12409f, 0.83f, -10f));
			}
			Singleton<ManagerFunctions>.use.addFunction(GoTo);
		}
	}

	private void GoTo()
	{
		Main.hero.heroT.position = new Vector2(Main.hero.heroT.position.x + speed * 60f * Time.deltaTime, Main.hero.heroT.position.y);
		if ((Main.hero.direct && Main.hero.heroT.position.x >= 33.8f) || (!Main.hero.direct && Main.hero.heroT.position.x <= 28.838f))
		{
			Main.hero.ReturnToGame();
			Singleton<ManagerFunctions>.use.removeFunction(GoTo);
		}
	}
}
