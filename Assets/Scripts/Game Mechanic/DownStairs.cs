using UnityEngine;

public class DownStairs : MonoBehaviour
{
	private int status = 1;

	private float waitTime;

	private bool soundGo = true;

	private void Update()
	{
		waitTime += Time.deltaTime;
		if (soundGo && waitTime >= 0.25f)
		{
			Singleton<Sounds>.use.So("ladderDown");
			soundGo = false;
		}
		if (!(waitTime > 0.15f))
		{
			return;
		}
		if (status == 1)
		{
			Main.hero.heroT.Translate(1.3f * Vector3.down * Time.deltaTime);
			if (Main.hero.heroT.position.y < 0.49f)
			{
				status = 2;
				Main.hero.SetAn("DownStairs");
				Main.hero.heroT.position = new Vector2(Main.hero.heroT.position.x, 0.49f);
			}
		}
		else if (status == 2 && Main.hero.AnCompleted())
		{
			Main.hero.heroT.Translate(0.182f * Vector3.right);
			Main.hero.ReturnToGame();
			Main.hero.SetDirect(n: true);
			Main.hero.SetAn(Main.hero.idleAn);
			Object.Destroy(base.gameObject);
		}
	}
}
