using UnityEngine;

public class ButtonFullScreen : MonoBehaviour, InterfaceBT
{
	public Menu menu;

	public void DoAction()
	{
		menu.SwitchFull();
	}
}
