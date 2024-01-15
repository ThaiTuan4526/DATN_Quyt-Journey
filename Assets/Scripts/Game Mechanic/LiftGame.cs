using UnityEngine;

public class LiftGame : MonoBehaviour
{
	public int liftID;

	public Animator mechAn;

	private float[] pos;

	private float speed = 2f;

	private int step;

	private float waitTime;

	internal bool go;

	private bool isMy;

	private void Start()
	{
		pos = new float[9];
		mechAn.speed = 0f;
		for (int i = 0; i < 8; i++)
		{
			pos[i] = -4.07f + 0.75f * (float)(1 + i);
		}
		pos[8] = 2.96f;
	}

	public void ToGo(bool n, int stepGo, int stepID)
	{
		step = stepGo;
		if (n)
		{
			speed = 2f;
		}
		else
		{
			speed = -2f;
		}
		if (stepID == liftID)
		{
			isMy = true;
			waitTime = 0f;
		}
		else
		{
			isMy = false;
			waitTime = 0.3f + 1f * Random.value;
		}
		go = true;
		Singleton<ManagerFunctions>.use.removeFunction(Go);
		Singleton<ManagerFunctions>.use.addFunction(Go);
	}

	private void Go()
	{
		waitTime -= Time.deltaTime;
		if (waitTime <= 0f)
		{
			base.transform.Translate(speed * Vector3.up * Time.deltaTime);
			mechAn.speed = 1f;
			if (isMy)
			{
				Main.hero.transform.Translate(speed * Vector3.up * Time.deltaTime);
			}
			if ((speed > 0f && base.transform.position.y >= pos[step]) || (speed < 0f && base.transform.position.y <= -4.07f))
			{
				mechAn.speed = 0f;
				go = false;
				Singleton<ManagerFunctions>.use.removeFunction(Go);
			}
		}
	}
}
