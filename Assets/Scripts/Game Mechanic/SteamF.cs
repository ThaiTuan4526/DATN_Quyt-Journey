using System;
using Steamworks;
using Steamworks.Data;
using UnityEngine;

public class SteamF : Singleton<SteamF>
{
	public bool isLife;

	public void Init()
	{
		SetUse(this);
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		base.gameObject.name = "[STEAM]";
		try
		{
			SteamClient.Init(1224020u);
			isLife = true;
			Debug.Log("Steam Initialized: " + SteamClient.Name + " / " + SteamClient.AppId.ToString());
		}
		catch (Exception)
		{
			isLife = false;
			Debug.Log("Couldn't init for some reason (steam is closed etc)");
		}
	}

	public void AddAch(int n)
	{
		if (isLife)
		{
			new Achievement("QuytJourney" + n).Trigger();
		}
	}
}
