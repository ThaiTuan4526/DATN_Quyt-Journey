using UnityEngine;

public class ButtonUI : MonoBehaviour
{
	private GameObject chose;

	private bool isActive;

	private InterfaceBT bt;

	public void Init()
	{
		chose = base.gameObject.transform.GetChild(0).gameObject;
		bt = base.gameObject.GetComponent<InterfaceBT>();
	}

	public void SetMode(bool n)
	{
		isActive = n;
		chose.SetActive(n);
	}

	private void Update()
	{
		if (isActive && Input.GetButtonDown("Submit"))
		{
			bt.DoAction();
		}
	}
}
