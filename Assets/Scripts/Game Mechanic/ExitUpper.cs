using UnityEngine;

public class ExitUpper : MonoBehaviour
{
	public bool isOutside;

	public EnterTower enterT;

	public ExitTower exitT;

	private void OnTriggerEnter2D(Collider2D n)
	{
		if (isOutside)
		{
			enterT.DoA();
			Singleton<Sounds>.use.StopS();
			Main.hero.heroT.position = new Vector2(9.61f, 12.73f);
			Main.hero.SetDirect(n: false);
			Main.game.cam.SetPos(new Vector3(Main.hero.heroT.position.x - 1.5f, Main.hero.heroT.position.y, 0f));
		}
		else
		{
			exitT.DoA();
			Singleton<Sounds>.use.StopS();
			Main.hero.heroT.position = new Vector2(29.66f, 7.12f);
			Main.hero.SetDirect(n: true);
			Main.hero.sp.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
			Main.game.cam.SetPos(new Vector3(Main.hero.heroT.position.x + 1.5f, Main.hero.heroT.position.y, 0f));
			Main.game.cam.followY = true;
		}
	}
}
