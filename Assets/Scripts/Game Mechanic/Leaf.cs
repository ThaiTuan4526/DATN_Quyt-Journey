using UnityEngine;

public class Leaf : MonoBehaviour
{
	public Rigidbody2D rb;

	private void OnTriggerEnter2D(Collider2D n)
	{
		if ((n.gameObject.CompareTag("Hero") || n.gameObject.CompareTag("Bro")) && rb.velocity.magnitude < 0.2f)
		{
			float num = 0.005f + Random.value * 0.005f;
			if (n.gameObject.transform.position.x > rb.gameObject.transform.position.x)
			{
				num *= -1f;
			}
			rb.AddForce(new Vector2(num, 0.021f + Random.value * 0.02f), ForceMode2D.Impulse);
			rb.AddTorque(8E-05f + Random.value * 0.0005f, ForceMode2D.Impulse);
		}
	}
}
