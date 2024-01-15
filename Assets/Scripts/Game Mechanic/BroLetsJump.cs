using UnityEngine;

public class BroLetsJump : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D n)
	{
		if (n.gameObject.CompareTag("Bro"))
		{
			Main.bro.SetJump();
		}
	}
}
