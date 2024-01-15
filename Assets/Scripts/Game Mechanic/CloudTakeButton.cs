using UnityEngine;

public class CloudTakeButton : MonoBehaviour
{
	public Animator an;

	private float timeWait;

	private int status = 1;

	private void Update()
	{
		if (status == 1)
		{
			timeWait += Time.deltaTime;
			if (timeWait > 2f)
			{
				timeWait = 0f;
				status = 2;
				an.Play("DreamBro");
			}
		}
		else if (status == 2)
		{
			timeWait += Time.deltaTime;
			if (timeWait > 3f)
			{
				timeWait = 0f;
				Main.hero.ReturnToGame();
				Object.Destroy(base.gameObject);
			}
		}
	}
}
