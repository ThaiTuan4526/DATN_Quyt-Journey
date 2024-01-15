using UnityEngine;

public class DoorLevel7An : MonoBehaviour
{
	public void ShowB()
	{
		Singleton<Sounds>.use.So("butterflyGame");
	}

	public void OpenClose()
	{
		Singleton<Sounds>.use.So("openDoorB");
	}
}
