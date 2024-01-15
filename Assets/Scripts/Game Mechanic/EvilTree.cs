using UnityEngine;

public class EvilTree : MonoBehaviour, InterfaceBreakObj
{
	public PipeAby pipe;

	public Animator anFace;

	public Animator anHand1;

	public Animator anHand2;

	public GameObject happyFace;

	public Fruit[] fruits;

	private bool attackMode;

	private bool catched;

	private float waitTime;

	private bool isHappy;

	private void Start()
	{
		happyFace.SetActive(value: false);
		Main.game.arrBreakObj.Add(this);
		Singleton<ManagerFunctions>.use.addFunction(Patrol);
		attackMode = false;
		Fruit[] array = fruits;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].tree = this;
		}
	}

	public void BreakObj()
	{
		Main.game.cam.isFollow = true;
		attackMode = false;
		catched = false;
		anFace.Play("Idle");
		anHand1.Play("Idle");
		anHand2.Play("Idle");
	}

	private void Patrol()
	{
		if (isHappy)
		{
			if (!attackMode)
			{
				if (Main.hero.heroT.position.x >= 57.224f)
				{
					attackMode = true;
					waitTime = 0f;
					anHand2.Play("Catch2");
					happyFace.GetComponent<SpriteRenderer>().flipX = true;
					Main.hero.stopActions = true;
					Main.hero.rb.velocity = Vector2.zero;
					Main.hero.jump = false;
					Main.hero.fall = false;
					Main.hero.SetDirect(n: true);
					Main.hero.SetAn("Fear");
				}
			}
			else
			{
				waitTime += Time.deltaTime;
				if (waitTime > 1f && anHand2.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
				{
					happyFace.GetComponent<SpriteRenderer>().flipX = false;
					pipe.SetDisable();
					Main.hero.ReturnToGame();
					Singleton<ManagerFunctions>.use.removeFunction(Patrol);
				}
			}
		}
		else if (!attackMode)
		{
			if (Main.hero.heroT.position.x > 47.35f)
			{
				attackMode = true;
				waitTime = 0f;
				Main.game.cam.isFollow = false;
				anHand1.Play("Catch");
				anFace.Play("Angry");
				Main.hero.stopActions = true;
				Main.hero.rb.velocity = Vector2.zero;
				Main.hero.jump = false;
				Main.hero.fall = false;
				Main.hero.SetDirect(n: true);
				Main.hero.SetAn("Fear");
			}
		}
		else if (!catched)
		{
			waitTime += Time.deltaTime;
			if (Main.game.cam.transform.position.x > 38.2f && waitTime > 2.35f)
			{
				Main.game.cam.transform.Translate(Vector3.left * Time.deltaTime * 29f);
			}
			if (waitTime > 1f && anHand1.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && waitTime > 6f)
			{
				Main.game.ReLoadScene();
				catched = true;
			}
		}
	}

	private void SetHappy(bool n)
	{
		isHappy = n;
		happyFace.SetActive(n);
		anFace.gameObject.SetActive(!n);
	}

	public void SetFruit()
	{
		Fruit[] array = fruits;
		for (int i = 0; i < array.Length; i++)
		{
			if (!array[i].IsGood())
			{
				SetHappy(n: false);
				return;
			}
		}
		SetHappy(n: true);
	}
}
