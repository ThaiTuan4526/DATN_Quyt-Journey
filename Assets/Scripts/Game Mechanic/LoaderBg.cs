using UnityEngine;
using UnityEngine.UI;

public class LoaderBg : Singleton<LoaderBg>
{
	public Image fade;

	private float waitTime;

	private bool toFade;

	internal string[] languages;

	internal int nOflanguages;

	public void Init()
	{
		SetUse(this);
		base.gameObject.name = "LOADERBG";
		fade.gameObject.SetActive(value: false);
		nOflanguages = 10;
		languages = new string[nOflanguages];
		languages[0] = "En";
		languages[1] = "Ru";
		languages[2] = "ChS";
		languages[3] = "ChT";
		languages[4] = "Ger";
		languages[5] = "Por";
		languages[6] = "Spa";
		languages[7] = "Tur";
		languages[8] = "Ita";
		languages[9] = "Fr";
		for (int i = 0; i < nOflanguages; i++)
		{
			string path = "Sprites/Buttons/loading" + languages[i];
			base.gameObject.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
			base.gameObject.transform.GetChild(i).gameObject.GetComponent<Image>().SetNativeSize();
		}
		HideLoader();
	}

	public void HideLoader()
	{
		base.gameObject.SetActive(value: false);
	}

	public void SetLoader()
	{
		base.gameObject.SetActive(value: true);
		for (int i = 0; i < base.transform.childCount; i++)
		{
			base.transform.GetChild(i).gameObject.SetActive(value: false);
		}
		base.transform.GetChild(Main.typeLanguageID).gameObject.SetActive(value: true);
	}

	private void SetFadeUp()
	{
		waitTime = 1f;
		fade.color = new Color(0f, 0f, 0f, waitTime);
		toFade = false;
		Main.game.RefreshScene();
		Singleton<ManagerFunctions>.use.removeIndieFunction(FadeDead);
		Singleton<ManagerFunctions>.use.addIndieFunction(FadeDead);
	}

	public void StartFade()
	{
		toFade = true;
		waitTime = 0f;
		fade.gameObject.SetActive(value: true);
		fade.color = new Color(0f, 0f, 0f, waitTime);
		Singleton<ManagerFunctions>.use.removeIndieFunction(FadeDead);
		Singleton<ManagerFunctions>.use.addIndieFunction(FadeDead);
	}

	public void BreakFade()
	{
		Singleton<ManagerFunctions>.use.removeIndieFunction(FadeDead);
		fade.color = new Color(0f, 0f, 0f, 0f);
		fade.gameObject.SetActive(value: false);
	}

	private void FadeDead()
	{
		if (toFade)
		{
			waitTime += Time.deltaTime * 0.5f;
			fade.color = new Color(0f, 0f, 0f, waitTime);
			if (waitTime >= 1f)
			{
				SetFadeUp();
			}
			return;
		}
		waitTime -= Time.deltaTime * 0.5f;
		fade.color = new Color(0f, 0f, 0f, waitTime);
		if (waitTime <= 0.05f)
		{
			Singleton<ManagerFunctions>.use.removeIndieFunction(FadeDead);
			fade.gameObject.SetActive(value: false);
		}
	}
}
