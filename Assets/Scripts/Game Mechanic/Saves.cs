using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Saves : Singleton<Saves>
{
	private int i;

	private int i2;

	public Dictionary<string, int> intData = new Dictionary<string, int>();

	public Dictionary<string, float> floatData = new Dictionary<string, float>();

	public Dictionary<string, string> stringData = new Dictionary<string, string>();

	public List<string> intKeys = new List<string>();

	public List<string> floatKeys = new List<string>();

	public List<string> stringKeys = new List<string>();

	public void InitSave()
	{
		SetUse(this);
		base.gameObject.name = "[SAVES]";
		SetMain();
	}

	private void SetMain()
	{
		SetNewGame();
		SaveSettings();
		LoadData();
	}

	private void SetNewGame()
	{
		for (int i = 1; i <= 8; i++)
		{
			AddDataInt("level" + i, 0);
		}
		keep("level1", 1);
		AddDataInt("timeOfBreak", 0);
		for (int j = 1; j <= 3; j++)
		{
			AddDataInt("button" + j, 0);
		}
	}

	private void SaveSettings()
	{
		AddDataFloat("volumeMusic", 1f);
		AddDataFloat("volumeSFX", 1f);
		AddDataInt("settings_firstSaveRes", 1);
		AddDataInt("settings_currentRes", -1);
		AddDataInt("settings_isFull", 1);
		AddDataInt("settings_tips", 0);
		AddDataString("languageGame", "En");
	}

	private void AddDataInt(string n, int n2)
	{
		intKeys.Add(n);
		intData.Add(n, n2);
	}

	private void AddDataFloat(string n, float n2)
	{
		floatKeys.Add(n);
		floatData.Add(n, n2);
	}

	private void AddDataString(string n, string n2)
	{
		stringKeys.Add(n);
		stringData.Add(n, n2);
	}

	public void SaveData()
	{
		SaveSystem.SaveData(this);
	}

	private void LoadData()
	{
		if (File.Exists(Application.persistentDataPath + "/playerData.deq"))
		{
			SaveBase saveBase = SaveSystem.LoadData();
			for (i = 0; i < intKeys.Count; i++)
			{
				string key = intKeys[i];
				if (saveBase.intData.ContainsKey(key))
				{
					intData[key] = saveBase.intData[key];
				}
			}
			for (i = 0; i < floatKeys.Count; i++)
			{
				string key2 = floatKeys[i];
				if (saveBase.floatData.ContainsKey(key2))
				{
					floatData[key2] = saveBase.floatData[key2];
				}
			}
			for (i = 0; i < stringKeys.Count; i++)
			{
				string key3 = stringKeys[i];
				if (saveBase.stringData.ContainsKey(key3))
				{
					stringData[key3] = saveBase.stringData[key3];
				}
			}
		}
		else
		{
			MonoBehaviour.print("LOCAL NOT FOUND");
			SaveData();
		}
	}

	private void OnApplicationQuit()
	{
		SaveData();
	}

	public int save(string n)
	{
		return intData[n];
	}

	public string saveS(string n)
	{
		return stringData[n];
	}

	public float saveF(string n)
	{
		return floatData[n];
	}

	public void keep(string n, int d)
	{
		intData[n] = d;
	}

	public void keepS(string n, string d)
	{
		stringData[n] = d;
	}

	public void keepF(string n, float d)
	{
		floatData[n] = d;
	}
}
