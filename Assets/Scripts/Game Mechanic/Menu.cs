using UnityEngine;
using UnityEngine.UI;

public class Menu : ScrollBT
{
    #region Variables
    public GameObject[] textMain;

	public GameObject[] textSettings;

	public LevelChoseBt[] levels;

	public GameObject buttonsMain;

	public GameObject buttonsLevels;

	public GameObject buttonsSettings;

	public GameObject screens;

	public Image fade;

	public RectTransform slider1;

	public RectTransform slider2;

	public GameObject isFullimage;

	public GameObject isTipsimage;

	public Text resText;

	public GameObject objMain;

	public GameObject objLevels;

	public GameObject objSettings;

	private ResolutionManager resolutionS;

	private int chosedScreen;

	private bool switchButtonX;

	private bool fadeDown;

	private bool fadeUp;

	private float lerpF;

	private int prevChosed;

	private const float dist = 85f;

	internal int resLevel;

	private bool isFull;
    #endregion

    private void Awake()
	{
		if (!Main.managerDone)
		{
			ScriptableObject.CreateInstance<InitiatorGame>().Init();
		}
		Main.menu = this;
		Main.location = 0;
		resolutionS = GetComponent<ResolutionManager>();
	}

	private void Start()
	{
		Main.SetCanvas();
		Singleton<ManagerFunctions>.use.isPause = false;
		Cursor.visible = false;
		Singleton<Music>.use.MusicStart("demoEnd");
		buttons = new ButtonUI[10];
		for (int i = 0; i < levels.Length; i++)
		{
			for (int j = 0; j < Singleton<LoaderBg>.use.nOflanguages; j++)
			{
				string text = "";
				text = ((i >= 8) ? ("Sprites/Buttons/backBt" + Singleton<LoaderBg>.use.languages[j]) : ("Sprites/Buttons/Episodes/level" + Singleton<LoaderBg>.use.languages[j] + "000" + (i + 1)));
				levels[i].transform.GetChild(j).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(text);
				levels[i].transform.GetChild(j).gameObject.GetComponent<Image>().SetNativeSize();
			}
		}
		for (int k = 0; k < buttonsLevels.transform.childCount - 1; k++)
		{
			RectTransform component = buttonsLevels.transform.GetChild(k).gameObject.GetComponent<RectTransform>();
			component.localPosition = new Vector3(component.localPosition.x, 280f - (float)k * 85f);
			if (Singleton<Saves>.use.save("level" + (k + 1)) == 1)
			{
				buttonsLevels.transform.GetChild(k).gameObject.SetActive(value: true);
			}
			else
			{
				buttonsLevels.transform.GetChild(k).gameObject.SetActive(value: false);
			}
		}
		UpdateAllTexts();
		SetButtons(1);
		LoadDataQ();
		Singleton<LoaderBg>.use.HideLoader();
		Singleton<LoaderBg>.use.BreakFade();
	}

	public void SetButtons(int n)
	{
		chosedScreen = n;
		objMain.SetActive(value: false);
		objLevels.SetActive(value: false);
		objSettings.SetActive(value: false);
		switch (n)
		{
		case 1:
		{
			HideAllScreens();
			screens.transform.GetChild(0).gameObject.SetActive(value: true);
			objMain.SetActive(value: true);
			fade.color = new Color(1f, 1f, 1f, 0f);
			lastBt = buttonsMain.transform.childCount;
			for (int k = 0; k < buttonsMain.transform.childCount; k++)
			{
				buttons[k] = buttonsMain.transform.GetChild(k).gameObject.GetComponent<ButtonUI>();
			}
			break;
		}
		case 2:
		{
			objLevels.SetActive(value: true);
			prevChosed = 0;
			lastBt = 0;
			fade.color = new Color(1f, 1f, 1f, 0f);
			fadeDown = false;
			fadeUp = false;
			HideAllScreens();
			screens.transform.GetChild(0).gameObject.SetActive(value: true);
			for (int j = 0; j < buttonsLevels.transform.childCount - 1; j++)
			{
				if (Singleton<Saves>.use.save("level" + (j + 1)) == 1)
				{
					lastBt++;
					buttons[j] = buttonsLevels.transform.GetChild(j).gameObject.GetComponent<ButtonUI>();
				}
			}
			buttons[lastBt] = buttonsLevels.transform.GetChild(8).gameObject.GetComponent<ButtonUI>();
			buttonsLevels.transform.GetChild(8).gameObject.GetComponent<RectTransform>().localPosition = new Vector3(buttonsLevels.transform.GetChild(8).gameObject.GetComponent<RectTransform>().localPosition.x, -425f);
			lastBt++;
			break;
		}
		case 3:
		{
			LoadDataQ();
			objSettings.SetActive(value: true);
			lastBt = buttonsSettings.transform.childCount;
			for (int i = 0; i < buttonsSettings.transform.childCount; i++)
			{
				buttons[i] = buttonsSettings.transform.GetChild(i).gameObject.GetComponent<ButtonUI>();
			}
			slider1.localPosition = new Vector2(-109f + 218f * Singleton<Saves>.use.saveF("volumeMusic"), slider1.localPosition.y);
			slider2.localPosition = new Vector2(-109f + 218f * Singleton<Saves>.use.saveF("volumeSFX"), slider2.localPosition.y);
			UpdateFullIcon();
			UpdateTipsIcon();
			break;
		}
		}
		for (int l = 0; l < lastBt; l++)
		{
			buttons[l].Init();
		}
		Unchosed();
		choseBT = 0;
		buttons[choseBT].SetMode(n: true);
	}

	private void UpdateAllTexts()
	{
		Main.typeLanguage = Singleton<Saves>.use.saveS("languageGame");
		for (int i = 0; i < Singleton<LoaderBg>.use.nOflanguages; i++)
		{
			if (Main.typeLanguage == Singleton<LoaderBg>.use.languages[i])
			{
				Main.typeLanguageID = i;
				break;
			}
		}
		GameObject[] array = textMain;
		for (int j = 0; j < array.Length; j++)
		{
			array[j].SetActive(value: false);
		}
		textMain[Main.typeLanguageID].SetActive(value: true);
		LevelChoseBt[] array2 = levels;
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j].UpdateText();
		}
		array = textSettings;
		for (int j = 0; j < array.Length; j++)
		{
			array[j].SetActive(value: false);
		}
		textSettings[Main.typeLanguageID].SetActive(value: true);
	}

	private void Update()
	{
		ScrollActions();
		if (chosedScreen == 3)
		{
			float num = Input.GetAxis("Horizontal") + Input.GetAxis("HorizontalJoy");
			if (choseBT == 0 && Mathf.Abs(num) > 0.05f)
			{
				float num2 = Singleton<Saves>.use.saveF("volumeMusic");
				num2 = ((!(num > 0f)) ? (num2 - 0.025f) : (num2 + 0.025f));
				if (num2 > 1f)
				{
					num2 = 1f;
				}
				if (num2 < 0f)
				{
					num2 = 0f;
				}
				Singleton<Saves>.use.keepF("volumeMusic", num2);
				slider1.localPosition = new Vector2(-109f + 218f * num2, slider1.localPosition.y);
				Singleton<Music>.use.maxVolume = Singleton<Saves>.use.saveF("volumeMusic");
				Singleton<Music>.use.musicSource.volume = Singleton<Saves>.use.saveF("volumeMusic");
			}
			if (choseBT == 1 && Mathf.Abs(num) > 0.05f)
			{
				float num3 = Singleton<Saves>.use.saveF("volumeSFX");
				num3 = ((!(num > 0f)) ? (num3 - 0.025f) : (num3 + 0.025f));
				if (num3 > 1f)
				{
					num3 = 1f;
				}
				if (num3 < 0f)
				{
					num3 = 0f;
				}
				Singleton<Saves>.use.keepF("volumeSFX", num3);
				slider2.localPosition = new Vector2(-109f + 218f * num3, slider2.localPosition.y);
				Singleton<Sounds>.use.currentVolume = Singleton<Saves>.use.saveF("volumeSFX");
				Singleton<Sounds>.use.soundSource.volume = Singleton<Saves>.use.saveF("volumeSFX");
			}
			if (choseBT == 2)
			{
				if (Mathf.Abs(num) > 0.05f)
				{
					if (!switchButtonX)
					{
						switchButtonX = true;
						if (num > 0f)
						{
							resLevel++;
							if (isFull)
							{
								if (resLevel >= Main.FullscreenResolutions.Count)
								{
									resLevel = 0;
								}
							}
							else if (resLevel >= Main.WindowedResolutions.Count)
							{
								resLevel = 0;
							}
						}
						else
						{
							resLevel--;
							if (resLevel < 0)
							{
								if (isFull)
								{
									resLevel = Main.FullscreenResolutions.Count - 1;
								}
								else
								{
									resLevel = Main.WindowedResolutions.Count - 1;
								}
							}
						}
						Singleton<Sounds>.use.So2("switch34", 0.5f);
						resolutionS.SetResolution(resLevel, isFull);
						Singleton<Saves>.use.keep("settings_currentRes", resLevel);
						ShowCurrentResText();
					}
				}
				else
				{
					switchButtonX = false;
				}
			}
			if (choseBT == 3)
			{
				if (Mathf.Abs(num) > 0.05f)
				{
					if (!switchButtonX)
					{
						switchButtonX = true;
						SwitchFull();
					}
				}
				else
				{
					switchButtonX = false;
				}
			}
			if (choseBT == 4)
			{
				if (Mathf.Abs(num) > 0.05f)
				{
					if (!switchButtonX)
					{
						switchButtonX = true;
						SwitchTips();
					}
				}
				else
				{
					switchButtonX = false;
				}
			}
			if (choseBT == 5)
			{
				if (Mathf.Abs(num) > 0.05f)
				{
					if (!switchButtonX)
					{
						switchButtonX = true;
						if (num > 0f)
						{
							Main.typeLanguageID++;
							if (Main.typeLanguageID >= Singleton<LoaderBg>.use.nOflanguages)
							{
								Main.typeLanguageID = 0;
							}
						}
						else
						{
							Main.typeLanguageID--;
							if (Main.typeLanguageID < 0)
							{
								Main.typeLanguageID = Singleton<LoaderBg>.use.nOflanguages - 1;
							}
						}
						Singleton<Saves>.use.keepS("languageGame", Singleton<LoaderBg>.use.languages[Main.typeLanguageID]);
						UpdateAllTexts();
						Singleton<Sounds>.use.So2("switch34", 0.5f);
					}
				}
				else
				{
					switchButtonX = false;
				}
			}
		}
		if (chosedScreen != 2)
		{
			return;
		}
		if (choseBT < lastBt - 1 && choseBT != prevChosed)
		{
			fadeDown = true;
			fadeUp = false;
			prevChosed = choseBT;
		}
		if (fadeDown)
		{
			lerpF += Time.deltaTime;
			fade.color = new Color(0f, 0f, 0f, lerpF);
			if (lerpF >= 1f)
			{
				fadeDown = false;
				fadeUp = true;
				HideAllScreens();
				if (choseBT < lastBt - 1)
				{
					screens.transform.GetChild(choseBT).gameObject.SetActive(value: true);
				}
				else
				{
					prevChosed = -1;
					screens.transform.GetChild(0).gameObject.SetActive(value: true);
				}
			}
		}
		if (fadeUp)
		{
			lerpF -= Time.deltaTime;
			fade.color = new Color(0f, 0f, 0f, lerpF);
			if (lerpF <= 0f)
			{
				fadeUp = false;
				fade.color = new Color(0f, 0f, 0f, 0f);
			}
		}
	}

	public void ShowCurrentResText()
	{
		try
		{
			if (isFull)
			{
				resText.text = Main.FullscreenResolutions[resolutionS.currFullscreenRes].x + "x" + Main.FullscreenResolutions[resolutionS.currFullscreenRes].y;
			}
			else
			{
				resText.text = Main.WindowedResolutions[resolutionS.currWindowedRes].x + "x" + Main.WindowedResolutions[resolutionS.currWindowedRes].y;
			}
		}
		catch
		{
			Invoke("ShowCurrentResText", 1f);
		}
	}

	private void LoadDataQ()
	{
		if (Singleton<Saves>.use.save("settings_isFull") == 1)
		{
			isFull = true;
		}
		else
		{
			isFull = false;
		}
		if (Singleton<Saves>.use.save("settings_tips") == 1)
		{
			Main.TIPS_MODE = true;
		}
		else
		{
			Main.TIPS_MODE = false;
		}
		if (Singleton<Saves>.use.save("settings_currentRes") >= 0)
		{
			resLevel = Singleton<Saves>.use.save("settings_currentRes");
		}
		else
		{
			resLevel = resolutionS.currFullscreenRes;
		}
		if (isFull)
		{
			if (resLevel >= Main.FullscreenResolutions.Count)
			{
				resLevel = 0;
			}
		}
		else if (resLevel >= Main.WindowedResolutions.Count)
		{
			resLevel = 0;
		}
		if (resLevel < 0)
		{
			if (isFull)
			{
				resLevel = Main.FullscreenResolutions.Count - 1;
			}
			else
			{
				resLevel = Main.WindowedResolutions.Count - 1;
			}
		}
		ShowCurrentResText();
	}

	public void SwitchFull()
	{
		isFull = !isFull;
		if (isFull)
		{
			resLevel = Main.FullscreenResolutions.Count - 1;
		}
		else
		{
			resLevel = Main.WindowedResolutions.Count - 1;
		}
		UpdateFullIcon();
		ShowCurrentResText();
		resolutionS.SetResolution(resLevel, isFull);
		if (isFull)
		{
			Singleton<Saves>.use.keep("settings_isFull", 1);
		}
		else
		{
			Singleton<Saves>.use.keep("settings_isFull", 0);
		}
		Singleton<Sounds>.use.So2("switch34", 0.5f);
	}

	public void SwitchTips()
	{
		Main.TIPS_MODE = !Main.TIPS_MODE;
		UpdateTipsIcon();
		if (Main.TIPS_MODE)
		{
			Singleton<Saves>.use.keep("settings_tips", 1);
		}
		else
		{
			Singleton<Saves>.use.keep("settings_tips", 0);
		}
	}

	private void UpdateFullIcon()
	{
		isFullimage.SetActive(isFull);
	}

	private void UpdateTipsIcon()
	{
		isTipsimage.SetActive(Main.TIPS_MODE);
	}

	private void HideAllScreens()
	{
		for (int i = 0; i < screens.transform.childCount; i++)
		{
			screens.transform.GetChild(i).gameObject.SetActive(value: false);
		}
	}
}
