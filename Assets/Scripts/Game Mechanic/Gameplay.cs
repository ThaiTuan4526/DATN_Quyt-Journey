using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
	[Header("Cài đặt kiểm tra")]
	public bool testMode;

	public int currentLevel;

	public bool CamToHero;

	public bool ToFollow;

	public GameObject level;

	public GameObject level2;

	public GameObject level3;

	public int choseScene;

	[Header("iên kết chính")]
	public Hero hero;

	public CameraGame cam;

	public Transform canvasT;

	internal Inventory inv;

	private PauseTab pause;

	private GameObject ns;

	private GameObject levelMain;

	private Image darkImage;

	internal GameObject activeScene;

	internal bool canShowPauseTab;

	internal bool bgmoveEx;

	internal string currentSceneName;

	internal List<GameObject> objects = new List<GameObject>();

	public Dictionary<string, GameObject> scenes = new Dictionary<string, GameObject>();

	public List<InterfaceBreakObj> arrBreakObj = new List<InterfaceBreakObj>();

	private void Awake()
	{
		if (!Main.managerDone)
		{
			ScriptableObject.CreateInstance<InitiatorGame>().Init();
		}
		Main.game = this;
		inv = ScriptableObject.CreateInstance<Inventory>();
	}

	private void Start()
	{
		Main.SetCanvas();
		darkImage = Main.canvasT.gameObject.transform.GetChild(1).gameObject.GetComponent<Image>();
		Singleton<ManagerFunctions>.use.addIndieFunction(HotKeysRuntime);
		ns = Object.Instantiate(Resources.Load("Prefabs/UI/PauseTab"), canvasT, instantiateInWorldSpace: false) as GameObject;
		pause = ns.GetComponent<PauseTab>();
		pause.Init();
		canShowPauseTab = true;
		inv.SetItems();
		if (testMode)
		{
			Main.location = currentLevel;
		}
		hero.Init();
		cam.Init();
		if (CamToHero)
		{
			cam.SetPos(hero.heroT.position);
		}
		if (ToFollow)
		{
			cam.ToFollow(hero.heroT);
		}
		SetBorders();
		Cursor.visible = false;
		SetLocation(isNew: false);
	}

	private void SetLocation(bool isNew = true)
	{
		if (!testMode)
		{
			Singleton<ManagerFunctions>.use.ClearMain();
		}
		Main.locationStep = 1;
		bgmoveEx = false;
		SetBorders();
		foreach (KeyValuePair<string, GameObject> scene in scenes)
		{
			Object.Destroy(scene.Value);
		}
		foreach (GameObject @object in objects)
		{
			Object.Destroy(@object);
		}
		if (!testMode)
		{
			arrBreakObj.Clear();
		}
		scenes.Clear();
		objects.Clear();
		if (Main.location == 1)
		{
			SetLevel1();
		}
		if (Main.location == 2)
		{
			SetLevel2();
		}
		if (Main.location == 3)
		{
			SetLevel3();
		}
		if (Main.location == 4)
		{
			SetLevel4();
		}
		if (Main.location == 5)
		{
			SetLevel5();
		}
		if (Main.location == 6)
		{
			SetLevel6();
		}
		if (Main.location == 7)
		{
			SetLevel7();
		}
		if (Main.location == 8)
		{
			SetLevel8();
		}
		hero.InitData();
		if (isNew)
		{
			inv.RemoveAllItems();
		}
		Singleton<LoaderBg>.use.HideLoader();
		Singleton<ManagerFunctions>.use.isPause = false;
	}

	private void SetLevel1()
	{
		if (!testMode)
		{
			level = Object.Instantiate(Resources.Load("Prefabs/Levels/Level1")) as GameObject;
		}
		level.GetComponent<BGmove>().Init();
		level.transform.GetChild(0).gameObject.GetComponent<LeafsFall>().Init();
		scenes.Add("forest", level);
		activeScene = level;
		if (!testMode)
		{
			hero.heroT.position = new Vector2(-11.4f, -1.88f);
			hero.SetDirect(n: true);
			cam.cameraT.position = new Vector3(0f, 1.005755f, -10f);
			cam.BreakFollow();
			Singleton<Music>.use.MusicStop();
		}
		if (!testMode)
		{
			ns = Object.Instantiate(Resources.Load("Prefabs/Other/ChapterShow")) as GameObject;
		}
	}

	private void SetLevel2()
	{
		hero.SetRootLayer();
		hero.ReturnToGame();
		if (!testMode)
		{
			level = Object.Instantiate(Resources.Load("Prefabs/Levels/Level2")) as GameObject;
		}
		scenes.Add("shack", level);
		activeScene = level;
		if (!testMode)
		{
			hero.heroT.position = new Vector2(-5.54f, -3.117f);
			hero.SetDirect(n: true);
			cam.cameraT.position = new Vector3(0f, 0f, -10f);
		}
		cam.BreakFollow();
		Singleton<Music>.use.MusicStart("shackMusic");
	}

	private void SetLevel3()
	{
		hero.ReturnToGame();
		if (!testMode)
		{
			level = Object.Instantiate(Resources.Load("Prefabs/Levels/Level3")) as GameObject;
		}
		level.GetComponent<BGmove>().Init();
		level.transform.GetChild(0).gameObject.GetComponent<LeafsFall>().Init();
		scenes.Add("bridge", level);
		activeScene = level;
		cam.maxDownY = -7.5f;
		if (!testMode)
		{
			hero.heroT.position = new Vector2(-2.442f, -1.67f);
			hero.rb.isKinematic = true;
			hero.stopActions = true;
			hero.SetDirect(n: true);
			Main.hero.SetAn("Run");
			cam.cameraT.position = new Vector3(0f, 0.915f, -10f);
			cam.ToFollow(hero.heroT);
		}
		Singleton<Music>.use.MusicStart("forest");
	}

	private void SetLevel4()
	{
		hero.ReturnToGame();
		if (!testMode)
		{
			levelMain = Object.Instantiate(Resources.Load("Prefabs/Levels/Level4Head")) as GameObject;
			level = levelMain.transform.GetChild(0).gameObject;
			level2 = levelMain.transform.GetChild(1).gameObject;
			objects.Add(levelMain);
		}
		level.GetComponent<BGmove>().Init();
		level.transform.GetChild(0).gameObject.GetComponent<LeafsFall>().Init();
		scenes.Add("camp", level);
		activeScene = level;
		scenes.Add("camphouse", level2);
		level.SetActive(value: true);
		level2.SetActive(value: false);
		if (choseScene == 2)
		{
			level2.SetActive(value: true);
			level.SetActive(value: false);
			Main.game.cam.BreakFollow();
			Main.game.cam.cameraT.position = new Vector3(0f, 0f, -10f);
			Main.hero.heroT.position = new Vector3(5.67f, -3.607383f, 0f);
			Main.hero.SetDirect(n: false);
		}
		Singleton<Music>.use.MusicStop();
		if (!testMode)
		{
			cam.SwitchOffsetY(3f);
			hero.heroT.position = new Vector2(-10f, -1.9f);
			hero.rb.isKinematic = true;
			hero.stopActions = true;
			hero.SetDirect(n: true);
			cam.cameraT.position = new Vector3(0f, 1.004779f, -10f);
			cam.ToFollow(hero.heroT);
		}
		if (!testMode)
		{
			ns = Object.Instantiate(Resources.Load("Prefabs/Other/ChapterShow")) as GameObject;
		}
		else
		{
			Singleton<Music>.use.MusicStart("forest");
		}
	}

	private void SetLevel5()
	{
		hero.ReturnToGame();
		if (!testMode)
		{
			level = Object.Instantiate(Resources.Load("Prefabs/Levels/Level5")) as GameObject;
		}
		scenes.Add("treehouse", level);
		activeScene = level;
		cam.followY = false;
		if (!testMode)
		{
			hero.heroT.position = new Vector2(-8.372f, 5.26f);
			hero.rb.isKinematic = true;
			hero.stopActions = true;
			hero.SetDirect(n: false);
			Main.hero.SetAn("Stairs");
			cam.cameraT.position = new Vector3(0f, 0f, -10f);
			cam.ToFollow(hero.heroT);
		}
		Singleton<Music>.use.MusicStart("shackMusic");
	}

	private void SetLevel6()
	{
		hero.ReturnToGame();
		if (!testMode)
		{
			levelMain = Object.Instantiate(Resources.Load("Prefabs/Levels/Level6Head")) as GameObject;
			level = levelMain.transform.GetChild(0).gameObject;
			level2 = levelMain.transform.GetChild(1).gameObject;
			objects.Add(levelMain);
		}
		level.GetComponent<BGmove>().Init();
		level.transform.GetChild(0).gameObject.GetComponent<LeafsFall>().Init();
		scenes.Add("trees", level);
		activeScene = level;
		cam.followY = true;
		scenes.Add("sleephome", level2);
		level.SetActive(value: true);
		level2.SetActive(value: false);
		if (choseScene == 2)
		{
			level2.SetActive(value: true);
			level.SetActive(value: false);
			Main.game.cam.BreakFollow();
			Main.game.cam.cameraT.position = new Vector3(0f, 0f, -10f);
		}
		Singleton<Music>.use.MusicStart("forest");
		if (!testMode)
		{
			hero.heroT.position = new Vector2(-10.957f, -1.96f);
			hero.stopActions = true;
			hero.SetDirect(n: true);
			Main.hero.sp.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
			cam.SetPos(hero.heroT.position);
			cam.ToFollow(hero.heroT);
		}
	}

	private void SetLevel7()
	{
		hero.ReturnToGame();
		if (!testMode)
		{
			levelMain = Object.Instantiate(Resources.Load("Prefabs/Levels/Level7Head")) as GameObject;
			level = levelMain.transform.GetChild(0).gameObject;
			level2 = levelMain.transform.GetChild(1).gameObject;
			level3 = levelMain.transform.GetChild(2).gameObject;
			objects.Add(levelMain);
		}
		level.GetComponent<BGmove>().Init();
		level.transform.GetChild(0).gameObject.GetComponent<LeafsFall>().Init();
		scenes.Add("castle", level);
		activeScene = level;
		cam.followY = true;
		scenes.Add("library", level2);
		scenes.Add("chapel", level3);
		level.SetActive(value: true);
		level2.SetActive(value: false);
		level3.SetActive(value: false);
		currentSceneName = "castle";
		if (choseScene == 2)
		{
			level2.SetActive(value: true);
			level3.SetActive(value: false);
			level.SetActive(value: false);
			Main.game.cam.SetBorders(new Vector2(0f, 0f));
			Main.game.cam.followY = false;
			currentSceneName = "library";
			Singleton<Music>.use.MusicStart("shackMusic");
			Main.hero.SetStepsNames(4);
		}
		if (choseScene == 3)
		{
			level2.SetActive(value: false);
			level3.SetActive(value: true);
			level.SetActive(value: false);
			Main.game.cam.SetBorders(new Vector2(-1f, 2f));
			Main.game.cam.maxDownY = 0f;
			Main.game.cam.maxUpY = 12.57f;
			currentSceneName = "chapel";
			Singleton<Music>.use.MusicStart("caveMusic");
			Main.hero.sp.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
			Main.hero.SetStepsNames(4);
		}
		cam.followY = false;
		if (!testMode)
		{
			hero.heroT.position = new Vector2(-11f, -3.12f);
			hero.rb.isKinematic = true;
			hero.stopActions = true;
			hero.SetDirect(n: true);
			Main.hero.SetAn("Run");
			cam.cameraT.position = new Vector3(0f, -0.5f, -10f);
			cam.ToFollow(hero.heroT);
			choseScene = 1;
			Singleton<Music>.use.MusicStop();
		}
		if (!testMode)
		{
			ns = Object.Instantiate(Resources.Load("Prefabs/Other/ChapterShow")) as GameObject;
		}
		else if (choseScene == 1)
		{
			Singleton<Music>.use.MusicStart("forest");
		}
	}

	private void SetLevel8()
	{
		hero.ReturnToGame();
		if (!testMode)
		{
			level = Object.Instantiate(Resources.Load("Prefabs/Levels/Level8")) as GameObject;
		}
		scenes.Add("girltower", level);
		activeScene = level;
		cam.followY = false;
		if (!testMode)
		{
			hero.heroT.position = new Vector2(-10.16f, -1.987383f);
			hero.stopActions = true;
			hero.SetDirect(n: true);
			Main.hero.SetAn("Run");
			cam.cameraT.position = new Vector3(-0.5f, 2.36f, -10f);
			cam.ToFollow(hero.heroT);
			cam.followY = false;
			cam.isGoToPos = false;
			Main.hero.sp.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
		}
		cam.maxDownY = -23.41f;
		Singleton<Music>.use.MusicStart("shackMusic");
	}

	public void InvokeNewLoc()
	{
		Invoke("SetLocationInvoke", 0.1f);
	}

	private void SetLocationInvoke()
	{
		SetLocation();
	}

	public void SetDarkAlpha(float n)
	{
		darkImage.color = new Color(1f, 1f, 1f, n);
	}

	private void SetBorders()
	{
		if (Main.location == 1)
		{
			cam.SetBorders(new Vector2(0f, 72f));
		}
		if (Main.location == 3)
		{
			cam.SetBorders(new Vector2(0f, 122f));
		}
		if (Main.location == 4)
		{
			cam.SetBorders(new Vector2(0f, 51f));
		}
		if (Main.location == 5)
		{
			cam.SetBorders(new Vector2(0f, 0f));
		}
		if (Main.location == 6)
		{
			cam.SetBorders(new Vector2(-2f, 83f));
		}
		if (Main.location == 7)
		{
			cam.SetBorders(new Vector2(0f, 100f));
		}
		if (Main.location == 8)
		{
			cam.SetBorders(new Vector2(-0.5f, 12.78511f));
		}
	}

	public void ShowScene(string sceneName)
	{
		foreach (GameObject value in scenes.Values)
		{
			value.SetActive(value: false);
		}
		scenes[sceneName].SetActive(value: true);
		activeScene = scenes[sceneName];
		currentSceneName = sceneName;
	}

	public void ReLoadScene()
	{
		Singleton<LoaderBg>.use.StartFade();
	}

	public void RefreshScene()
	{
		if (Main.location == 1)
		{
			hero.heroT.position = new Vector2(67f, -4.8f);
			hero.ReturnToGame();
			cam.SetPos(hero.heroT.position);
		}
		if (Main.location == 2)
		{
			hero.heroT.position = new Vector2(-5.54f, -3.117f);
			hero.ReturnToGame();
			hero.SetDirect(n: true);
		}
		if (Main.location == 3)
		{
			if (Main.locationStep == 1)
			{
				hero.heroT.position = new Vector2(16.21f, -0.442f);
			}
			else if (Main.locationStep == 2)
			{
				hero.heroT.position = new Vector2(59.51f, -2.04f);
			}
			else if (Main.locationStep == 3)
			{
				hero.heroT.position = new Vector2(89.84f, -3.54f);
				Main.hero.SetStepsNames(1);
			}
			else if (Main.locationStep == 4)
			{
				hero.heroT.position = new Vector2(25f, -2.21f);
				Main.game.cam.SwitchOffsetY(-3f);
			}
			else if (Main.locationStep == 5)
			{
				hero.heroT.position = new Vector2(26.58f, -8.66f);
			}
			hero.ReturnToGame();
			hero.SetDirect(n: true);
			cam.SetPos(hero.heroT.position);
		}
		if (Main.location == 5)
		{
			arrBreakObj.Clear();
			Singleton<Sounds>.use.StopAllSounds();
			Singleton<ManagerFunctions>.use.ClearAll();
			Singleton<InputChecker>.use.ClearObj();
			SceneManager.LoadScene("RestartLocation");
		}
		if (Main.location == 6)
		{
			hero.heroT.position = new Vector2(42.28f, -4.577395f);
			hero.ReturnToGame();
			hero.SetDirect(n: true);
		}
		if (Main.location == 7)
		{
			Main.hero.heroT.position = new Vector2(0f, -4.872f);
			hero.ReturnToGame();
			hero.SetDirect(n: true);
			Main.game.cam.calculateSmoothPos.x = 0f;
			Main.game.cam.calculateSmoothPos.y = -0.68f;
			Main.game.cam.calculateSmoothPos.z = -10f;
			Main.game.cam.cameraT.position = Main.game.cam.calculateSmoothPos;
			Main.game.cam.isGoToPos = false;
		}
		if (Main.location == 8)
		{
			hero.heroT.position = new Vector2(-7.78f, -1.987383f);
			cam.cameraT.position = new Vector3(-0.5f, 2.36f, -10f);
			hero.ReturnToGame();
			hero.SetDirect(n: true);
		}
		if (bgmoveEx)
		{
			level.GetComponent<BGmove>().SetPos();
		}
		foreach (InterfaceBreakObj item in arrBreakObj)
		{
			item.BreakObj();
		}
	}

	private void HotKeysRuntime()
	{
		if (canShowPauseTab && Input.GetButtonDown("Cancel"))
		{
			pause.SetMode(!Main.game.cam.isPause);
		}
	}
}
