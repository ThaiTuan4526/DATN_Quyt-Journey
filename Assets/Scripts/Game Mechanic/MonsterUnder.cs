using UnityEngine;

public class MonsterUnder : NPCBase, InterfaceBreakObj
{
	internal bool isSleep;

	public GameObject leafRb;

	public GameObject inHiddenSkin;

	public GameObject leafSkin;

	public Tree tree;

	private int status;

	private float waitTime;

	private Vector3 defaultPos;

	public bool attackMode;

	internal bool dead;

	private AudioSource audioSource;

	private void Start()
	{
		Main.game.arrBreakObj.Add(this);
		isSleep = true;
		maxSpeed = 0.07f;
		posAn.Add("Awake", new Vector2(-0.154f, 0.09f));
		posAn.Add("Run", new Vector2(0f, 0f));
		posAn.Add("Attack", new Vector2(-0.086f, 0f));
		posAn.Add("Attack2", new Vector2(0.228f, -0.222f));
		posAn.Add("Jump", new Vector2(-0.06f, 0.015f));
		posAn.Add("Fall", new Vector2(0f, 0f));
		posAn.Add("Fall2", new Vector2(-0.284f, -0.019f));
		posAn.Add("Sleep", new Vector2(-0.142f, -0.352f));
		inHiddenSkin.SetActive(value: false);
		dead = false;
		leafSkin.SetActive(value: false);
		InitMain();
		audioSource = base.gameObject.GetComponent<AudioSource>();
		defaultPos = skinT.position;
		SetAn("Sleep");
	}

	public void BreakObj()
	{
		if (!dead)
		{
			boxCollider2D.enabled = true;
			circleCollider2D.enabled = true;
			Singleton<InputChecker>.use.StopVibro();
			rb.velocity = Vector2.zero;
			rb.angularVelocity = 0f;
			audioSource.Play();
			base.gameObject.SetActive(value: true);
			isSleep = true;
			attackMode = false;
			skinT.position = defaultPos;
			direct = true;
			sp.flipX = !direct;
			SetAn("Sleep");
			Singleton<ManagerFunctions>.use.removeFunction(WakeUp);
			Singleton<ManagerFunctions>.use.removeFunction(Go);
			Singleton<ManagerFunctions>.use.removeFunction(Attack);
			Singleton<ManagerFunctions>.use.removeFunction(Fallen);
		}
	}

	public void WakeUpMonster()
	{
		if (isSleep)
		{
			audioSource.Stop();
			isSleep = false;
			Singleton<ManagerFunctions>.use.addFunction(WakeUp);
			SetAn("Awake");
			waitTime = 0f;
		}
	}

	public void LeafFall()
	{
		if (direct && onPlatform && !dead && !attackMode)
		{
			Singleton<InputChecker>.use.StopVibro();
			leafSkin.SetActive(value: true);
			leafSkin.GetComponent<Animator>().Play("Fall");
			leafRb.SetActive(value: false);
			rb.isKinematic = true;
			boxCollider2D.enabled = false;
			circleCollider2D.enabled = false;
			SetAn("Fall2");
			dead = true;
			waitTime = 1f;
			status = 1;
			Singleton<Sounds>.use.So("leafCrash");
			Singleton<ManagerFunctions>.use.addFunction(Fallen);
			Singleton<ManagerFunctions>.use.removeFunction(WakeUp);
			Singleton<ManagerFunctions>.use.removeFunction(Go);
			Singleton<ManagerFunctions>.use.removeFunction(Attack);
			tree.MonsterDead();
		}
	}

	public void SetJump(bool n = false)
	{
		if (!jump && !attackMode && onPlatform)
		{
			if (!n)
			{
				rb.AddForce(skinT.up * 6.5f, ForceMode2D.Impulse);
			}
			else
			{
				rb.AddForce(skinT.up * 8.5f, ForceMode2D.Impulse);
			}
			onPlatform = false;
			jumpTime = 0f;
			SetDirectAn("Jump");
			jump = true;
			idle = false;
			run = false;
			boxCollider2D.enabled = false;
			circleCollider2D.enabled = false;
		}
	}

	public void FindBoy()
	{
		if (skinT.position.x <= Main.hero.heroT.position.x)
		{
			SetDirectRun(n: true);
		}
		else if (skinT.position.x > Main.hero.heroT.position.x)
		{
			SetDirectRun(n: false);
		}
	}

	public void SetDirectRun(bool n)
	{
		if (!attackMode)
		{
			if (n)
			{
				goX = 1f;
				direct = true;
			}
			else
			{
				goX = -1f;
				direct = false;
			}
			sp.flipX = !direct;
		}
	}

	private void WakeUp()
	{
		waitTime += Time.deltaTime;
		if (waitTime > 1f && AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(WakeUp);
			Singleton<ManagerFunctions>.use.addFunction(Go);
			if (skinT.position.x <= Main.hero.heroT.position.x)
			{
				SetRun(n: true);
			}
			else if (skinT.position.x > Main.hero.heroT.position.x)
			{
				SetRun(n: false);
			}
		}
	}

	private void Go()
	{
		Run();
		CheckGround();
		float num = 0.05f + 0.3f * (5f / (5f * Vector2.Distance(skinT.position, Main.hero.heroT.position)));
		if (num > 0.3f)
		{
			num = 0.3f;
		}
		Singleton<InputChecker>.use.SetSpecialVibro(num);
		if (Mathf.Abs(skinT.position.x - Main.hero.heroT.position.x) < 2f && Mathf.Abs(skinT.position.y - Main.hero.heroT.position.y) < 0.65f)
		{
			if (onPlatform)
			{
				rb.AddForce(skinT.up * 3.5f, ForceMode2D.Impulse);
			}
			Singleton<Sounds>.use.So("underAttack");
			SetAn("Attack");
			Singleton<ManagerFunctions>.use.removeFunction(Go);
			Singleton<ManagerFunctions>.use.addFunction(Attack);
			status = 1;
			FindBoy();
			attackMode = true;
			Main.hero.speedX = 0f;
			Main.hero.stopActions = true;
			boxCollider2D.enabled = true;
			circleCollider2D.enabled = true;
		}
	}

	private void Attack()
	{
		if (Mathf.Abs(skinT.position.x - Main.hero.heroT.position.x) > 0.1f)
		{
			Run();
		}
		if (status == 1)
		{
			if (Mathf.Abs(skinT.position.x - Main.hero.heroT.position.x) < 1f || AnCompleted())
			{
				Main.hero.gameObject.SetActive(value: false);
				SetAn("Attack2");
				Singleton<Sounds>.use.So("isDead");
				status = 2;
				waitTime = 0f;
			}
		}
		else
		{
			waitTime += Time.deltaTime;
			if (waitTime > 1f)
			{
				Singleton<ManagerFunctions>.use.removeFunction(Attack);
				Main.game.ReLoadScene();
			}
		}
	}

	private void Fallen()
	{
		if (status == 1)
		{
			skinT.position = new Vector3(skinT.position.x, skinT.position.y - 0.05f * waitTime * 60f * Time.deltaTime, 0f);
			waitTime += 0.03f;
			if (skinT.position.y < -8f)
			{
				status = 2;
				waitTime = 0f;
				Singleton<Sounds>.use.So("fallenUnder");
				inHiddenSkin.SetActive(value: true);
				inHiddenSkin.GetComponent<GremlinInTrash>().Init();
				base.gameObject.SetActive(value: false);
			}
		}
		else if (status == 2)
		{
			waitTime += Time.deltaTime;
			if (waitTime > 5f)
			{
				leafRb.SetActive(value: true);
				Singleton<ManagerFunctions>.use.removeFunction(Fallen);
			}
		}
	}
}
