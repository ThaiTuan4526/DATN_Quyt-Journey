using UnityEngine;

public class ButtonTips : MonoBehaviour, InterfaceBT
{
	public Menu menu;

	public void DoAction()
	{
		menu.SwitchTips();
	}
}
