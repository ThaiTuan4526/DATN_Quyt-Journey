using UnityEngine;

public class PlatShowY : MonoBehaviour
{
	private Transform platsObj;

	private Transform[] plats;

	private Transform heroT;

	private Hero heroS;

	private int nOf;

	private BoxCollider2D[] colliderLeaf;

	private void Start()
	{
		Invoke("Init", 0.1f);
	}

	private void Init()
	{
		heroT = Main.hero.heroT;
		heroS = Main.hero;
		platsObj = base.gameObject.transform;
		nOf = platsObj.childCount;
		colliderLeaf = new BoxCollider2D[nOf];
		plats = new Transform[nOf];
		for (int i = 0; i < nOf; i++)
		{
			plats[i] = platsObj.GetChild(i).gameObject.transform;
			colliderLeaf[i] = platsObj.GetChild(i).gameObject.GetComponent<BoxCollider2D>();
		}
		Singleton<ManagerFunctions>.use.addFunction(CheckDown);
	}

	private void CheckDown()
	{
		float num = heroT.position.y + 0.2f;
		if (heroS.jump)
		{
			return;
		}
		if (heroS.onPlatform)
		{
			for (int i = 0; i < nOf; i++)
			{
				if (plats[i].position.y > num)
				{
					colliderLeaf[i].enabled = false;
				}
			}
			return;
		}
		for (int j = 0; j < nOf; j++)
		{
			if (plats[j].position.y < num)
			{
				colliderLeaf[j].enabled = true;
			}
		}
	}
}
