using UnityEngine;

public class GremlinInTrash : MonoBehaviour
{
	private bool goDown;

	private bool goUp;

	private bool idle;

	public void Init()
	{
		idle = true;
		Singleton<ManagerFunctions>.use.addFunction(Actions);
	}

	private void Actions()
	{
		if (idle)
		{
			if (base.transform.position.y > -8.478f)
			{
				if (Mathf.Abs(base.transform.position.x - Main.hero.heroT.position.x) < 4f)
				{
					goDown = true;
					idle = false;
					Singleton<SteamF>.use.AddAch(4);
				}
			}
			else if (Mathf.Abs(base.transform.position.x - Main.hero.heroT.position.x) > 5f)
			{
				goUp = true;
				idle = false;
			}
		}
		else if (goDown)
		{
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y - 0.05f, 0f);
			if (base.transform.position.y < -8.478f)
			{
				goDown = false;
				idle = true;
			}
		}
		else if (goUp)
		{
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 0.01f, 0f);
			if (base.transform.position.y > -7.947f)
			{
				goUp = false;
				idle = true;
			}
		}
	}
}
