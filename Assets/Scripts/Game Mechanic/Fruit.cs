using UnityEngine;

public class Fruit : MonoBehaviour
{
	public Animator an;

	internal EvilTree tree;

	private int state = 1;

	public void CheckPipe()
	{
		if (Vector2.Distance(Main.hero.heroT.position, base.transform.position) < 2f)
		{
			an.Play("fruit" + state);
			if (state == 1)
			{
				Singleton<Sounds>.use.So("growMush");
			}
			else if (state == 2)
			{
				Singleton<Sounds>.use.So("deadFruit");
			}
			state++;
			if (state > 3)
			{
				state = 1;
			}
			tree.SetFruit();
		}
	}

	private void OnEnable()
	{
		if (state > 1)
		{
			an.Play("fruit" + (state - 1), -1, 1f);
		}
	}

	public bool IsGood()
	{
		if (state == 3)
		{
			return true;
		}
		return false;
	}
}
