using UnityEngine;

public class PhysicsLevel : MonoBehaviour
{
	public bool testMode;

	private void Start()
	{
		if (testMode)
		{
			return;
		}
		int childCount = base.gameObject.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = base.transform.GetChild(i).gameObject;
			int childCount2 = gameObject.transform.childCount;
			if (gameObject.HasComponent<SpriteRenderer>())
			{
				gameObject.GetComponent<SpriteRenderer>().enabled = false;
			}
			for (int j = 0; j < childCount2; j++)
			{
				GameObject gameObject2 = gameObject.transform.GetChild(j).gameObject;
				if (gameObject2.HasComponent<SpriteRenderer>())
				{
					gameObject2.GetComponent<SpriteRenderer>().enabled = false;
				}
			}
		}
	}
}
