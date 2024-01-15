using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T _instance;

	public static T use => _instance;

	public void SetUse(T n)
	{
		_instance = n;
	}
}
