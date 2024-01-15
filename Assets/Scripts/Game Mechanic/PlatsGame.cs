using UnityEngine;

public class PlatsGame : MonoBehaviour
{
	public LiftGame[] lifts;

	public int step;

	internal bool isGame;

	public Transform gear1;

	public Transform gear2;

	public Transform gear3;

	public Transform gear4;

	public Transform gear5;

	public Transform gear6;

	public Transform gear7;

	private AudioSource audioSource;

	private bool toRotate;

	private bool toBack;

	private int stepsRotate;

	private float rotateTime;

	private bool waitOnGround;

	private bool moveOnLifts;

	private bool moveToDown;

	private int prevStep;

	private bool winGo;

	private int[] rightSteps;

	private int timeOfBreak;

	private void Start()
	{
		isGame = false;
		audioSource = GetComponent<AudioSource>();
		timeOfBreak = 0;
		rightSteps = new int[9];
		float value = Random.value;
		if (value < 0.4f)
		{
			rightSteps[0] = 2;
			rightSteps[1] = 3;
			rightSteps[2] = 2;
			rightSteps[3] = 1;
			rightSteps[4] = 2;
			rightSteps[5] = 3;
			rightSteps[6] = 4;
			rightSteps[7] = 3;
			rightSteps[8] = 4;
		}
		else if (value < 0.6f)
		{
			rightSteps[0] = 3;
			rightSteps[1] = 4;
			rightSteps[2] = 3;
			rightSteps[3] = 2;
			rightSteps[4] = 1;
			rightSteps[5] = 2;
			rightSteps[6] = 1;
			rightSteps[7] = 2;
			rightSteps[8] = 1;
		}
		else if (value < 0.8f)
		{
			rightSteps[0] = 1;
			rightSteps[1] = 2;
			rightSteps[2] = 3;
			rightSteps[3] = 2;
			rightSteps[4] = 1;
			rightSteps[5] = 2;
			rightSteps[6] = 3;
			rightSteps[7] = 4;
			rightSteps[8] = 3;
		}
		else
		{
			rightSteps[0] = 4;
			rightSteps[1] = 3;
			rightSteps[2] = 4;
			rightSteps[3] = 3;
			rightSteps[4] = 2;
			rightSteps[5] = 1;
			rightSteps[6] = 2;
			rightSteps[7] = 1;
			rightSteps[8] = 2;
		}
		Singleton<ManagerFunctions>.use.addFunction(RotateF);
		Singleton<ManagerFunctions>.use.addFunction(GameF);
		Init();
	}

	public void Init()
	{
		stepsRotate = 0;
		rotateTime = 0f;
		toBack = false;
		toRotate = false;
		isGame = true;
		prevStep = -1;
		step = 0;
	}

	public void SetPos(int n)
	{
		if (!isGame || step >= 9 || prevStep == n)
		{
			return;
		}
		if (n == rightSteps[step])
		{
			isGame = false;
			waitOnGround = true;
			toRotate = true;
			prevStep = n;
			stepsRotate++;
		}
		else if (step > 0)
		{
			timeOfBreak++;
			Singleton<Saves>.use.keep("timeOfBreak", timeOfBreak);
			Singleton<Sounds>.use.So2("endGame", 0.5f);
			toBack = true;
			audioSource.Play();
			isGame = false;
			moveToDown = true;
			LiftGame[] array = lifts;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].ToGo(n: false, 0, n);
			}
		}
	}

	public void BreakGame()
	{
		toBack = true;
		audioSource.Play();
		winGo = true;
		isGame = false;
		moveToDown = true;
		LiftGame[] array = lifts;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].ToGo(n: false, 0, 3);
		}
		Main.hero.SetAn(Main.hero.idleAn);
		Main.hero.speedX = 0f;
		Main.hero.run = false;
		Main.hero.idle = true;
	}

	private void GameF()
	{
		if (moveToDown)
		{
			for (int i = 0; i < lifts.Length; i++)
			{
				if (lifts[i].go)
				{
					return;
				}
			}
			audioSource.Stop();
			isGame = true;
			moveToDown = false;
			step = 0;
			prevStep = -1;
			if (winGo)
			{
				Main.hero.stopActions = false;
				winGo = false;
			}
		}
		if (Main.hero.transform.position.y < -4.5f && step > 0)
		{
			Singleton<Sounds>.use.So2("endGame", 0.5f);
			toBack = true;
			audioSource.Play();
			isGame = false;
			moveToDown = true;
			LiftGame[] array = lifts;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].ToGo(n: false, 0, -1);
			}
		}
		if (waitOnGround && Main.hero.onPlatform)
		{
			audioSource.Play();
			Singleton<Sounds>.use.So("plateGameRotate");
			Main.hero.SetAn(Main.hero.idleAn);
			Main.hero.speedX = 0f;
			Main.hero.run = false;
			Main.hero.idle = true;
			Main.hero.stopActions = true;
			waitOnGround = false;
			moveOnLifts = true;
			LiftGame[] array = lifts;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].ToGo(n: true, step, rightSteps[step]);
			}
			step++;
			if (step == 9 && timeOfBreak < 3)
			{
				Singleton<SteamF>.use.AddAch(10);
			}
		}
		if (!moveOnLifts)
		{
			return;
		}
		for (int k = 0; k < lifts.Length; k++)
		{
			if (lifts[k].go)
			{
				return;
			}
		}
		audioSource.Stop();
		isGame = true;
		moveOnLifts = false;
		Main.hero.stopActions = false;
	}

	private void RotateF()
	{
		if (toRotate)
		{
			float num = Time.deltaTime * 150f;
			gear1.Rotate(Vector3.forward * num);
			gear2.Rotate(Vector3.back * num);
			gear3.Rotate(Vector3.forward * num);
			gear4.Rotate(Vector3.back * num);
			gear5.Rotate(Vector3.forward * num);
			gear6.Rotate(Vector3.forward * num);
			gear7.Rotate(Vector3.back * num);
			rotateTime += num;
			if (rotateTime >= (float)(90 * stepsRotate))
			{
				toRotate = false;
			}
		}
		if (toBack)
		{
			float num2 = -1f * Time.deltaTime * 50f * (float)stepsRotate;
			gear1.Rotate(Vector3.forward * num2);
			gear2.Rotate(Vector3.back * num2);
			gear3.Rotate(Vector3.forward * num2);
			gear4.Rotate(Vector3.back * num2);
			gear5.Rotate(Vector3.forward * num2);
			gear6.Rotate(Vector3.forward * num2);
			gear7.Rotate(Vector3.back * num2);
			rotateTime += num2;
			if (rotateTime <= 0f)
			{
				toBack = false;
				stepsRotate = 0;
			}
		}
	}
}
