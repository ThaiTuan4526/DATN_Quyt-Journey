using UnityEngine;

public class WakeUpLev6 : MonoBehaviour
{
	public MonsterAmber monsterAmber;

	private void OnTriggerStay2D(Collider2D n)
	{
		if (n.gameObject.CompareTag("Hero") && Main.hero.CanUse())
		{
			Singleton<Sounds>.use.So("breanchCrack");
			monsterAmber.WakeUp();
			Object.Destroy(base.gameObject);
		}
	}
}
