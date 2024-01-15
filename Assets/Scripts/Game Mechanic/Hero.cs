using System;
using TMPro;
using UnityEngine;

public class Hero : CharBase
{
    #region Hero Variable
    public delegate void SetMethodAction();

	public delegate bool SetMethodOnBlock(LayerMask n);

	[Header("Настройки физики")]
	[Space]
	public float acpX = 0.003f;

	public float jumpSpeed = 6f;

	public float maxSpeed = 0.07f;

	public float SPEED_MAX = 0.07f;

	public float goYkUp = 0.1f;

	public float goYkDown = 0.1f;

	public TextMeshProUGUI text;

	internal Transform heroT;

	private Transform audioT;

	internal bool someAn;

	private bool boost;

	internal bool catchMode;

	internal bool wallMode;

	internal bool runAnyway;

	internal float catchDistFall;

	private float someAnTime;

	private float forceMove;

	internal float goX;

	private float goY;

	private float goAction;

	private int timeStayInFrontOfWall;

	internal CatchHero catchHero;

	internal HeroGoTo goHero;

	internal HeroAn stepAn;

	internal bool pauseMode;

	private Vector2 cachedRbSpeed;

	public SetMethodAction actionM;

	public SetMethodOnBlock blockM;
    #endregion

    public void Init()
	{
		Main.hero = this;
		heroT = base.gameObject.transform;
		heroObj = heroT.GetChild(0).gameObject.transform.GetChild(0).gameObject;
		rb = base.gameObject.GetComponent<Rigidbody2D>();
		sp = heroObj.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
		an = heroObj.transform.GetChild(0).gameObject.GetComponent<Animator>();
		an.keepAnimatorControllerStateOnDisable = true;
		stepAn = heroObj.transform.GetChild(0).gameObject.GetComponent<HeroAn>();
		catchHero = new CatchHero();
		catchHero.Init(this);
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/GameObj/HeroAudio")) as GameObject;
		audioT = gameObject.transform;
		goHero = new HeroGoTo();
		goHero.Init(this);
		blockM = OnBlock;
		InitData();
		direct = true;
		SetPos();
		SetIdle();
	}

	public void InitData()
	{
		actionM = null;
		actionM = (SetMethodAction)Delegate.Combine(actionM, new SetMethodAction(EmptyFunction));
		if (Main.location == 1)
		{
			SetHeroAn(2);
			SetStepsNames(1);
		}
		else if (Main.location == 2 || Main.location == 5)
		{
			SetHeroAn(1);
			SetStepsNames(2);
		}
		else if (Main.location == 8)
		{
			SetHeroAn(1);
			SetStepsNames(4);
		}
		else
		{
			SetHeroAn(1);
			SetStepsNames(1);
		}
	}

	public void Run()
	{
		if (Mathf.Abs(goX) > 0f)
		{
			if (goX > 0f)
			{
				goX = 1f;
				direct = true;
			}
			else
			{
				goX = -1f;
				direct = false;
			}
			if (runAnyway || (!direct && !blockM(blockLeftLayer)) || (direct && !blockM(blockRightLayer)))
			{
				if (onPlatform && !jump)
				{
					if (!run && !catchMode)
					{
						SetAn(runStartAn);
					}
					else if (myAn == runStartAn && AnCompleted())
					{
						SetAn(runAn);
					}
					run = true;
				}
				idle = false;
				if (!boost)
				{
					boost = true;
					speedX = 0f;
				}
			}
			if (!catchMode)
			{
				sp.flipX = !direct;
			}
			else if ((direct && catchDistFall > 0f) || (!direct && catchDistFall < 0f))
			{
				Vector3 position = heroT.position;
				position.x += catchDistFall;
				position.y -= 0.15f;
				colliders = Physics2D.OverlapCircleAll(position, 0.05f, groundLayer);
				if (colliders.Length == 0)
				{
					speedX = 0f;
					boost = false;
				}
			}
		}
		else
		{
			boost = false;
		}
		if (boost)
		{
			speedX += goX * 0.25f * Time.deltaTime;
			if (direct)
			{
				if (speedX > maxSpeed)
				{
					speedX = maxSpeed;
				}
				if (speedX < 0f)
				{
					speedX += 0.05f;
				}
			}
			else
			{
				if (speedX < 0f - maxSpeed)
				{
					speedX = 0f - maxSpeed;
				}
				if (speedX > 0f)
				{
					speedX -= 0.05f;
				}
			}
		}
		else if (onPlatform)
		{
			speedX *= 0.9f;
			if (Mathf.Abs(speedX) < 0.05f)
			{
				speedX = 0f;
			}
			SetIdle();
		}
		else
		{
			speedX *= 0.99f;
		}
		if (Mathf.Abs(speedX) > 0f)
		{
			if (runAnyway || (speedX < 0f && !blockM(blockLeftLayer)) || (speedX > 0f && !blockM(blockRightLayer)))
			{
				heroT.position = new Vector2(heroT.position.x + speedX * 60f * Time.deltaTime, heroT.position.y - goY * 60f * Time.deltaTime);
				if (catchMode)
				{
					catchHero.SetBoxPos();
				}
			}
			else
			{
				speedX = 0f;
				SetIdle();
			}
		}
		if (idle && !catchMode && !wallMode && AnCompleted())
		{
			if (classicAn && myAn != "Idle1_2" && UnityEngine.Random.value > 0.9f)
			{
				SetAn("Idle1_2");
			}
			else if (classicAn && myAn == "Idle")
			{
				SetDirectAn("Idle3");
			}
			else
			{
				SetDirectAn(idleAn);
			}
		}
	}

	private void Actions()
	{
		if (onPlatform && Input.GetButtonDown("Action"))
		{
			actionM();
			if (!stopActions && !catchMode && classicAn)
			{
				SetSomeAn("NoActions");
			}
		}
	}

	private void Jump()
	{
		if (!fall)
		{
			if (Input.GetButtonDown("Jump") && !jump && !catchMode)
			{
				rb.AddForce(heroT.up * jumpSpeed, ForceMode2D.Impulse);
				onPlatform = false;
				timeStayInFrontOfWall = 0;
				SetDirectAn(jumpAn);
				maxSpeed = SPEED_MAX;
				jump = true;
				idle = false;
				run = false;
				goY = 0f;
				Singleton<Sounds>.use.So2("jump", 0.2f);
			}
			else if (jump && AnCompleted())
			{
				jump = false;
				fall = true;
				SetAn(fallAn);
			}
		}
	}

	private void ForceMove()
	{
		forceMove += Time.deltaTime;
		if (forceMove > 0.25f)
		{
			Singleton<ManagerFunctions>.use.removeFunction(ForceMove);
		}
		if ((speedX < 0f && !blockM(blockLeftLayer)) || (speedX > 0f && !blockM(blockRightLayer)))
		{
			heroT.position = new Vector2(heroT.position.x + speedX, heroT.position.y);
		}
	}

	public void SetIdle()
	{
		if (onPlatform && !jump && !catchMode && !idle)
		{
			idle = true;
			run = false;
			SetAn(idleAn);
		}
	}

	public void ReturnToGame()
	{
		stopActions = false;
		runAnyway = false;
		idle = true;
		run = false;
		speedX = 0f;
		Main.hero.gameObject.SetActive(value: true);
		Main.hero.rb.isKinematic = false;
		SetAn(Main.hero.idleAn);
	}

	public void SetFear(float n)
	{
		if (!stopActions && onPlatform)
		{
			stopActions = true;
			SetDirect(heroT.position.x < n);
			SetAn("Fear");
		}
	}

	private void CheckGround()
	{
		if (jump)
		{
			return;
		}
		colliders = Physics2D.OverlapCircleAll(heroT.position, 0.05f, groundLayer);
		goY = 0f;
		if (colliders.Length != 0)
		{
			if (!catchMode)
			{
				float z = colliders[0].transform.localRotation.z;
				if (Mathf.Abs(z) > 0.12f)
				{
					if ((z > 0f && speedX < 0f) || (z < 0f && speedX > 0f))
					{
						goY = Mathf.Abs(z * goYkDown);
					}
					else
					{
						goY = -1f * Mathf.Abs(z * goYkUp);
					}
				}
			}
			if (rb.velocity.y < -9.5f)
			{
				if (classicAn)
				{
					SetSomeAn("LandingFall");
					Singleton<Sounds>.use.So("fall");
					speedX = 0.05f;
					forceMove = 0f;
					if (!direct)
					{
						speedX *= -1f;
					}
				}
			}
			else if (fall)
			{
				Singleton<Sounds>.use.So(stepAn.step1);
			}
			onPlatform = true;
			fall = false;
		}
		else
		{
			if (!(rb.velocity.y < -0.8f))
			{
				return;
			}
			onPlatform = false;
			if (!fall && rb.velocity.y < -1.5f)
			{
				if (catchMode)
				{
					catchHero.CatchBreak();
					maxSpeed = SPEED_MAX;
				}
				SetAn(fall2An);
				idle = false;
				fall = true;
				run = false;
			}
		}
	}

	public bool OnBlock(LayerMask n)
	{
		v3 = heroT.position;
		v3.y += 0.6f;
		colliders = Physics2D.OverlapCircleAll(v3, 0.4f, blockBoxLayer);
		if (colliders.Length != 0 && isBlockOnHero() && ((speedX < 0f && heroT.position.x > colliders[iC].transform.position.x) || (speedX > 0f && heroT.position.x < colliders[iC].transform.position.x)))
		{
			return true;
		}
		colliders = Physics2D.OverlapCircleAll(heroT.position, 0.4f, n);
		if (colliders.Length != 0 && isBlockOnHero())
		{
			if (++timeStayInFrontOfWall > 20 && onPlatform)
			{
				idle = true;
				if (classicAn && !someAn)
				{
					SetAn("Wall");
					wallMode = true;
				}
			}
			return true;
		}
		wallMode = false;
		timeStayInFrontOfWall = 0;
		return false;
	}

	private bool isBlockOnHero()
	{
		float num = heroT.position.y + 0.8f;
		for (i = 0; i < colliders.Length; i++)
		{
			if (heroT.position.y < colliders[i].transform.position.y + colliders[i].transform.localScale.y / 2f && num > colliders[i].transform.position.y - colliders[i].transform.localScale.y / 2f)
			{
				iC = i;
				return true;
			}
		}
		return false;
	}

	private bool isObjOnHero()
	{
		float num = heroT.position.y + 0.6f;
		for (i = 0; i < collidersObj.Length; i++)
		{
			if (num < collidersObj[i].transform.position.y + collidersObj[i].transform.localScale.y / 2f && num > collidersObj[i].transform.position.y - collidersObj[i].transform.localScale.y / 2f)
			{
				iC = i;
				return true;
			}
		}
		return false;
	}

	public void SetLink(GameObjInterface inObj, bool n)
	{
		if (n)
		{
			actionM = (SetMethodAction)Delegate.Combine(actionM, new SetMethodAction(inObj.DoAction));
		}
		else
		{
			actionM = (SetMethodAction)Delegate.Remove(actionM, new SetMethodAction(inObj.DoAction));
		}
	}

	public void EmptyFunction()
	{
	}

	public void SetStepsNames(int n)
	{
		switch (n)
		{
		case 1:
			stepAn.step1 = "stepGrass1";
			stepAn.step2 = "stepGrass2";
			break;
		case 2:
			stepAn.step1 = "step_room_zvuk";
			stepAn.step2 = "step_room2_zvuk";
			break;
		case 3:
			stepAn.step1 = "wood1";
			stepAn.step2 = "wood2";
			break;
		case 4:
			stepAn.step1 = "stoneStep1";
			stepAn.step2 = "stoneStep2";
			break;
		case 5:
			stepAn.step1 = "stepWood1";
			stepAn.step2 = "stepWood2";
			break;
		}
	}

	public void SetHeroAn(int n)
	{
		if (n == 1)
		{
			idleAn = "Idle";
			runAn = "Run";
			runStartAn = "RunStart";
			jumpAn = "Jump";
			fallAn = "Landing";
			fall2An = "FallIdle";
			classicAn = true;
		}
		else
		{
			idleAn = "Idle2";
			runAn = "Run2";
			runStartAn = "RunStart2";
			jumpAn = "Jump2";
			fallAn = "Landing2";
			fall2An = "Landing2";
			classicAn = false;
		}
	}

	public bool CanUse()
	{
		if (!jump && !stopActions && !someAn && onPlatform && !catchMode)
		{
			return true;
		}
		return false;
	}

	public void SetSomeAn(string n)
	{
		speedX = 0f;
		someAn = true;
		someAnTime = 0f;
		SetAn(n);
	}

	public void SetPause(bool n)
	{
		if (base.gameObject.activeSelf)
		{
			pauseMode = n;
			if (n)
			{
				an.speed = 0f;
				cachedRbSpeed = rb.velocity;
				rb.isKinematic = true;
				rb.velocity = Vector2.zero;
			}
			else
			{
				an.speed = 1f;
				rb.isKinematic = false;
				rb.velocity = cachedRbSpeed;
			}
		}
	}

	private void Update()
	{
		if (!stopActions && !pauseMode)
		{
			if (someAn)
			{
				someAnTime += Time.deltaTime;
				if (someAnTime > 0.2f && AnCompleted())
				{
					someAn = false;
					SetIdle();
				}
				return;
			}
			CheckGround();
			goX = Input.GetAxis("Horizontal") + Input.GetAxis("HorizontalJoy");
			if (!someAn)
			{
				Run();
				Actions();
				Jump();
			}
		}
		audioT.position = heroT.position;
	}
}
