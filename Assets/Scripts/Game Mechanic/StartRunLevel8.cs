using UnityEngine;

public class StartRunLevel8 : MonoBehaviour
{
	private float waitTime;

	private int status = 1;

	private void Update()
	{
		if (!Main.game.canShowPauseTab)
		{
			return;
		}
		if (status == 1)
		{
			if ((waitTime += 1f) > 2f)
			{
				status = 2;
				Main.hero.SetAn("Run");
			}
			return;
		}
		Main.hero.heroT.Translate(0.8f * Main.hero.maxSpeed * Vector3.right * 60f * Time.deltaTime);
		if (Main.hero.heroT.position.x >= -8.46f)
		{
			Main.hero.sp.maskInteraction = SpriteMaskInteraction.None;
			Main.hero.ReturnToGame();
			Main.hero.SetAn(Main.hero.idleAn);
			Object.Destroy(base.gameObject);
		}
	}
}
