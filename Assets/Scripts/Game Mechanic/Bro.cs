using UnityEngine;

public class Bro : NPCBase
{
	public Transform[] pointsT;

	public ButterflyStart butterfly;

	public GameObject cloud;

	public DoorHome door;

	public BoxCollider2D mushBox;

	private GameObject abyUI;

	private Vector3[] targets;

	private float waitTime;

	private int status;

	private bool waitPlayer;

	private int nextPoint;

	private bool finalGo;

	private float jumpSpeed = 6f;

	private void Start()
	{
		targets = new Vector3[pointsT.Length];
		for (int i = 0; i < pointsT.Length; i++)
		{
			targets[i] = pointsT[i].position;
		}
		Main.bro = this;
		waitPlayer = false;
		waitTime = 0f;
		status = 1;
		Init();
		door.SetOpen(n: true);
		cloud.SetActive(value: false);
		SetDirect(n: false);
	}

	public void Spawn()
	{
		base.gameObject.tag = "Monster";
		mushBox.enabled = false;
		waitTime = 0f;
		status = 1;
		SetDirect(n: true);
		SetAn("Idle");
		Singleton<ManagerFunctions>.use.addFunction(CutScene1);
		abyUI = Object.Instantiate(Resources.Load("Prefabs/UI/controlsUI"), Main.game.canvasT, instantiateInWorldSpace: false) as GameObject;
		abyUI.SetActive(value: false);
		Main.game.objects.Add(abyUI);
	}

	private void Init()
	{
		posAn.Add("Idle", new Vector2(0f, 0f));
		posAn.Add("Run", new Vector2(-0.197f, 0f));
		posAn.Add("Run2", new Vector2(-0.0637f, 0.02f));
		posAn.Add("Hand", new Vector2(0.239f, 0.014f));
		posAn.Add("Sigh", new Vector2(0.017f, 0.019f));
		posAn.Add("Sigh2", new Vector2(0.017f, 0.019f));
		posAn.Add("Jump", new Vector2(-0.182f, 0.019f));
		posAn.Add("Fall", new Vector2(-0.131f, 0.04f));
		posAn.Add("Mushroom", new Vector2(0.325f, 0f));
		InitMain();
	}

	public void SetButterfly()
	{
		Singleton<ManagerFunctions>.use.removeFunction(IdleTime);
		Singleton<ManagerFunctions>.use.addFunction(CutScene2);
		status = 1;
		waitTime = 0f;
		cloud.SetActive(value: false);
	}

	private void CutScene1()
	{
		if (status == 1)
		{
			waitTime += Time.deltaTime;
			if (waitTime > 0.3f)
			{
				SetAn("Run2");
				status = 2;
			}
		}
		else if (status == 2)
		{
			base.transform.Translate(2f * Vector3.right * Time.deltaTime);
			if (base.transform.position.x > 2.2f)
			{
				status = 3;
				SetAn("Mushroom");
			}
		}
		else if (status == 3)
		{
			if (AnCompleted())
			{
				SetDirect(n: false);
				SetAn("Hand");
				status = 4;
				waitTime = 0f;
			}
		}
		else if (status == 31)
		{
			if (AnCompleted())
			{
				SetDirect(n: false);
				SetAn("Hand");
				status = 4;
				waitTime = 0f;
			}
		}
		else if (status == 4)
		{
			waitTime += Time.deltaTime;
			if (waitTime > 1f)
			{
				cloud.SetActive(value: true);
				status = 5;
			}
		}
		else if (status == 5)
		{
			if (AnCompleted())
			{
				Main.hero.gameObject.SetActive(value: true);
				Main.hero.SetDirect(n: true);
				Main.hero.SetAn(Main.hero.runAn);
				status = 6;
			}
		}
		else if (status == 6)
		{
			Main.hero.transform.Translate(2.5f * Vector3.right * Time.deltaTime);
			if (Main.hero.transform.position.x > -5f)
			{
				Main.hero.SetAn(Main.hero.idleAn);
				Main.hero.stopActions = false;
				SetAn("Idle");
				Singleton<ManagerFunctions>.use.removeFunction(CutScene1);
				Singleton<ManagerFunctions>.use.addFunction(IdleTime);
				waitTime = 0f;
				status = 1;
				mushBox.enabled = true;
				base.gameObject.tag = "Bro";
				Main.game.canShowPauseTab = true;
				abyUI.SetActive(value: true);
				abyUI.GetComponent<ControlsUI>().SetIcon();
			}
		}
	}

	private void IdleTime()
	{
		if (status == 1)
		{
			waitTime += Time.deltaTime;
			if (waitTime > 3f)
			{
				SetAn("Sigh");
				status = 2;
			}
		}
		else if (AnCompleted())
		{
			SetAn("Idle");
			waitTime = 0f;
			status = 1;
		}
	}

	private void CutScene2()
	{
		if (status == 1)
		{
			if (!Main.hero.stopActions && Main.hero.onPlatform)
			{
				status = 2;
				waitTime = 0f;
				Main.hero.stopActions = true;
				Main.hero.SetAn("Idle2");
				butterfly.Spawn();
				Singleton<Sounds>.use.So("butGo");
				abyUI.SetActive(value: false);
			}
		}
		else if (status == 2)
		{
			waitTime += Time.deltaTime;
			if (waitTime > 1f)
			{
				SetAn("Sigh");
				Main.hero.SetDirect(n: true);
				Main.hero.SetAn("SeeUp");
				status = 3;
				waitTime = 0f;
			}
		}
		else if (status == 3)
		{
			waitTime += Time.deltaTime;
			if (waitTime > 1.5f)
			{
				Main.hero.SetAn("SeeUp2");
			}
			if (waitTime > 3.5f)
			{
				status = 4;
				waitTime = 0f;
				SetRun(n: true);
				Singleton<ManagerFunctions>.use.addFunction(Go);
				Singleton<Sounds>.use.So("goToB");
			}
		}
		else if (status == 4)
		{
			waitTime += Time.deltaTime;
			if (waitTime > 2f)
			{
				Main.hero.stopActions = false;
				Main.hero.run = false;
				Main.hero.SetAn("Idle2");
				Main.game.cam.ToFollow(Main.game.hero.heroT);
				Singleton<ManagerFunctions>.use.removeFunction(CutScene2);
			}
		}
	}

	private void Go()
	{
		if (finalGo)
		{
			if (waitPlayer)
			{
				if (Main.hero.onPlatform && Mathf.Abs(Main.hero.heroT.position.x - skinT.position.x) < 2.8f)
				{
					MonoBehaviour.print("goCutScene");
					Main.hero.stopActions = true;
					Main.hero.run = false;
					Main.hero.speedX = 0f;
					Main.hero.SetDirect(n: true);
					Main.hero.SetAn("Watch");
					Singleton<Sounds>.use.So("seeMonster");
					waitPlayer = false;
					run = false;
					butterfly.SetSlowUp();
					butterfly.SetWait(n: false);
				}
			}
			else
			{
				if (butterfly.gameObject.activeSelf)
				{
					return;
				}
				if (!run)
				{
					SetRun(n: true);
					return;
				}
				Run();
				if (skinT.position.x > 63.7f)
				{
					Main.hero.stopActions = false;
					Singleton<Sounds>.use.So("closeDoor1");
					base.gameObject.SetActive(value: false);
					door.SetOpen(n: false);
					Singleton<ManagerFunctions>.use.removeFunction(Go);
				}
			}
			return;
		}
		if (waitPlayer)
		{
			if (Mathf.Abs(Main.hero.heroT.position.x - skinT.position.x) < 4f)
			{
				if (nextPoint + 1 >= targets.Length)
				{
					butterfly.SetSpeedUp();
					butterfly.SetWait(n: true);
					finalGo = true;
					waitPlayer = true;
					SetAn("Sigh2");
				}
				else
				{
					waitPlayer = false;
					butterfly.SetWait(n: false);
					SetRun(n: true);
				}
			}
			return;
		}
		Run();
		CheckGround();
		if (onPlatform && skinT.position.x > targets[nextPoint].x)
		{
			if (Mathf.Abs(Main.hero.heroT.position.x - skinT.position.x) > 5f)
			{
				waitPlayer = true;
				SetAn("Sigh2");
				butterfly.SetWait(n: true);
				return;
			}
			nextPoint++;
			butterfly.SetSpeedUp();
			if (nextPoint >= targets.Length)
			{
				butterfly.SetWait(n: true);
				finalGo = true;
				waitPlayer = true;
				SetAn("Sigh2");
			}
		}
		if (jump)
		{
			jumpTime += Time.deltaTime;
			if (jumpTime > 0.2f && AnCompleted())
			{
				jump = false;
				fall = true;
				SetAn("Fall");
			}
		}
	}

	public void SetJump()
	{
		if (!fall && !jump && onPlatform)
		{
			rb.AddForce(skinT.up * jumpSpeed, ForceMode2D.Impulse);
			onPlatform = false;
			SetAn("Jump");
			jumpTime = 0f;
			jump = true;
			idle = false;
			run = false;
			goY = 0f;
		}
	}

	private void SetDirect(bool n)
	{
		direct = n;
		sp.flipX = !n;
	}
}
