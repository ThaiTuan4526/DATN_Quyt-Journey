using UnityEngine;

public class ChapterShow : MonoBehaviour
{
	public SpriteRenderer sp1;

	public SpriteRenderer sp2;

	private float goTime;

	private float colorA;

	private int status;

	private void Start()
	{
		status = 100;
		colorA = 0f;
		goTime = 0f;
		sp1.sprite = Resources.Load<Sprite>("Sprites/Chapters/chapter" + Main.location + Main.typeLanguage);
		sp2.sprite = Resources.Load<Sprite>("Sprites/Chapters/chapterName" + Main.location + Main.typeLanguage);
		sp1.color = new Color(1f, 1f, 1f, colorA);
		sp2.color = new Color(1f, 1f, 1f, colorA);
		base.transform.position = new Vector3(Main.game.cam.transform.position.x, Main.game.cam.transform.position.y, 0f);
		Main.game.canShowPauseTab = false;
		Singleton<ManagerFunctions>.use.isPause = true;
		Main.game.cam.isPause = true;
		Main.hero.stopActions = true;
		Main.hero.gameObject.SetActive(value: false);
		Singleton<LoaderBg>.use.SetLoader();
	}

	private void Update()
	{
		if (status == 100)
		{
			goTime += Time.deltaTime;
			if (goTime > 0.8f)
			{
				status = 1;
				Main.game.activeScene.SetActive(value: false);
				Singleton<LoaderBg>.use.HideLoader();
			}
		}
		else if (status == 1)
		{
			goTime += Time.deltaTime;
			if (goTime > 0.5f)
			{
				status = 2;
				Singleton<Sounds>.use.So("chapterSo");
				if (Main.location == 7)
				{
					Singleton<Sounds>.use.So("evilGo2");
				}
			}
		}
		else if (status == 2)
		{
			colorA += Time.deltaTime * 0.8f;
			sp1.color = new Color(1f, 1f, 1f, colorA);
			if (colorA >= 1f)
			{
				colorA = 0f;
				status = 3;
				if (Main.location == 1)
				{
					Singleton<Sounds>.use.So("butterflyStart");
				}
			}
		}
		else if (status == 3)
		{
			colorA += Time.deltaTime * 0.8f;
			sp2.color = new Color(1f, 1f, 1f, colorA);
			if (colorA >= 1f)
			{
				goTime = 0f;
				status = 4;
			}
		}
		else if (status == 4)
		{
			goTime += Time.deltaTime;
			if (goTime > 2f)
			{
				EndScene();
			}
		}
	}

	private void EndScene()
	{
		if (Main.location == 1)
		{
			Singleton<Music>.use.MusicStart("forest");
			Main.game.activeScene.SetActive(value: true);
			Main.bro.Spawn();
		}
		else if (Main.location == 4)
		{
			Singleton<Music>.use.MusicStart("forest");
			Main.hero.gameObject.SetActive(value: true);
			Main.game.activeScene.SetActive(value: true);
			Main.hero.SetAn("Run");
		}
		else if (Main.location == 7)
		{
			Singleton<Music>.use.MusicStart("forest");
			Main.hero.gameObject.SetActive(value: true);
			Main.game.activeScene.SetActive(value: true);
			Main.hero.SetAn("Run");
		}
		Singleton<ManagerFunctions>.use.isPause = false;
		Main.game.cam.isPause = false;
		if (Main.location > 1)
		{
			Main.game.canShowPauseTab = true;
		}
		status = 5;
		Object.Destroy(base.gameObject);
	}
}
