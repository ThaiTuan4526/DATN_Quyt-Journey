using UnityEngine;

public class KeyOnWall : MonoBehaviour
{
	private bool haveLink;

	public GameObject key2;

	private void OnTriggerEnter2D(Collider2D n)
	{
		if (n.gameObject.CompareTag("Hero"))
		{
			haveLink = true;
		}
	}

	private void OnTriggerExit2D(Collider2D n)
	{
		if (n.gameObject.CompareTag("Hero"))
		{
			haveLink = false;
		}
	}

	private void Update()
	{
		if (haveLink && Input.GetButtonDown("Action"))
		{
			Singleton<Sounds>.use.So("key");
			Main.game.inv.AddItem(Items.key1);
			base.gameObject.SetActive(value: false);
			key2.SetActive(value: false);
		}
	}
}
