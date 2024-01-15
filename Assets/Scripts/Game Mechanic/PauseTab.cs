using UnityEngine;
using UnityEngine.UI;

public class PauseTab : ScrollBT
{
	public LevelChoseBt[] levels;

	public ButtonUI bt1;

	public ButtonUI bt2;

	public void Init()
	{
		buttons = new ButtonUI[2];
		buttons[0] = bt1;
		buttons[1] = bt2;
		lastBt = 2;
		for (int i = 0; i < levels.Length; i++)
		{
			for (int j = 0; j < Singleton<LoaderBg>.use.nOflanguages; j++)
			{
				string text = "";
				text = ((i != 1) ? ("Sprites/Buttons/continueBt" + Singleton<LoaderBg>.use.languages[j]) : ("Sprites/Buttons/exitBt" + Singleton<LoaderBg>.use.languages[j]));
				levels[i].transform.GetChild(j).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(text);
				levels[i].transform.GetChild(j).gameObject.GetComponent<Image>().SetNativeSize();
			}
		}
		ButtonUI[] array = buttons;
		for (int k = 0; k < array.Length; k++)
		{
			array[k].Init();
		}
		LevelChoseBt[] array2 = levels;
		for (int k = 0; k < array2.Length; k++)
		{
			array2[k].UpdateText();
		}
		base.gameObject.SetActive(value: false);
	}

	public void SetMode(bool n)
	{
		base.gameObject.SetActive(n);
		Singleton<ManagerFunctions>.use.isPause = n;
		Main.hero.SetPause(n);
		Main.game.cam.isPause = n;
		switchButton = false;
		if (n)
		{
			Unchosed();
			choseBT = 0;
			buttons[choseBT].SetMode(n: true);
		}
	}

	private void Update()
	{
		ScrollActions();
	}
}
