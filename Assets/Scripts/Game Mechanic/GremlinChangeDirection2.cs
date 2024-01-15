using UnityEngine;

public class GremlinChangeDirection2 : MonoBehaviour
{
	public MonsterUnder monsterUnder;

	private void OnTriggerEnter2D(Collider2D n)
	{
		if (n.gameObject.CompareTag("Monster"))
		{
			monsterUnder.SetDirectRun(n: true);
		}
	}
}
