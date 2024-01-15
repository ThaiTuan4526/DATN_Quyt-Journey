using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLevel : MonoBehaviour, InterfaceBT
{
	public int level;

	public void DoAction()
	{
		Singleton<Sounds>.use.So2("click3", 0.5f);
		Singleton<ManagerFunctions>.use.ClearAll();
		Singleton<InputChecker>.use.ClearObj();
		Singleton<LoaderBg>.use.SetLoader();
		Main.location = level;
		SceneManager.LoadScene("Game");
	}
}
