using UnityEngine;

public class ChangeLevelStep : MonoBehaviour
{
	public int step;

	private void OnTriggerEnter2D(Collider2D n)
	{
		if (n.gameObject.CompareTag("Hero"))
		{
			Main.locationStep = step;
			Object.Destroy(base.gameObject);
		}
	}
}
