using UnityEngine;

public class ControlsUI : MonoBehaviour, GameObjInterface
{
	public GameObject controls1;

	public GameObject controls2;

	private void Start()
	{
		Singleton<InputChecker>.use.AddObj(this);
		SetIcon();
	}

	public void DoActionOnPos()
	{
	}

	public void DoAction()
	{
	}

	public void SetSprite(Sprite n)
	{
		SetIcon();
	}

	public void SetIcon()
	{
		controls1.SetActive(value: false);
		controls2.SetActive(value: false);
		if (Singleton<InputChecker>.use.currentIcon == 1)
		{
			controls1.SetActive(value: true);
		}
		else
		{
			controls2.SetActive(value: true);
		}
	}
}
