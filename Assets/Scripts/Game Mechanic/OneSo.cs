using UnityEngine;

public class OneSo : MonoBehaviour
{
	public string nameSound;

	public float volumeSound = 1f;

	private void OnTriggerEnter2D(Collider2D n)
	{
		if (n.gameObject.CompareTag("Hero"))
		{
			Singleton<Sounds>.use.So2(nameSound, volumeSound);
			Object.Destroy(base.gameObject);
		}
	}
}
