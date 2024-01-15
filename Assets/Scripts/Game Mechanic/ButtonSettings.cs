using UnityEngine;

public class ButtonSettings : MonoBehaviour, InterfaceBT
{
	public Menu menu;

	public void DoAction()
	{
		menu.SetButtons(3);
		Singleton<Sounds>.use.So2("click3", 0.5f);
	}
}
