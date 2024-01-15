using UnityEngine;

public class MushroomBoom : MonoBehaviour, InterfaceBreakObj
{
	public MonsterUnder monster;

	public Tree tree;

	public ParticleSystem pr;

	private void Start()
	{
		Main.game.arrBreakObj.Add(this);
	}

	public void BreakObj()
	{
		pr.Stop();
		base.gameObject.SetActive(value: true);
	}

	private void OnTriggerEnter2D(Collider2D n)
	{
		if ((n.gameObject.CompareTag("Hero") || n.gameObject.CompareTag("Monster")) && base.gameObject.activeSelf)
		{
			pr.Play();
			base.gameObject.SetActive(value: false);
			if (monster.isSleep)
			{
				Singleton<Sounds>.use.So("wolf_eat_zvuk");
			}
			Singleton<Sounds>.use.So2("mushBoom", 0.5f);
			monster.WakeUpMonster();
			if (Mathf.Abs(base.transform.position.x - tree.gameObject.transform.position.x) < 3f)
			{
				tree.SetLeafs(n: true);
			}
		}
	}
}
