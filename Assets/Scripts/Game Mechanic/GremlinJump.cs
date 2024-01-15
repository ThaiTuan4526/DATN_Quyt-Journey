using UnityEngine;

public class GremlinJump : MonoBehaviour
{
	public MonsterUnder monsterUnder;

	private void OnTriggerEnter2D(Collider2D n)
	{
		if (n.gameObject.CompareTag("Monster"))
		{
			monsterUnder.SetJump();
		}
	}
}
