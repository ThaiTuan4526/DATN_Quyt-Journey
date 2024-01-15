using System;
using System.Collections.Generic;

[Serializable]
public class SaveBase
{
	public Dictionary<string, int> intData = new Dictionary<string, int>();

	public Dictionary<string, float> floatData = new Dictionary<string, float>();

	public Dictionary<string, string> stringData = new Dictionary<string, string>();

	public SaveBase(Saves saves)
	{
		foreach (string intKey in saves.intKeys)
		{
			intData[intKey] = saves.intData[intKey];
		}
		foreach (string floatKey in saves.floatKeys)
		{
			floatData[floatKey] = saves.floatData[floatKey];
		}
		foreach (string stringKey in saves.stringKeys)
		{
			stringData[stringKey] = saves.stringData[stringKey];
		}
	}
}
