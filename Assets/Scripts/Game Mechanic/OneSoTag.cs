using UnityEngine;

public class OneSoTag : MonoBehaviour
{
	public string nameSound;

	public string tagObject;

	private bool onlyOne = true;

	private void OnTriggerEnter2D(Collider2D n)
	{
		if (n.gameObject.CompareTag(tagObject) && onlyOne)
		{
			onlyOne = false;
			Singleton<Sounds>.use.So(nameSound);
			Object.Destroy(base.gameObject);
		}
	}
}
