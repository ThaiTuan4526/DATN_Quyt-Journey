using UnityEngine;

public class CloudAn : MonoBehaviour
{
	public Animator an;

	private void Update()
	{
		if (an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
		{
			Object.Destroy(base.gameObject);
		}
	}
}
