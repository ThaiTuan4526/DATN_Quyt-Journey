using UnityEngine;

public class GameObjOverMonster : MonoBehaviour
{
	public GoodMonster goodMonster;

	private void OnTriggerEnter2D(Collider2D n)
	{
		n.gameObject.CompareTag("Monster");
	}

	private void OnTriggerExit2D(Collider2D n)
	{
		n.gameObject.CompareTag("Monster");
	}
}
