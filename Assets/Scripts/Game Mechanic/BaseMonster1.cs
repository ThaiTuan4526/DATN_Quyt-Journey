using UnityEngine;

public class BaseMonster1 : NPCBase
{
	internal float waitTime;

	protected int status;

	protected void Init()
	{
		Vector2 vector = new Vector2(0.07f, 0.092f);
		posAn.Add("Idle", new Vector2(-0.013f, 0f) + vector);
		posAn.Add("Run", new Vector2(0f, 0f) + vector);
		posAn.Add("Chew", new Vector2(0f, 0f) + vector);
		posAn.Add("See", new Vector2(0.045f, 0f) + vector);
		posAn.Add("Attack", new Vector2(0.259f, -0.014f) + vector);
		posAn.Add("Seat", new Vector2(-0.496f, 0.372f) + vector);
		posAn.Add("Eat", new Vector2(0.011f, 0.04f) + vector);
		InitMain();
		base.gameObject.SetActive(value: false);
	}
}
