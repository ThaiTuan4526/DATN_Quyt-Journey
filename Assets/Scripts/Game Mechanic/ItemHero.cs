using UnityEngine;
using UnityEngine.UI;

public class ItemHero : MonoBehaviour
{
	public Items type;

	public Image icon;

	public void Spawn(Items t, int a = 0)
	{
		type = t;
		if (type == Items.mushwitch)
		{
			if (a < 10)
			{
				icon.sprite = Resources.Load<Sprite>("Sprites/Items/Mushs/mushwitch000" + a);
			}
			else
			{
				icon.sprite = Resources.Load<Sprite>("Sprites/Items/Mushs/mushwitch00" + a);
			}
			icon.SetNativeSize();
		}
		else
		{
			icon.sprite = Resources.Load<Sprite>("Sprites/Items/" + type);
			icon.SetNativeSize();
		}
	}
}
