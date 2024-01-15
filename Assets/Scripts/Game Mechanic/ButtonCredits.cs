using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonCredits : MonoBehaviour, InterfaceBT
{
	public void DoAction()
	{
		Singleton<Sounds>.use.So2("click3", 0.5f);
		Singleton<ManagerFunctions>.use.ClearAll();
		Singleton<InputChecker>.use.ClearObj();
		Singleton<LoaderBg>.use.SetLoader();
		SceneManager.LoadScene("Credits");
	}
}
