using UnityEngine;

public class Crow : MonoBehaviour
{
	public Animator an;

	private float waitTime;

	private bool isAn;

	private void Update()
	{
		if (isAn)
		{
			if (an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
			{
				isAn = false;
				waitTime = 0f;
			}
			return;
		}
		waitTime += Time.deltaTime;
		if (waitTime > 5f)
		{
			isAn = true;
			an.Play("crow", -1, 0f);
		}
	}
}
