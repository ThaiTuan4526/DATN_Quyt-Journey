using UnityEngine;

public class ButtonQuit : MonoBehaviour, InterfaceBT
{
	public void DoAction()
	{
		Application.Quit();
	}
}
