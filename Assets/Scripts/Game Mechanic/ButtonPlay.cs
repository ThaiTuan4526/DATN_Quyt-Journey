using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPlay : MonoBehaviour, InterfaceBT
{
	public Menu menu;

	public void DoAction()
	{
		if (Singleton<Saves>.use.save("level2") == 1)
		{
			menu.SetButtons(2);
			Singleton<Sounds>.use.So2("click3", 0.5f);
			return;
		}
		Singleton<ManagerFunctions>.use.ClearAll();
		Singleton<InputChecker>.use.ClearObj();
		Singleton<LoaderBg>.use.SetLoader();
		Main.location = 1;
		SceneManager.LoadScene("Game");
	}
}
