using UnityEngine;

public class ShowSpirits : MonoBehaviour
{
	public Spirit[] spirits;

	private void OnTriggerStay2D(Collider2D n)
	{
		if (n.gameObject.CompareTag("Hero"))
		{
			Spirit[] array = spirits;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].ShowSpirit();
			}
			Object.Destroy(base.gameObject);
		}
	}
}
