using UnityEngine;

public class FlowerBox : MonoBehaviour
{
	public HeroFloor floor;

	public SpriteRenderer flower;

	public bool isHide;

	private bool isOver;

	private void Start()
	{
		Singleton<ManagerFunctions>.use.addFunction(FlowerDepth);
	}

	private void FlowerDepth()
	{
		if (!Main.hero.gameObject.activeSelf || Main.hero.stopActions || floor.floor != 1 || !Main.hero.onPlatform || !(Main.game.currentSceneName == "library"))
		{
			return;
		}
		if (Main.hero.heroT.position.y > -4.748f)
		{
			if (!isOver)
			{
				flower.sortingLayerName = "overHero";
				isOver = true;
				isHide = true;
				Singleton<Sounds>.use.So("hideFlower");
			}
		}
		else if (isOver)
		{
			flower.sortingLayerName = "gameobj";
			isOver = false;
			isHide = false;
		}
	}
}
