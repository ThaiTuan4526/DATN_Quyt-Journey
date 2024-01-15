using System.Collections.Generic;
using UnityEngine;

public class NPCBase : MonoBehaviour
{
    #region NPC Variables
    private float acpX = 0.003f;

	private float goYkUp = 0.1f;

	private float goYkDown = 0.1f;

	protected float goX;

	protected internal bool direct;

	protected bool idle;

	protected bool run;

	protected bool boost;

	protected float goY;

	protected int iC;

	protected float speedX;

	protected Rigidbody2D rb;

	protected Vector3 v3;

	internal string myAn = "";

	internal Animator an;

	private GameObject skinObj;

	protected bool onPlatform;

	private float nextStepSo;

	protected bool jump;

	private bool someAn;

	protected bool fall;

	protected SpriteRenderer sp;

	protected LayerMask groundLayer = 1024;

	protected LayerMask blockRightLayer = 2048;

	protected LayerMask blockLeftLayer = 4096;

	protected LayerMask blockBoxLayer = 16384;

	protected Collider2D[] colliders;

	protected Dictionary<string, Vector2> posAn = new Dictionary<string, Vector2>();

	protected float maxSpeed = 0.09f;

	protected Transform skinT;

	private int nextStepSoID = 3;

	protected float jumpTime;

	protected BoxCollider2D boxCollider2D;

	protected CircleCollider2D circleCollider2D;
    #endregion

    protected void InitMain()
	{
		skinT = base.gameObject.transform;
		skinObj = skinT.GetChild(0).gameObject.transform.GetChild(0).gameObject;
		rb = base.gameObject.GetComponent<Rigidbody2D>();
		sp = skinObj.GetComponent<SpriteRenderer>();
		an = skinObj.GetComponent<Animator>();
		boxCollider2D = base.gameObject.GetComponent<BoxCollider2D>();
		circleCollider2D = base.gameObject.GetComponent<CircleCollider2D>();
	}

	protected void Run()
	{
		if (boost)
		{
			speedX += goX * 0.3f * Time.deltaTime;
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
			if ((speedX < 0f && !OnBlock(blockLeftLayer)) || (speedX > 0f && !OnBlock(blockRightLayer)))
			{
				skinT.position = new Vector2(skinT.position.x + speedX * 60f * Time.deltaTime, skinT.position.y - goY * 60f * Time.deltaTime);
			}
			else
			{
				speedX = 0f;
				SetIdle();
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
				boxCollider2D.enabled = true;
				circleCollider2D.enabled = true;
			}
		}
	}

	private void SetIdle()
	{
		if (onPlatform && !idle)
		{
			idle = true;
			run = false;
			SetAn("Idle");
		}
	}

	protected void SetRun(bool n)
	{
		SetAn("Run");
		run = true;
		idle = false;
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
		boost = true;
		speedX = 0f;
	}

	protected void CheckGround()
	{
		colliders = Physics2D.OverlapCircleAll(skinT.position, 0.05f, groundLayer);
		goY = 0f;
		if (colliders.Length != 0)
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
			onPlatform = true;
			if (!jump)
			{
				SetAn("Run");
			}
			fall = false;
		}
		else
		{
			onPlatform = false;
			if (!fall && rb.velocity.y < -1.5f)
			{
				SetAn("Fall");
				idle = false;
				fall = true;
				run = false;
			}
		}
	}

	private bool isBlockOnHero()
	{
		float num = skinT.position.y + 0.8f;
		for (int i = 0; i < colliders.Length; i++)
		{
			if (skinT.position.y < colliders[i].transform.position.y + colliders[i].transform.localScale.y / 2f && num > colliders[i].transform.position.y - colliders[i].transform.localScale.y / 2f)
			{
				iC = i;
				return true;
			}
		}
		return false;
	}

	private bool OnBlock(LayerMask n)
	{
		v3 = skinT.position;
		v3.y += 0.6f;
		colliders = Physics2D.OverlapCircleAll(v3, 0.4f, blockBoxLayer);
		if (colliders.Length != 0 && isBlockOnHero() && ((speedX < 0f && skinT.position.x > colliders[iC].transform.position.x) || (speedX > 0f && skinT.position.x < colliders[iC].transform.position.x)))
		{
			return true;
		}
		colliders = Physics2D.OverlapCircleAll(skinT.position, 0.4f, n);
		if (colliders.Length != 0 && isBlockOnHero())
		{
			if (onPlatform)
			{
				idle = true;
				SetAn("Idle");
			}
			return true;
		}
		return false;
	}

	protected void SetAn(string n)
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

	public void SetDirectAn(string n)
	{
		myAn = "";
		SetAn(n);
	}

	protected bool AnCompleted()
	{
		return an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;
	}
}
