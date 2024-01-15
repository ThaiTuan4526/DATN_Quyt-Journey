using UnityEngine;

public class StartRunLev4 : MonoBehaviour
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
			waitTime += Time.deltaTime;
			if (waitTime > 0.1f)
			{
				status = 2;
				Main.hero.SetAn("Run");
			}
			return;
		}
		Main.hero.heroT.Translate(48f * Main.hero.maxSpeed * Vector3.right * Time.deltaTime);
		if (Main.hero.heroT.position.x >= -6.91f)
		{
			Main.hero.ReturnToGame();
			Main.hero.SetAn(Main.hero.idleAn);
			Object.Destroy(base.gameObject);
		}
	}
}
