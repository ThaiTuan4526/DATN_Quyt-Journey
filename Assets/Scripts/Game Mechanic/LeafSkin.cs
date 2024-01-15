using UnityEngine;

public class LeafSkin : MonoBehaviour
{
	public SpriteRenderer skin;

	public bool isR;

	private Sprite leaf1;

	private Sprite leaf2;

	private void Start()
	{
		leaf1 = Resources.Load<Sprite>("Sprites/Other/leafL3_2");
		leaf2 = Resources.Load<Sprite>("Sprites/Other/leafL3");
	}

	public void SetSkin(bool n)
	{
		if (n)
		{
			skin.sprite = leaf1;
			if (isR)
			{
				skin.gameObject.transform.localPosition = new Vector3(0.952f, skin.gameObject.transform.localPosition.y, 0f);
			}
			else
			{
				skin.gameObject.transform.localPosition = new Vector3(0.796f, skin.gameObject.transform.localPosition.y, 0f);
			}
		}
		else
		{
			skin.sprite = leaf2;
			skin.gameObject.transform.localPosition = new Vector3(0.868f, skin.gameObject.transform.localPosition.y, 0f);
		}
	}

	private void OnCollisionEnter2D(Collision2D n)
	{
		if (n.gameObject.CompareTag("Hero"))
		{
			Singleton<Sounds>.use.CanTimeSoundLong("leafGo");
		}
	}
}
