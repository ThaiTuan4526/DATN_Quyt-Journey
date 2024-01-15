using UnityEngine;

public class ControlScenes : MonoBehaviour
{
	public GameObject[] levels;

	public void ShowLevel(int n)
	{
		HideAll();
		levels[n].SetActive(value: true);
	}

	private void HideAll()
	{
		GameObject[] array = levels;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: false);
		}
	}
}
