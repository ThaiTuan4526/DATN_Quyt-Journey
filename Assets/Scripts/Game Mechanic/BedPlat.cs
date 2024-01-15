using UnityEngine;

public class BedPlat : MonoBehaviour, InterfaceBreakObj
{
	public Transform[] plats;

	private Transform heroT;

	private Hero heroS;

	private int nOf;

	private BoxCollider2D[] colliderLeaf;

	private void Start()
	{
		Invoke("Init", 0.1f);
	}

	public void BreakObj()
	{
		Singleton<ManagerFunctions>.use.addFunction(CheckDown);
	}

	private void Init()
	{
		Main.game.arrBreakObj.Add(this);
		heroT = Main.hero.heroT;
		heroS = Main.hero;
		nOf = plats.Length;
		colliderLeaf = new BoxCollider2D[nOf];
		for (int i = 0; i < nOf; i++)
		{
			colliderLeaf[i] = plats[i].gameObject.GetComponent<BoxCollider2D>();
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
