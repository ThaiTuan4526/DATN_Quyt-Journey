using UnityEngine;

public class Winch : ActiveObj
{
	public Animator an;

	public Animator anMech;

	public SpriteRenderer mech;

	public AddFish addFish;

	private bool actionDone;

	private float posAnDrag;

	internal bool directAnDrag;

	private AudioSource audioSource;

	private void Start()
	{
		Init();
		an.speed = 0f;
		anMech.speed = 0f;
		audioSource = base.gameObject.GetComponent<AudioSource>();
	}

	public override void DoAction()
	{
		Main.hero.goHero.GoTo(base.transform.position.x, this);
	}

	public override void DoActionOnPos()
	{
		Main.hero.heroT.position = new Vector2(base.transform.position.x, Main.hero.heroT.position.y);
		Main.hero.SetDirect(n: false);
		Main.hero.an.speed = 0f;
		Main.hero.SetAn("Winch1");
		if (!directAnDrag)
		{
			Main.hero.an.Play("Winch1", -1, an.GetCurrentAnimatorStateInfo(0).normalizedTime);
		}
		else
		{
			Main.hero.an.Play("Winch2", -1, an.GetCurrentAnimatorStateInfo(0).normalizedTime);
		}
		mech.sortingLayerName = "Default";
		Singleton<ManagerFunctions>.use.addFunction(WinchActions);
	}

	private void BreakPauseHero()
	{
		Main.hero.pauseMode = false;
	}

	private void WinchActions()
	{
		if (Input.GetButtonDown("Action"))
		{
			audioSource.Stop();
			Main.hero.pauseMode = true;
			Main.hero.an.speed = 1f;
			an.speed = 0f;
			anMech.speed = 0f;
			mech.sortingLayerName = "gameobj";
			Main.hero.SetAn("Idle");
			Main.hero.ReturnToGame();
			addFish.UpdateIcon();
			Singleton<ManagerFunctions>.use.removeFunction(WinchActions);
			Invoke("BreakPauseHero", 0.1f);
			return;
		}
		float num = Input.GetAxis("Horizontal") + Input.GetAxis("HorizontalJoy");
		if (Mathf.Abs(num) > 0f)
		{
			if (num > 0f)
			{
				if (!directAnDrag || (directAnDrag && an.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f))
				{
					Main.hero.an.speed = 1f;
					an.speed = 1f;
					anMech.speed = 1f;
					if (!directAnDrag)
					{
						posAnDrag = Main.hero.an.GetCurrentAnimatorStateInfo(0).normalizedTime;
						if (posAnDrag > 1f)
						{
							posAnDrag = 1f;
						}
						Main.hero.an.Play("Winch2", -1, 1f - posAnDrag);
						an.Play("Winch2", -1, 1f - posAnDrag);
						anMech.Play("Winch2", -1, 1f - posAnDrag);
						directAnDrag = true;
					}
				}
				else
				{
					Main.hero.an.speed = 0f;
					an.speed = 0f;
					anMech.speed = 0f;
				}
			}
			else if (directAnDrag || (!directAnDrag && an.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f))
			{
				Main.hero.an.speed = 1f;
				an.speed = 1f;
				anMech.speed = 1f;
				if (directAnDrag)
				{
					posAnDrag = Main.hero.an.GetCurrentAnimatorStateInfo(0).normalizedTime;
					if (posAnDrag > 1f)
					{
						posAnDrag = 1f;
					}
					Main.hero.an.Play("Winch1", -1, 1f - posAnDrag);
					an.Play("Winch1", -1, 1f - posAnDrag);
					anMech.Play("Winch1", -1, 1f - posAnDrag);
					directAnDrag = false;
				}
			}
			else
			{
				Main.hero.an.speed = 0f;
				an.speed = 0f;
				anMech.speed = 0f;
			}
		}
		else
		{
			Main.hero.an.speed = 0f;
			an.speed = 0f;
			anMech.speed = 0f;
		}
		if (Main.hero.an.speed > 0f)
		{
			if (!audioSource.isPlaying)
			{
				audioSource.Play();
			}
		}
		else
		{
			audioSource.Pause();
		}
	}
}
