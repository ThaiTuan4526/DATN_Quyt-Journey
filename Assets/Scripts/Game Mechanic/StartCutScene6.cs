using UnityEngine;

public class StartCutScene6 : MonoBehaviour
{
	public Transform palanqeen;

	public GirlOutside girl;

	public Animator girlJump;

	public Animator girlEnterAn;

	public GameObject girlEnter;

	public Mpalankeen1 mpalankeen1;

	public Mpalankeen2 mpalankeen2;

	public GameObject hand1;

	public GameObject hand2;

	public AudioSource heartSo;

	public GameObject door1;

	public GameObject door2;

	private float speed;

	private RectTransform dark;

	private bool startScene;

	private int status = 1;

	private float waitTime;

	private float volumeHeart;

	private bool volumeUp;

	private bool volumeDown;

	private float borderUp;

	private bool speedDown;

	private float nextStop;

	private void Start()
	{
		GameObject gameObject = Object.Instantiate(Resources.Load("Prefabs/UI/darkCutScene"), Main.game.canvasT, instantiateInWorldSpace: false) as GameObject;
		dark = gameObject.GetComponent<RectTransform>();
		dark.gameObject.SetActive(value: false);
		volumeHeart = 0f;
		girlEnter.SetActive(value: false);
		girlJump.gameObject.SetActive(value: false);
		girl.gameObject.SetActive(value: false);
		door2.SetActive(value: false);
		speed = 3f;
		speedDown = false;
	}

	private void OnTriggerEnter2D(Collider2D n)
	{
		if (!startScene && n.gameObject.CompareTag("Hero"))
		{
			startScene = true;
			Singleton<ManagerFunctions>.use.addFunction(CutScene);
			Singleton<ManagerFunctions>.use.addFunction(VolumeControl);
			mpalankeen1.SetAn("Run");
			mpalankeen2.SetAn("Run");
		}
	}

	private void VolumeControl()
	{
		if (volumeUp)
		{
			volumeHeart += Time.deltaTime * 0.1f;
			heartSo.volume = volumeHeart;
			dark.localScale = new Vector2(1.4f - volumeHeart * 0.6f, 1.4f - volumeHeart * 0.6f);
			if (volumeHeart >= borderUp)
			{
				volumeUp = false;
				if (borderUp >= 1f)
				{
					dark.localScale = new Vector2(0.8f, 0.8f);
					heartSo.volume = 1f;
				}
				else
				{
					dark.localScale = new Vector2(1.4f - borderUp * 0.6f, 1.4f - borderUp * 0.6f);
					heartSo.volume = borderUp;
				}
			}
		}
		if (volumeDown)
		{
			volumeHeart -= Time.deltaTime * 0.12f;
			heartSo.volume = volumeHeart;
			dark.localScale = new Vector2(1.4f - volumeHeart * 0.6f, 1.4f - volumeHeart * 0.6f);
			if (volumeHeart <= 0f)
			{
				volumeDown = false;
				dark.gameObject.SetActive(value: false);
				heartSo.volume = 0f;
				Main.hero.SetAn("Run");
				Singleton<ManagerFunctions>.use.removeFunction(VolumeControl);
				Singleton<ManagerFunctions>.use.addFunction(FinalRun);
			}
		}
	}

	private void FinalRun()
	{
		Main.hero.heroT.position = new Vector2(Main.hero.heroT.position.x + Main.hero.maxSpeed * 0.7f * 60f * Time.deltaTime, Main.hero.heroT.position.y);
		if (Main.hero.heroT.position.x > 84f)
		{
			Main.hero.sp.sortingLayerName = "hero";
			Main.hero.sp.sortingOrder = 0;
			Main.game.cam.isFollow = true;
			Main.hero.ReturnToGame();
			Main.hero.SetAn("Idle");
			Singleton<ManagerFunctions>.use.removeFunction(FinalRun);
		}
	}

	private void CutScene()
	{
		if (status == 1)
		{
			if (Main.hero.CanUse())
			{
				status = 2;
				Main.hero.stopActions = true;
				Main.hero.SetDirect(n: true);
				Main.hero.SetAn("SeeGirl");
				Singleton<Sounds>.use.So("cutSceneWindow");
			}
		}
		else if (status == 2)
		{
			if (Main.hero.AnCompleted())
			{
				Main.hero.SetAn("Run");
				status = 3;
			}
		}
		else if (status == 3)
		{
			Main.hero.heroT.position = new Vector2(Main.hero.heroT.position.x + Main.hero.maxSpeed * 60f * Time.deltaTime, Main.hero.heroT.position.y);
			if (Main.hero.heroT.position.x > 82f)
			{
				Main.hero.heroT.position = new Vector2(82f, Main.hero.heroT.position.y);
				status = 4;
				Main.game.cam.BreakFollow();
				Main.hero.SetAn("HideBush");
				Main.hero.sp.sortingLayerName = "gameobj";
				Main.hero.sp.sortingOrder = -3;
				waitTime = 0f;
			}
		}
		else if (status == 4)
		{
			Main.game.cam.transform.Translate(5f * Vector3.right * Time.deltaTime);
			if (Main.game.cam.transform.position.x > 90f)
			{
				status = 5;
				Singleton<Sounds>.use.So("evilGo");
			}
		}
		else if (status == 5)
		{
			palanqeen.Translate(speed * Vector3.left * Time.deltaTime);
			if (palanqeen.position.x < 90f)
			{
				if (Main.game.cam.transform.position.x > 82f)
				{
					Main.game.cam.transform.position = new Vector3(palanqeen.position.x, Main.game.cam.transform.position.y, -10f);
				}
				dark.gameObject.SetActive(value: true);
				volumeUp = true;
				borderUp = 0.4f;
				heartSo.Play();
			}
			if (!speedDown)
			{
				if (palanqeen.position.x < 80f)
				{
					Singleton<Sounds>.use.So2("bell", 0.5f);
					speedDown = true;
				}
				return;
			}
			if (speed > 0.5f)
			{
				nextStop += 60f * Time.deltaTime;
				if (nextStop > 1f)
				{
					speed *= 0.99f;
					nextStop = 0f;
				}
			}
			if (palanqeen.position.x < 77f)
			{
				status = 6;
			}
		}
		else if (status == 6)
		{
			palanqeen.Translate(Vector3.down * Time.deltaTime);
			if (palanqeen.position.y < -2.1f)
			{
				palanqeen.position = new Vector3(palanqeen.position.x, -2.1f);
				status = 7;
				mpalankeen1.SetAn("Take2");
				mpalankeen2.SetAn("Take2");
				hand1.SetActive(value: false);
				hand2.SetActive(value: false);
			}
		}
		else if (status == 7)
		{
			if (mpalankeen1.AnCompleted())
			{
				mpalankeen1.SetDirect(n: false);
				mpalankeen1.SetAn("Run2");
				status = 8;
			}
		}
		else if (status == 8)
		{
			mpalankeen1.transform.Translate(Vector3.right * Time.deltaTime);
			if (mpalankeen1.transform.localPosition.x > 3.4f)
			{
				mpalankeen1.SetDirect(n: true);
				mpalankeen1.sp.sortingOrder = 500;
				status = 9;
			}
		}
		else if (status == 9)
		{
			mpalankeen1.transform.Translate(Vector3.left * Time.deltaTime);
			if (mpalankeen1.transform.localPosition.y > 0.082f)
			{
				mpalankeen1.transform.Translate(0.5f * Vector3.down * Time.deltaTime);
			}
			if (mpalankeen1.transform.localPosition.x < 1.954f)
			{
				mpalankeen1.transform.localPosition = new Vector3(1.954f, 0.082f);
				status = 10;
				mpalankeen1.SetAn("OpenDoor");
				door1.SetActive(value: false);
			}
		}
		else if (status == 10)
		{
			if (mpalankeen1.an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
			{
				door2.SetActive(value: true);
			}
			if (mpalankeen1.AnCompleted())
			{
				status = 11;
				girlJump.gameObject.SetActive(value: true);
				girlJump.Play("Jump", -1, 0f);
				volumeUp = true;
				borderUp = 1f;
			}
		}
		else if (status == 11)
		{
			if (girlJump.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
			{
				status = 12;
				girlJump.gameObject.SetActive(value: false);
				girl.gameObject.SetActive(value: true);
				girl.SetDirect(n: false);
				girl.SetAn("Sniff");
			}
		}
		else if (status == 12)
		{
			if (girl.AnCompleted())
			{
				girl.SetDirect(n: true);
				girl.SetAn("Run");
				status = 13;
			}
		}
		else if (status == 13)
		{
			girl.transform.Translate(Vector3.right * Time.deltaTime);
			if (girl.transform.localPosition.x > 3.8f)
			{
				girl.SetAn("Sniff2");
				status = 14;
			}
		}
		else if (status == 14)
		{
			if (girl.AnCompleted())
			{
				girl.SetDirect(n: false);
				girl.SetAn("Run");
				status = 15;
			}
		}
		else if (status == 15)
		{
			girl.transform.Translate(Vector3.left * Time.deltaTime);
			if (girl.transform.localPosition.x < 0.475f)
			{
				girl.transform.localPosition = new Vector2(0.475f, girl.transform.localPosition.y);
				status = 151;
				mpalankeen1.SetAn("Run2");
				girl.SetDirect(n: true);
				girl.SetAn("Wait");
			}
		}
		else if (status == 151)
		{
			mpalankeen1.transform.Translate(Vector3.left * Time.deltaTime);
			if (mpalankeen1.transform.localPosition.x < 1.435f)
			{
				mpalankeen1.transform.localPosition = new Vector3(1.435f, 0.082f);
				status = 16;
				mpalankeen1.SetAn("GirlUp");
			}
		}
		else if (status == 16)
		{
			if (mpalankeen1.AnCompleted())
			{
				status = 17;
			}
		}
		else if (status == 17)
		{
			if (girlEnterAn.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
			{
				girlEnter.SetActive(value: false);
				status = 171;
				mpalankeen1.SetAn("Run2");
			}
		}
		else if (status == 171)
		{
			mpalankeen1.transform.Translate(Vector3.right * Time.deltaTime);
			if (mpalankeen1.transform.localPosition.x > 1.954f)
			{
				mpalankeen1.transform.localPosition = new Vector3(1.954f, 0.082f);
				mpalankeen1.SetAn("OpenDoor2");
				status = 18;
			}
		}
		else if (status == 18)
		{
			if (mpalankeen1.an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.15f)
			{
				door2.SetActive(value: false);
			}
			if (mpalankeen1.AnCompleted())
			{
				status = 19;
				mpalankeen1.SetDirect(n: false);
				mpalankeen1.SetAn("Run2");
				door1.SetActive(value: true);
			}
		}
		else if (status == 19)
		{
			mpalankeen1.transform.Translate(Vector3.right * Time.deltaTime);
			if (mpalankeen1.transform.localPosition.y < 0.188f)
			{
				mpalankeen1.transform.Translate(0.5f * Vector3.up * Time.deltaTime);
			}
			if (mpalankeen1.transform.localPosition.x > 3.4f)
			{
				status = 20;
				mpalankeen1.SetDirect(n: true);
				mpalankeen1.sp.sortingOrder = 10;
			}
		}
		else if (status == 20)
		{
			mpalankeen1.transform.Translate(Vector3.left * Time.deltaTime);
			if (mpalankeen1.transform.localPosition.x < 2.307f)
			{
				status = 21;
				mpalankeen1.SetAn("Take");
				mpalankeen2.SetAn("Take");
			}
		}
		else if (status == 21)
		{
			if (mpalankeen1.AnCompleted())
			{
				hand1.SetActive(value: true);
				hand2.SetActive(value: true);
				status = 22;
				mpalankeen1.SetAn("Run");
				mpalankeen2.SetAn("Run");
				volumeDown = true;
			}
		}
		else
		{
			if (status != 22)
			{
				return;
			}
			if (palanqeen.position.y < -1.7f)
			{
				palanqeen.Translate(Vector3.up * Time.deltaTime);
				return;
			}
			palanqeen.Translate(3f * Vector3.left * Time.deltaTime);
			if (palanqeen.position.x < 60f)
			{
				palanqeen.gameObject.transform.position = new Vector2(-150f, 150f);
				status = 13;
				Singleton<ManagerFunctions>.use.removeFunction(CutScene);
			}
		}
	}
}
