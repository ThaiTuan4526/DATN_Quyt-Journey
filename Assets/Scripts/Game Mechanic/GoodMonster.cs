using System.Collections.Generic;
using UnityEngine;

public class GoodMonster : MonoBehaviour
{
	public Animator an;

	public SpriteRenderer sp;

	public GameObject skinObj;

	public GameObject rb;

	public Monster8 monster8;

	public Girl girl;

	public GameObject block;

	public GameObject butterflyEnd;

	public GameObject growl;

	internal bool onFloor2;

	private float waitTime;

	private float speed;

	private bool direct;

	private int status;

	private string myAn;

	internal bool chainMode;

	private Dictionary<string, Vector2> posAn = new Dictionary<string, Vector2>();

	internal bool transAn;

	internal LayerMask blockRightLayer = 2048;

	internal LayerMask blockLeftLayer = 4096;

	protected Collider2D[] colliders;

	private bool canRun = true;

	private bool goDown;

	private Vector2 posGo;

	private Vector2 posHero;

	private float timeGo;

	private float speedX;

	private float suffocationTime;

	private void Start()
	{
		InitAn();
		block.SetActive(value: false);
		butterflyEnd.SetActive(value: false);
		suffocationTime = 0f;
		SetDirect(n: false);
		SetAn("Idle1");
	}

	private void InitAn()
	{
		posAn.Add("Run", new Vector2(-0.077f, 0.005f));
		posAn.Add("Idle1", new Vector2(0.751f, -0.904f));
		posAn.Add("Idle2", new Vector2(0f, 0f));
		posAn.Add("Eat", new Vector2(0.215f, 0.019f));
		posAn.Add("Hold", new Vector2(0.812f, 0.321f));
		posAn.Add("Attack1", new Vector2(0.672f, 0.294f));
		posAn.Add("Attack2", new Vector2(0f, 0f));
		posAn.Add("Power", new Vector2(0.044f, 0.259f));
		posAn.Add("Spit", new Vector2(0.947f, -0.906f));
		posAn.Add("Open", new Vector2(0.947f, -0.906f));
		posAn.Add("Close", new Vector2(0.947f, -0.906f));
		posAn.Add("Stroke", new Vector2(0.752f, -0.902f));
		posAn.Add("Stroke2", new Vector2(0.752f, -0.902f));
		posAn.Add("Trans", new Vector2(0.306f, 0.166f));
		posAn.Add("Kill1", new Vector2(0.722f, 0.376f));
		posAn.Add("Kill2", new Vector2(-0.329f, 0.089f));
		posAn.Add("Join", new Vector2(0.988f, 0.509f));
	}

	private void ClassicActions()
	{
		if (goDown)
		{
			if (status == 10)
			{
				base.transform.position = new Vector2(base.transform.position.x + speedX * 60f * Time.deltaTime, base.transform.position.y);
				if (Mathf.Abs(-1.74f - base.transform.position.x) < 0.15f)
				{
					status = 1;
					timeGo = 0f;
					posHero = base.transform.position;
					posGo = new Vector2(0.57f, 7.38f);
					sp.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
					SetDirect(n: true);
				}
			}
			else if (status == 1)
			{
				base.transform.position = Vector2.Lerp(posHero, posGo, timeGo);
				timeGo += Time.deltaTime * 1.5f;
				if (timeGo >= 1f)
				{
					timeGo = 0f;
					status = 2;
					Main.game.cam.GoToPos(new Vector3(Main.game.cam.transform.position.x, 2.36f, -10f), 0.02f);
					SetDirect(n: false);
					base.transform.position = new Vector3(0.22f, 1.1f);
					posHero = base.transform.position;
					posGo = new Vector2(-1.74f, -0.38f);
				}
			}
			else if (status == 2)
			{
				base.transform.position = Vector2.Lerp(posHero, posGo, timeGo);
				timeGo += Time.deltaTime * 1.7f;
				if (timeGo >= 1f)
				{
					goDown = false;
					canRun = true;
					onFloor2 = true;
					sp.maskInteraction = SpriteMaskInteraction.None;
					Main.game.cam.isFollow = true;
					block.SetActive(value: true);
				}
			}
			return;
		}
		if (canRun)
		{
			float num = Input.GetAxis("Horizontal") + Input.GetAxis("HorizontalJoy");
			speedX = 0f;
			if (Mathf.Abs(num) > 0f)
			{
				if (num > 0f)
				{
					speedX = 0.07f;
					direct = true;
					sp.flipX = false;
				}
				else
				{
					speedX = -0.07f;
					direct = false;
					sp.flipX = true;
				}
			}
			if (Mathf.Abs(speedX) > 0f)
			{
				if ((speedX < 0f && !OnBlock(blockLeftLayer)) || (speedX > 0f && !OnBlock(blockRightLayer)))
				{
					SetAn("Run");
					base.transform.position = new Vector2(base.transform.position.x + speedX * 60f * Time.deltaTime, base.transform.position.y);
				}
				else
				{
					SetAn("Idle2");
				}
			}
			else
			{
				SetAn("Idle2");
			}
		}
		if (!Input.GetButtonDown("Action"))
		{
			return;
		}
		if (onFloor2 && base.transform.position.x >= 16f)
		{
			Singleton<ManagerFunctions>.use.addFunction(Kill2);
			Singleton<ManagerFunctions>.use.removeFunction(ClassicActions);
			SetDirect(n: true);
			SetAn("Kill2");
			status = 1;
			girl.gameObject.SetActive(value: false);
		}
		if (!onFloor2 && base.transform.position.x >= -3f && base.transform.position.x < 0f)
		{
			canRun = false;
			goDown = true;
			status = 10;
			if (base.transform.position.x < -1.74f)
			{
				SetDirect(n: true);
				speedX = 0.05f;
			}
			else
			{
				SetDirect(n: false);
				speedX = -0.05f;
			}
			SetAn("Run");
		}
	}

	private void Kill1()
	{
		if (status == 1)
		{
			if (myAn == "Attack1" && AnCompleted())
			{
				SetAn("Hold");
			}
			if (!(myAn == "Hold"))
			{
				return;
			}
			suffocationTime += Time.deltaTime;
			if (Input.GetButtonDown("Action"))
			{
				SetAn("Kill1");
				status = 2;
				Singleton<Sounds>.use.So("neckCrack");
				if (suffocationTime > 10f)
				{
					Singleton<SteamF>.use.AddAch(12);
				}
			}
		}
		else if (status == 2 && AnCompleted())
		{
			SetAn("Idle2");
			Singleton<ManagerFunctions>.use.removeFunction(Kill1);
			Singleton<ManagerFunctions>.use.addFunction(ClassicActions);
			canRun = true;
		}
	}

	private void Kill2()
	{
		if (status == 1)
		{
			if (an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.88f)
			{
				butterflyEnd.SetActive(value: true);
				butterflyEnd.transform.position = new Vector2(base.transform.position.x + 0.5f, base.transform.position.y + 1.6f);
			}
			if (AnCompleted())
			{
				sp.sortingLayerName = "gameobj";
				waitTime = 0f;
				status = 2;
			}
		}
		else
		{
			waitTime += Time.deltaTime;
			if (waitTime >= 3f)
			{
				Main.hero.ReturnToGame();
				Main.game.cam.GoToPos(new Vector3(Main.game.cam.transform.position.x, 4.82f, -10f), 0.02f);
				Singleton<ManagerFunctions>.use.removeFunction(Kill2);
				block.SetActive(value: false);
				Main.game.cam.ToFollow(Main.hero.transform);
			}
		}
	}

	private void StrokeEnd()
	{
		waitTime += Time.deltaTime;
		if (waitTime > 5f)
		{
			growl.SetActive(value: true);
			SetAn("Stroke2");
			Singleton<ManagerFunctions>.use.removeFunction(StrokeEnd);
		}
	}

	private void Power()
	{
		if (status == 1)
		{
			waitTime += Time.deltaTime;
			if (waitTime > 1f)
			{
				Main.hero.SetDirect(n: true);
				Main.hero.SetAn("Watch");
				SetAn("Power");
				status = 2;
				Singleton<Sounds>.use.So("roar");
				Singleton<Sounds>.use.So("com2_zvuk");
				sp.sortingOrder = 300;
			}
		}
		else if (status == 2)
		{
			if (chainMode)
			{
				base.transform.Translate(Vector3.down * Time.deltaTime * 5f);
				if (base.transform.position.y < 5.88f)
				{
					base.transform.position = new Vector2(base.transform.position.x, 5.88f);
					if (base.transform.position.y > 5.88f)
					{
						base.transform.position = new Vector2(base.transform.position.x, 5.88f);
					}
				}
				base.transform.Translate(Vector3.left * Time.deltaTime * 5f);
				if (base.transform.position.x < 14.975f)
				{
					chainMode = false;
					base.transform.position = new Vector2(14.975f, base.transform.position.y);
				}
			}
			if (AnCompleted())
			{
				status = 3;
				SetAn("Join");
				sp.gameObject.transform.localPosition = new Vector2(-0.018f, 0.096f);
				Main.hero.gameObject.SetActive(value: false);
				Singleton<InputChecker>.use.SetVibro(4f);
			}
		}
		else if (status == 3 && AnCompleted())
		{
			Main.game.cam.ToFollow(base.transform);
			Singleton<InputChecker>.use.StopVibro();
			Singleton<ManagerFunctions>.use.addFunction(ClassicActions);
			Singleton<ManagerFunctions>.use.removeFunction(Power);
			SetAn("Idle2");
			Main.hero.gameObject.transform.position = new Vector2(12f, Main.hero.gameObject.transform.position.y);
			Main.hero.gameObject.SetActive(value: true);
			Main.hero.SetDirect(n: true);
			Main.hero.SetAn("Connect");
		}
	}

	private void Transmutate()
	{
		if (status == 1)
		{
			waitTime += Time.deltaTime;
			if (waitTime > 2f)
			{
				SetAn("Trans");
				status = 2;
				Singleton<Sounds>.use.So("transmutate");
			}
		}
		else
		{
			if (status != 2)
			{
				return;
			}
			if (chainMode)
			{
				sp.sortingLayerName = "hero";
				sp.sortingOrder = -1;
				sp.gameObject.layer = 8;
				if (base.transform.position.x > 15.325001f)
				{
					base.transform.Translate(Time.deltaTime * 2f * Vector3.left);
					if (base.transform.position.x <= 15.325001f)
					{
						base.transform.position = new Vector2(15.325001f, base.transform.position.y);
					}
				}
				rb.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.5f, 5.1f);
				if (rb.transform.position.y > 7.6990004f)
				{
					base.transform.position = new Vector2(15.325001f, base.transform.position.y);
					chainMode = false;
					rb.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					rb.GetComponent<Rigidbody2D>().isKinematic = true;
				}
			}
			if (AnCompleted())
			{
				girl.SetSecondLoop();
				transAn = false;
				SetAn("Idle2");
				Singleton<ManagerFunctions>.use.removeFunction(Transmutate);
			}
		}
	}

	private void FeedFail()
	{
		if (Main.hero.AnCompleted())
		{
			SetAn("Idle2");
			Singleton<ManagerFunctions>.use.removeFunction(FeedFail);
		}
	}

	private void TransFail()
	{
		if (AnCompleted())
		{
			SetAn("Idle1");
			Singleton<ManagerFunctions>.use.removeFunction(TransFail);
		}
	}

	public void SetCare()
	{
		SetAn("Stroke");
		waitTime = 0f;
		growl.SetActive(value: false);
		Singleton<ManagerFunctions>.use.removeFunction(StrokeEnd);
		Singleton<ManagerFunctions>.use.addFunction(StrokeEnd);
	}

	public void Feed(bool n)
	{
		SetAn("Eat");
		if (n)
		{
			waitTime = 0f;
			chainMode = false;
			status = 1;
			Singleton<ManagerFunctions>.use.addFunction(Power);
			Singleton<ManagerFunctions>.use.removeFunction(StrokeEnd);
		}
		else
		{
			Singleton<ManagerFunctions>.use.addFunction(FeedFail);
		}
	}

	public void SetTrans(bool n)
	{
		if (n)
		{
			growl.SetActive(value: false);
			SetAn("Close");
			Singleton<ManagerFunctions>.use.addFunction(Transmutate);
			Singleton<ManagerFunctions>.use.removeFunction(StrokeEnd);
			Singleton<ManagerFunctions>.use.removeFunction(TransFail);
			waitTime = 0f;
			status = 1;
			chainMode = false;
			transAn = true;
		}
		else
		{
			SetAn("Spit");
			Singleton<ManagerFunctions>.use.addFunction(TransFail);
		}
		Singleton<Sounds>.use.So("chew");
	}

	public void ShowMonsterDead()
	{
		monster8.gameObject.SetActive(value: true);
		monster8.gameObject.transform.position = new Vector2(base.transform.position.x + 1.74f, base.transform.position.y + 0.167f);
		monster8.SetAn("Dead");
		monster8.sp.sortingOrder = -1;
	}

	public bool OnBlock(LayerMask n)
	{
		colliders = Physics2D.OverlapCircleAll(base.transform.position, 0.4f, n);
		if (colliders.Length != 0 && isBlockOnHero())
		{
			return true;
		}
		return false;
	}

	public void Attack1()
	{
		SetDirect(n: true);
		canRun = false;
		SetAn("Attack1");
		Singleton<ManagerFunctions>.use.addFunction(Kill1);
		Singleton<ManagerFunctions>.use.removeFunction(ClassicActions);
		status = 1;
		Singleton<Sounds>.use.So("attackMonster1");
	}

	private bool isBlockOnHero()
	{
		float num = base.transform.position.y + 0.8f;
		for (int i = 0; i < colliders.Length; i++)
		{
			if (base.transform.position.y < colliders[i].transform.position.y + colliders[i].transform.localScale.y / 2f && num > colliders[i].transform.position.y - colliders[i].transform.localScale.y / 2f)
			{
				return true;
			}
		}
		return false;
	}

	public void SetAn(string n)
	{
		if (!(myAn != n))
		{
			return;
		}
		an.Play(n, -1, 0f);
		myAn = n;
		if (posAn.ContainsKey(n))
		{
			Vector2 vector = posAn[n];
			if (sp.flipX)
			{
				vector.x *= -1f;
			}
			skinObj.transform.localPosition = vector;
		}
	}

	private void SetDirect(bool n)
	{
		direct = n;
		sp.flipX = !n;
	}

	public bool AnCompleted()
	{
		return an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;
	}
}
