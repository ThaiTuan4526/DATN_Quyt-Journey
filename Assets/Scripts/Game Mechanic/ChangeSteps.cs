using UnityEngine;

public class ChangeSteps : MonoBehaviour
{
	public int desiredSteps;

	private void OnTriggerEnter2D(Collider2D n)
	{
		if (n.gameObject.CompareTag("Hero"))
		{
			Main.hero.SetStepsNames(desiredSteps);
		}
	}
}
