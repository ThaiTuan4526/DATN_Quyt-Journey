using UnityEngine;

public class StoneGame : MonoBehaviour
{
	internal PlatsGame platsGame;

	internal StonePlat platS;

	internal int id;

	public bool onPlat;

	private void Start()
	{
		onPlat = false;
	}

	public void BreakCombo()
	{
		onPlat = false;
		platS.ToHide();
	}

	private void OnTriggerStay2D(Collider2D n)
	{
		if (n.gameObject.CompareTag("Hero") && !onPlat && Main.hero.onPlatform && Main.hero.heroT.position.y + 0.15f > base.transform.position.y)
		{
			platsGame.SetPos(id);
			onPlat = true;
		}
	}
}
