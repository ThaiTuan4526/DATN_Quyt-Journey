using UnityEngine;

public class ButtonsResume : MonoBehaviour, InterfaceBT
{
	public PauseTab pause;

	public void DoAction()
	{
		pause.SetMode(n: false);
		Singleton<Sounds>.use.So2("click3", 0.5f);
	}
}
