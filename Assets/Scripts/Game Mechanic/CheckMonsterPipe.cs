using UnityEngine;

public class CheckMonsterPipe : MonoBehaviour
{
	public MonsterAmberOnTree monsterAmberOnTree;

	public bool CheckPipe(bool n)
	{
		if (Mathf.Abs(Main.hero.heroT.position.x - base.transform.position.x) < 3f && Mathf.Abs(Main.hero.heroT.position.y - base.transform.position.y) < 1f)
		{
			return monsterAmberOnTree.SetAction(n);
		}
		return false;
	}
}
