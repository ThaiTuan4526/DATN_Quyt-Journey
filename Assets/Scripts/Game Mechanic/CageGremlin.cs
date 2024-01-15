using UnityEngine;

public class CageGremlin : MonoBehaviour, InterfaceBreakObj
{
	public ButterflyBridge butterflyBridge;

	public MonsterBridge monsterBridge;

	public GameObject cage;

	private Animator an;

	private void Start()
	{
		an = base.gameObject.GetComponent<Animator>();
		an.Play("Idle");
		cage.SetActive(value: false);
		Main.game.arrBreakObj.Add(this);
	}

	public void BreakObj()
	{
		an.Play("Idle");
		cage.SetActive(value: false);
	}

	private void OnTriggerEnter2D(Collider2D n)
	{
		if (n.gameObject.CompareTag("Hero"))
		{
			if (butterflyBridge.goLoop)
			{
				butterflyBridge.GoTo();
				an.Play("Scream");
				Singleton<Sounds>.use.So2("gremlin2", 0.4f);
				cage.SetActive(value: true);
			}
			else if (!monsterBridge.dead)
			{
				an.Play("Scream");
				Singleton<Sounds>.use.So("gremlin2");
				monsterBridge.RunAgain();
			}
		}
	}
}
