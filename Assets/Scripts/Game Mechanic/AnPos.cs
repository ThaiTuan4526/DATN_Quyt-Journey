using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New An Base", menuName = "AnBase")]
public class AnPos : ScriptableObject
{
	public string[] anNames;

	public Vector2[] pos;

	private Dictionary<string, Vector2> posAn = new Dictionary<string, Vector2>();

	private bool initialized;

	public void Init()
	{
		if (!initialized)
		{
			for (int i = 0; i < anNames.Length; i++)
			{
				posAn.Add(anNames[i], pos[i]);
			}
			initialized = true;
		}
	}

	public Vector2 GetPos(string n)
	{
		if (!posAn.ContainsKey(n))
		{
			return Vector2.zero;
		}
		return posAn[n];
	}
}
