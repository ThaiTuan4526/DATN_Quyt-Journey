using UnityEngine;

public class StoneDropAn : MonoBehaviour
{
	public void DropSo()
	{
		Singleton<Sounds>.use.So2("stoneDropBook", 0.5f);
	}
}
