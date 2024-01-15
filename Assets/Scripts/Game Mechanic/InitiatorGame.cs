using UnityEngine;

public class InitiatorGame : ScriptableObject
{
	public void Init()
	{
		Main.InitMain();
		GameObject obj = Object.Instantiate(Resources.Load("Prefabs/Main/SteamF")) as GameObject;
		obj.GetComponent<SteamF>().Init();
		Object.DontDestroyOnLoad(obj);
		GameObject obj2 = Object.Instantiate(Resources.Load("Prefabs/Main/Saves")) as GameObject;
		obj2.GetComponent<Saves>().InitSave();
		Object.DontDestroyOnLoad(obj2);
		GameObject obj3 = Object.Instantiate(Resources.Load("Prefabs/Main/FRun")) as GameObject;
		obj3.GetComponent<ManagerFunctions>().Init();
		obj3.GetComponent<InputChecker>().Init();
		Object.DontDestroyOnLoad(obj3);
		GameObject obj4 = Object.Instantiate(Resources.Load("Prefabs/Main/SFX")) as GameObject;
		obj4.GetComponent<Sounds>().Init();
		Object.DontDestroyOnLoad(obj4);
		GameObject obj5 = Object.Instantiate(Resources.Load("Prefabs/Main/Music")) as GameObject;
		obj5.GetComponent<Music>().Init();
		Object.DontDestroyOnLoad(obj5);
		GameObject obj6 = Object.Instantiate(Resources.Load("Prefabs/Main/CanvasGlobal")) as GameObject;
		Main.canvasT = obj6.transform;
		obj6.transform.GetChild(0).gameObject.GetComponent<LoaderBg>().Init();
		Object.DontDestroyOnLoad(obj6);
		Main.managerDone = true;
		Main.typeLanguage = Singleton<Saves>.use.saveS("languageGame");
	}
}
