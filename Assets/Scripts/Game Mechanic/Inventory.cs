using System.Collections.Generic;
using UnityEngine;

public class Inventory : ScriptableObject
{
	public List<ItemHero> items = new List<ItemHero>();

	public void AddItem(Items t, int a = 0)
	{
		ItemHero component = (Object.Instantiate(Resources.Load("Prefabs/UI/ItemHero"), Main.game.canvasT, instantiateInWorldSpace: false) as GameObject).GetComponent<ItemHero>();
		component.Spawn(t, a);
		items.Add(component);
		SetItems();
	}

	public void RemoveItem(Items t)
	{
		for (int i = 0; i < items.Count; i++)
		{
			if (items[i].type == t)
			{
				Object.Destroy(items[i].gameObject);
				items.RemoveAt(i);
				break;
			}
		}
		SetItems();
	}

	public void RemoveAllItems()
	{
		for (int i = 0; i < items.Count; i++)
		{
			Object.Destroy(items[i].gameObject);
		}
		items.Clear();
	}

	public bool HaveItem(Items t)
	{
		foreach (ItemHero item in items)
		{
			if (item.type == t)
			{
				return true;
			}
		}
		return false;
	}

	public void SetItems()
	{
		for (int i = 0; i < items.Count; i++)
		{
			items[i].gameObject.transform.localPosition = new Vector3(Main.rCornerX - (float)(i * 150), Main.downCornerY);
		}
	}
}
