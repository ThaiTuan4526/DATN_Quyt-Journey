using UnityEngine;

public class GremlinFall : MonoBehaviour
{
	public MonsterUnder monsterUnder;

	private void OnTriggerEnter2D(Collider2D n)
	{
		if (n.gameObject.CompareTag("Monster"))
		{
			monsterUnder.LeafFall();
		}
	}
}
