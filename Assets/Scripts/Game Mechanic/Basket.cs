using UnityEngine;

public class Basket : MonoBehaviour
{
	public Animator an;

	public GameObject basketFallen;

	public void ToFall()
	{
		an.Play("BasketFall");
		Singleton<ManagerFunctions>.use.addFunction(Fall);
	}

	private void Fall()
	{
		if (an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
		{
			basketFallen.SetActive(value: true);
			Singleton<ManagerFunctions>.use.removeFunction(Fall);
			an.gameObject.SetActive(value: false);
		}
	}
}
