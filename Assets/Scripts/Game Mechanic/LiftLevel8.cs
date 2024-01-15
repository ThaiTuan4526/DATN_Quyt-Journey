using UnityEngine;

public class LiftLevel8 : MonoBehaviour
{
	public Animator leverAn;

	public Animator mechAn;

	private AudioSource audioSource;

	internal bool canUse;

	private float max = 3.231f;

	private float min = -19.185f;

	private float speed;

	internal bool downPos;

	private float waitTime;

	private bool go;

	private const float maxSpeed = -1.6f;

	private void Start()
	{
		mechAn.speed = 0f;
		downPos = false;
		canUse = false;
		audioSource = GetComponent<AudioSource>();
	}

	public void ToGo()
	{
		if (canUse && !downPos)
		{
			leverAn.Play("Lever");
			Singleton<ManagerFunctions>.use.addFunction(Go);
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
				mechAn.Play("Down");
				audioSource.Play();
				mechAn.speed = 0f;
				speed = -0.8f;
				go = true;
				Main.game.cam.followY = true;
				Main.game.cam.SwitchOffsetY(0f);
			}
			return;
		}
		if (mechAn.speed < 1f)
		{
			mechAn.speed += 1.5f * Time.deltaTime;
		}
		if (speed > -1.6f)
		{
			speed *= 1.1f;
			if (speed < -1.6f)
			{
				speed = -1.6f;
			}
		}
		base.transform.Translate(speed * Vector3.up * Time.deltaTime);
		Main.hero.transform.Translate(speed * Vector3.up * Time.deltaTime);
		Main.game.cam.checkYpos = 3f;
		if ((downPos && base.transform.position.y > max) || (!downPos && base.transform.position.y < min))
		{
			if (downPos)
			{
				base.transform.position = new Vector2(base.transform.position.x, max);
			}
			else
			{
				base.transform.position = new Vector2(base.transform.position.x, min);
			}
			Singleton<ManagerFunctions>.use.removeFunction(Go);
			downPos = !downPos;
			mechAn.speed = 0f;
			Main.hero.ReturnToGame();
			leverAn.Play("Back");
		}
	}
}
