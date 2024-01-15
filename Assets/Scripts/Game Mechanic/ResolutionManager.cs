using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
	private bool FixedAspectRatio;

	private float TargetAspectRatio = 1.7777778f;

	private float WindowedAspectRatio = 1.7777778f;

	private Resolution DisplayResolution;

	internal int currWindowedRes;

	internal int currFullscreenRes;

	private void Start()
	{
		StartCoroutine(StartRoutine());
	}

	public void SetResolution(int index, bool fullscreen)
	{
		Vector2 vector = default(Vector2);
		if (fullscreen)
		{
			currFullscreenRes = index;
			vector = Main.FullscreenResolutions[currFullscreenRes];
		}
		else
		{
			currWindowedRes = index;
			vector = Main.WindowedResolutions[currWindowedRes];
		}
		bool flag = Screen.fullScreen && !fullscreen;
		Screen.SetResolution((int)vector.x, (int)vector.y, fullscreen);
		if (Application.platform == RuntimePlatform.OSXPlayer)
		{
			StopAllCoroutines();
			if (flag)
			{
				StartCoroutine(SetResolutionAfterResize(vector));
			}
		}
	}

	private IEnumerator StartRoutine()
	{
		if (Application.platform == RuntimePlatform.OSXPlayer)
		{
			DisplayResolution = Screen.currentResolution;
		}
		else if (Screen.fullScreen)
		{
			Resolution r = Screen.currentResolution;
			yield return null;
			yield return null;
			DisplayResolution = Screen.currentResolution;
			Screen.SetResolution(r.width, r.height, fullscreen: true);
			yield return null;
		}
		else
		{
			DisplayResolution = Screen.currentResolution;
		}
		InitResolutions();
	}

	private void InitResolutions()
	{
		if (Main.firstSetRes)
		{
			return;
		}
		Main.firstSetRes = true;
		Main.resolutions = Screen.resolutions;
		_ = (float)DisplayResolution.width / (float)DisplayResolution.height;
		Main.WindowedResolutions = new List<Vector2>();
		Main.FullscreenResolutions = new List<Vector2>();
		float num = 0f;
		for (int i = 0; i < Main.resolutions.Length; i++)
		{
			if (Mathf.Abs(num - (float)Main.resolutions[i].width) > 40f)
			{
				num = Main.resolutions[i].width;
				Vector2 item = new Vector2(Main.resolutions[i].width, Mathf.Round((float)Main.resolutions[i].width / (FixedAspectRatio ? TargetAspectRatio : WindowedAspectRatio)));
				if (DisplayResolution.height >= 440 && item.y < (float)DisplayResolution.height * 0.8f)
				{
					Main.WindowedResolutions.Add(item);
				}
				Main.FullscreenResolutions.Add(new Vector2(Main.resolutions[i].width, Main.resolutions[i].height));
			}
		}
		if (Singleton<Saves>.use.save("settings_firstSaveRes") == 1)
		{
			Singleton<Saves>.use.keep("settings_firstSaveRes", 2);
			Singleton<Saves>.use.keep("settings_currentRes", Main.FullscreenResolutions.Count);
		}
		Main.FullscreenResolutions = Enumerable.ToList(Enumerable.OrderBy(Main.FullscreenResolutions, (Vector2 resolution) => resolution.x));
		bool flag = false;
		currWindowedRes = Main.WindowedResolutions.Count - 1;
		currFullscreenRes = Main.FullscreenResolutions.Count - 1;
		if (Screen.fullScreen)
		{
			for (int j = 0; j < Main.FullscreenResolutions.Count; j++)
			{
				if (Main.FullscreenResolutions[j].x == (float)Screen.width && Main.FullscreenResolutions[j].y == (float)Screen.height)
				{
					currFullscreenRes = j;
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				SetResolution(Main.FullscreenResolutions.Count - 1, fullscreen: true);
			}
			return;
		}
		for (int k = 0; k < Main.WindowedResolutions.Count; k++)
		{
			if (Main.WindowedResolutions[k].x == (float)Screen.width && Main.WindowedResolutions[k].y == (float)Screen.height)
			{
				flag = true;
				currWindowedRes = k;
				break;
			}
		}
		if (!flag)
		{
			SetResolution(Main.WindowedResolutions.Count - 1, fullscreen: false);
		}
	}

	private IEnumerator SetResolutionAfterResize(Vector2 r)
	{
		int maxTime = 5;
		float time = Time.time;
		yield return null;
		yield return null;
		int lastW = Screen.width;
		int lastH = Screen.height;
		while (Time.time - time < (float)maxTime)
		{
			if (lastW != Screen.width || lastH != Screen.height)
			{
				Screen.SetResolution((int)r.x, (int)r.y, Screen.fullScreen);
				break;
			}
			yield return null;
		}
	}
}
