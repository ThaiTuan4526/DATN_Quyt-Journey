using UnityEngine;
using UnityEngine.UI;

public class PipeAby : MonoBehaviour, GameObjInterface
{
	private bool actionDone;

	private GameObject abyUI;

	private Image icon;

	public Fruit[] fruits;

	public CheckMonsterPipe monsterPipe;

	internal bool goodMusic;

	private float playTime;

	private void Start()
	{
		Invoke("Init", 0.1f);
	}

	private void Init()
	{
		goodMusic = false;
		abyUI = Object.Instantiate(Resources.Load("Prefabs/UI/AbyPipeUI"), Main.game.canvasT, instantiateInWorldSpace: false) as GameObject;
		abyUI.transform.localPosition = new Vector3(Main.lCornerX, Main.downCornerY);
		icon = abyUI.transform.GetChild(1).gameObject.GetComponent<Image>();
		Singleton<InputChecker>.use.AddObj2(this);
		SetDisable();
	}

	public void SetSprite(Sprite n)
	{
		icon.sprite = n;
	}

	public void SetDisable()
	{
		base.gameObject.SetActive(value: false);
		abyUI.SetActive(value: false);
	}

	public void SetEnable()
	{
		base.gameObject.SetActive(value: true);
		abyUI.SetActive(value: true);
	}

	private void Update()
	{
		if (Input.GetButtonDown("Aby") && !Main.hero.stopActions && Main.hero.onPlatform && !Main.hero.catchMode && !Main.hero.jump && !Main.hero.fall)
		{
			Main.hero.stopActions = true;
			Singleton<ManagerFunctions>.use.addFunction(PlayPipe);
			if (goodMusic)
			{
				Main.hero.SetAn("PipeGood");
			}
			else
			{
				Main.hero.SetAn("PipeBad");
			}
			playTime = 0f;
			actionDone = false;
		}
	}

	private void PlayPipe()
	{
		if (!actionDone)
		{
			playTime += Time.deltaTime;
			if (playTime > 0.6f)
			{
				actionDone = true;
				if (goodMusic)
				{
					Singleton<Sounds>.use.So("pipeGood");
					Fruit[] array = fruits;
					for (int i = 0; i < array.Length; i++)
					{
						array[i].CheckPipe();
					}
				}
				else
				{
					Singleton<Sounds>.use.So("pipeBad");
				}
				if (monsterPipe.CheckPipe(goodMusic) && !goodMusic)
				{
					Singleton<ManagerFunctions>.use.removeFunction(PlayPipe);
				}
			}
		}
		if (Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(PlayPipe);
			Main.hero.ReturnToGame();
		}
	}

	public virtual void DoAction()
	{
	}

	public virtual void DoActionOnPos()
	{
	}
}
