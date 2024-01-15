using System.Collections.Generic;
using UnityEngine;

public static class Main
{
	public static int location = 1;

	public static int locationStep = 1;

	public static float rCornerX;

	public static float lCornerX;

	public static float upCornerY;

	public static float downCornerY;

	public static float aspectRatio;

	public const bool testingMode = false;

	public static Gameplay game;

	public static Hero hero;

	public static Bro bro;

	public static Menu menu;

	public static Transform canvasT;

	public static bool TIPS_MODE = false;

	public static Resolution[] resolutions;

	public static bool firstSetRes = false;

	public static List<Vector2> FullscreenResolutions = new List<Vector2>();

	public static List<Vector2> WindowedResolutions = new List<Vector2>();

	public const int overallLanguages = 4;

	public const string mainSavePath = "/playerData.deq";

	public static string typeLanguage;

	public static int typeLanguageID;

	public static bool managerDone = false;

	public const float DEFAULT_DARK = 0.65f;

	public static void InitMain()
	{
		QualitySettings.vSyncCount = 1;
	}

	public static void SetCanvas()
	{
		rCornerX = canvasT.gameObject.GetComponent<RectTransform>().sizeDelta.x / 2f;
		lCornerX = -1f * (canvasT.gameObject.GetComponent<RectTransform>().sizeDelta.x / 2f);
		downCornerY = -1f * (canvasT.gameObject.GetComponent<RectTransform>().sizeDelta.y / 2f);
		upCornerY = canvasT.gameObject.GetComponent<RectTransform>().sizeDelta.y / 2f;
		aspectRatio = canvasT.gameObject.GetComponent<RectTransform>().sizeDelta.x / canvasT.gameObject.GetComponent<RectTransform>().sizeDelta.y;
	}

	public static bool HasComponent<T>(this GameObject flag) where T : Component
	{
		return (Object)flag.GetComponent<T>() != (Object)null;
	}
}
