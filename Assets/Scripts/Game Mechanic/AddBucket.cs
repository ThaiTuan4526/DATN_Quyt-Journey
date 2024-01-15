using UnityEngine;

public class AddBucket : ActiveObj
{
	public GameObject bucket;

	public GameObject b1;

	public GameObject b2;

	private bool actionDone;

	private bool addedBucket;

	private bool addedWater;

	private void Start()
	{
		Init();
		bucket.SetActive(value: false);
		addedWater = false;
		b2.SetActive(value: false);
		b1.SetActive(value: true);
	}

	public void AddWater()
	{
		if (addedBucket)
		{
			addedWater = true;
			b1.SetActive(value: false);
			b2.SetActive(value: true);
			Singleton<Sounds>.use.So("wellB");
		}
	}

	public override void DoAction()
	{
		if (Main.game.inv.HaveItem(Items.bucket) || addedBucket)
		{
			Main.hero.goHero.GoTo(base.transform.position.x, this);
		}
	}

	public override void DoActionOnPos()
	{
		if (Main.game.inv.HaveItem(Items.bucket))
		{
			Main.hero.SetDirect(n: true);
			Main.hero.SetAn("Take2");
			actionDone = false;
			Singleton<ManagerFunctions>.use.addFunction(AddB);
		}
		else if (addedBucket)
		{
			Main.hero.SetDirect(n: true);
			Main.hero.SetAn("Take2");
			actionDone = false;
			Singleton<ManagerFunctions>.use.addFunction(RemoveB);
		}
	}

	private void AddB()
	{
		if (!actionDone && Main.hero.AnFrame(0.4f))
		{
			actionDone = true;
			addedWater = false;
			Main.game.inv.RemoveItem(Items.bucket);
			b2.SetActive(value: false);
			b1.SetActive(value: true);
			bucket.SetActive(value: true);
			addedBucket = true;
			Singleton<Sounds>.use.So2("Setbucket", 0.7f);
		}
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(AddB);
			Main.hero.ReturnToGame();
		}
	}

	private void RemoveB()
	{
		if (!actionDone && Main.hero.AnFrame(0.4f))
		{
			actionDone = true;
			if (addedWater)
			{
				Main.game.inv.AddItem(Items.bucketwater);
				Singleton<Sounds>.use.So2("takeBucket", 0.5f);
			}
			else
			{
				Main.game.inv.AddItem(Items.bucket);
				Singleton<Sounds>.use.So2("TakeBucketClear", 0.7f);
			}
			bucket.SetActive(value: false);
			addedBucket = false;
		}
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(RemoveB);
			Main.hero.ReturnToGame();
		}
	}
}
