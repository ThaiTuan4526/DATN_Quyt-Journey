using NaughtyAttributes;
using UnityEngine;

public class ControlScenesEditorLevel6 : MonoBehaviour
{
	public Gameplay game;

	public GameObject level6;

	public GameObject level6_2;

	[Button("Главная")]
	private void BTloadLevel1()
	{
		HideAll();
		level6.SetActive(value: true);
		game.choseScene = 1;
	}

	[Button("Домик")]
	private void BTloadLevel2()
	{
		HideAll();
		level6_2.SetActive(value: true);
		game.choseScene = 2;
	}

	private void HideAll()
	{
		level6.SetActive(value: false);
		level6_2.SetActive(value: false);
	}
}
