using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenu : MonoBehaviour, InterfaceBT
{
	public GameObject pause;

	public void DoAction()
	{
		Singleton<Sounds>.use.So2("click3", 0.5f);
		Main.location = 0;
		Singleton<Sounds>.use.StopAllSounds();
		Singleton<ManagerFunctions>.use.ClearAll();
		Singleton<InputChecker>.use.ClearObj();
		pause.SetActive(value: false);
		Singleton<LoaderBg>.use.SetLoader();
		SceneManager.LoadScene("Menu");
	}
}
