using UnityEngine;

public class Lift : MonoBehaviour
{
	public Animator leverAn;

	public Animator mechAn;

	public Leve72start leve72Start;

	public BoxCollider2D leverBox;

	private AudioSource liftSo;

	internal bool canUse;

	private float max = 3.231f;

	private float min = -3.55f;

	private float speed = 2f;

	internal bool downPos;

	private float waitTime;

	private bool go;

	private bool needBack;

	private void Start()
	{
		mechAn.speed = 0f;
		downPos = false;
		canUse = false;
		liftSo = base.gameObject.GetComponent<AudioSource>();
	}

	public void ToGo(bool n = true)
	{
		needBack = n;
		if (canUse)
		{
			if (n)
			{
				leverAn.Play("Lever");
			}
			leverBox.enabled = false;
			Singleton<ManagerFunctions>.use.addFunction(Go);
			if (downPos)
			{
				speed = 2f;
				leve72Start.SetBack();
			}
			else
			{
				speed = -2f;
			}
			go = false;
			waitTime = 0f;
		}
		else
		{
			leverAn.Play("Lever2");
			Singleton<ManagerFunctions>.use.addFunction(EndLever);
		}
	}

	private void EndLever()
	{
		if (leverAn.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
		{
			leverAn.Play("Idle");
			Singleton<ManagerFunctions>.use.removeFunction(EndLever);
			Main.hero.ReturnToGame();
		}
	}

	private void Go()
	{
		if (!go)
		{
			waitTime += Time.deltaTime;
			if (waitTime > 1f)
			{
				if (downPos)
				{
					mechAn.Play("Up", -1, 1f - mechAn.GetCurrentAnimatorStateInfo(0).normalizedTime);
				}
				else
				{
					mechAn.Play("Down");
				}
				liftSo.Play();
				mechAn.speed = 1f;
				go = true;
			}
			return;
		}
		base.transform.Translate(speed * Vector3.up * Time.deltaTime);
		if (needBack)
		{
			Main.hero.transform.Translate(speed * Vector3.up * Time.deltaTime);
			Main.game.cam.checkYpos = 3f;
		}
		if ((downPos && base.transform.position.y > max) || (!downPos && base.transform.position.y < min))
		{
			liftSo.Stop();
			if (downPos)
			{
				base.transform.position = new Vector2(base.transform.position.x, max);
			}
			else
			{
				base.transform.position = new Vector2(base.transform.position.x, min);
			}
			leverBox.enabled = true;
			Singleton<ManagerFunctions>.use.removeFunction(Go);
			downPos = !downPos;
			mechAn.speed = 0f;
			if (needBack)
			{
				Main.hero.ReturnToGame();
				leverAn.Play("Back");
			}
		}
	}
}
