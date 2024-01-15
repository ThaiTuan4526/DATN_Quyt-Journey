using UnityEngine;

public class StonePlat : MonoBehaviour
{
	public GameObject platform;

	public Animator an;

	private void Start()
	{
		an.keepAnimatorControllerStateOnDisable = true;
	}

	public void SetShow()
	{
		platform.SetActive(value: true);
		an.Play("Show");
	}

	public void SetHide()
	{
		platform.SetActive(value: false);
		an.Play("Idle");
	}

	public void ToShow()
	{
		platform.SetActive(value: true);
		an.Play("Open");
	}

	public void ToHide()
	{
		platform.SetActive(value: false);
		an.Play("Back");
	}
}
