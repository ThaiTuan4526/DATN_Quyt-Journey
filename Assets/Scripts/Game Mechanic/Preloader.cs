using UnityEngine;
using UnityEngine.SceneManagement;

public class Preloader : Singleton<Preloader>
{
	private void Awake()
	{
		if (!Main.managerDone)
		{
			ScriptableObject.CreateInstance<InitiatorGame>().Init();
		}
	}

	private void Start()
	{
		Main.SetCanvas();
		SceneManager.LoadScene("Logo");
	}
}
