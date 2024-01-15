using UnityEngine;

public class MonsterBridge : NPCBase, InterfaceBreakObj
{
	public GameObject b2;

	public GameObject bridgePart;

	public ButterflyBridge butterflyBridge;

	public BridgeLever bridgeLever;

	private Vector3 defaultPos;

	private bool isSleep;

	private float waitTime;

	private int status;

	private bool stayMode;

	private bool idleMode;

	internal bool dead;

	private bool goSleep;

	private int takeHero;

	private void Start()
	{
		Main.game.arrBreakObj.Add(this);
		posAn.Add("Awake", new Vector2(-0.467f, -0.223f));
		posAn.Add("Sleep", new Vector2(-0.467f, -0.223f));
		posAn.Add("ToSleep", new Vector2(-0.467f, -0.223f));
		posAn.Add("Run", new Vector2(0f, 0f));
		posAn.Add("Dive", new Vector2(0.099f, -0.122f));
		posAn.Add("Attack", new Vector2(0.203f, 0f));
		posAn.Add("Fall", new Vector2(-0.087f, -0.064f));
		InitMain();
		takeHero = 0;
		isSleep = true;
		dead = false;
		defaultPos = skinT.position;
		direct = false;
		sp.flipX = true;
		maxSpeed = 0.08f;
		b2.SetActive(value: false);
		SetAn("Sleep");
	}

	public void BreakObj()
	{
		an.speed = 1f;
		dead = false;
		goSleep = false;
		maxSpeed = 0.08f;
		sp.flipX = true;
		direct = false;
		stayMode = false;
		idleMode = false;
		isSleep = true;
		rb.isKinematic = false;
		GetComponent<BoxCollider2D>().enabled = true;
		GetComponent<CircleCollider2D>().enabled = true;
		skinT.position = defaultPos;
		b2.SetActive(value: false);
		SetAn("Sleep");
		Singleton<ManagerFunctions>.use.removeFunction(WakeUp);
		Singleton<ManagerFunctions>.use.removeFunction(Go);
		Singleton<ManagerFunctions>.use.removeFunction(Attack);
		Singleton<ManagerFunctions>.use.removeFunction(Fall);
	}

	public void SetWakeUp()
	{
		if (isSleep)
		{
			maxSpeed = 0.12f;
			isSleep = false;
			waitTime = 0f;
			SetAn("Awake");
			Singleton<ManagerFunctions>.use.addFunction(WakeUp);
			AddCloud();
		}
	}

	public void RunAgain()
	{
		if (!dead && (goSleep || direct))
		{
			goSleep = false;
			maxSpeed = 0.08f;
			SetRun(n: false);
			Singleton<ManagerFunctions>.use.addFunction(Go);
			Singleton<ManagerFunctions>.use.removeFunction(Down);
		}
	}

	private void AddCloud()
	{
		if (takeHero > 2)
		{
			(Object.Instantiate(Resources.Load("Prefabs/GameObj/Cloud2"), Main.hero.heroT, instantiateInWorldSpace: false) as GameObject).transform.localPosition = Vector3.zero;
		}
	}

	private void WakeUp()
	{
		waitTime += Time.deltaTime;
		if (waitTime > 1f && AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(WakeUp);
			Singleton<ManagerFunctions>.use.addFunction(Go);
			SetRun(n: false);
		}
	}

	private void Go()
	{
		if (idleMode)
		{
			waitTime += Time.deltaTime;
			if (waitTime > 2f)
			{
				SetRun(n: true);
				idleMode = false;
				speedX = 0f;
				an.speed = 1f;
				maxSpeed = 0.05f;
			}
			return;
		}
		if (stayMode)
		{
			waitTime += Time.deltaTime;
			if (waitTime > 7f)
			{
				SetRun(n: true);
				idleMode = false;
				stayMode = false;
				speedX = 0f;
				an.speed = 1f;
				maxSpeed = 0.05f;
				return;
			}
			if (!Main.hero.AnFrame(0.74f) || bridgeLever.isDown)
			{
				return;
			}
			stayMode = false;
			speedX = 0f;
			an.speed = 1f;
		}
		CheckGround();
		if (skinT.position.x > 100f && fall)
		{
			speedX = 0.05f;
			Singleton<ManagerFunctions>.use.removeFunction(Go);
			Singleton<ManagerFunctions>.use.addFunction(Fall);
			dead = true;
			GetComponent<BoxCollider2D>().enabled = false;
			GetComponent<CircleCollider2D>().enabled = false;
			return;
		}
		if (!direct && base.transform.position.x < 102f && maxSpeed > 0.07f)
		{
			maxSpeed *= 0.99f;
		}
		Run();
		if (direct)
		{
			if (base.transform.position.x > 118f)
			{
				maxSpeed = 0.08f;
				sp.flipX = true;
				direct = false;
				goSleep = true;
				SetAn("ToSleep");
				Singleton<ManagerFunctions>.use.removeFunction(Go);
				Singleton<ManagerFunctions>.use.addFunction(Down);
				return;
			}
			if (base.transform.position.x > 104f)
			{
				maxSpeed = 0.035f;
			}
		}
		if (!direct && base.transform.position.x < 72f && !bridgePart.activeSelf)
		{
			idleMode = true;
			waitTime = 0f;
			an.speed = 0f;
		}
		if (!direct && Mathf.Abs(skinT.position.x - Main.hero.heroT.position.x) < 2f)
		{
			Main.hero.SetFear(skinT.position.x);
		}
		if ((!direct && Mathf.Abs(skinT.position.x - Main.hero.heroT.position.x) < 1f) || (direct && Mathf.Abs(skinT.position.x - Main.hero.heroT.position.x) < 0.5f))
		{
			SetAn("Attack");
			bridgeLever.StopF();
			status = 1;
			waitTime = 0f;
			Singleton<ManagerFunctions>.use.removeFunction(Go);
			Singleton<ManagerFunctions>.use.addFunction(Attack);
		}
	}

	private void Down()
	{
		if (an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
		{
			SetAn("Sleep");
			isSleep = true;
			goSleep = false;
			butterflyBridge.BreakObj();
			butterflyBridge.gameObject.transform.position = new Vector2(117.983f, -2.385f);
			Singleton<ManagerFunctions>.use.removeFunction(Down);
		}
	}

	private void Fall()
	{
		skinT.position = new Vector2(skinT.position.x + speedX * 60f * Time.deltaTime, skinT.position.y);
		if (skinT.position.y < -8.2f)
		{
			if (Main.hero.stopActions && Main.hero.myAn == "Fear")
			{
				Main.hero.stopActions = false;
				Main.hero.SetAn("Idle");
			}
			rb.isKinematic = true;
			rb.velocity = Vector2.zero;
			skinT.position = new Vector2(skinT.position.x, -8.2f);
			SetAn("Dive");
			Singleton<Sounds>.use.So("fallToWater2");
			b2.SetActive(value: true);
			b2.transform.position = new Vector3(base.transform.position.x - 0.1f, base.transform.position.y + 2.238f, 0f);
			Singleton<ManagerFunctions>.use.removeFunction(Fall);
		}
	}

	private void Attack()
	{
		if (status == 1)
		{
			waitTime += Time.deltaTime;
			if ((double)waitTime > 0.1 && an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.82f)
			{
				Main.hero.gameObject.SetActive(value: false);
				status = 2;
				waitTime = 0f;
				Singleton<Sounds>.use.So("isDead");
				Singleton<InputChecker>.use.SetVibro(1f);
			}
		}
		else
		{
			waitTime += Time.deltaTime;
			if (waitTime > 1f)
			{
				Singleton<ManagerFunctions>.use.removeFunction(Attack);
				Main.game.ReLoadScene();
				takeHero++;
			}
		}
	}
}
