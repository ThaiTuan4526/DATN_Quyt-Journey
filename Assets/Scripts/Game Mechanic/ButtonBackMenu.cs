using UnityEngine;

public class ButtonBackMenu : MonoBehaviour, InterfaceBT
{
	public Menu menu;

	public void DoAction()
	{
		menu.SetButtons(1);
		Singleton<Sounds>.use.So2("click3", 0.5f);
	}
}
