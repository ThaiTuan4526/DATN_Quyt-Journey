using UnityEngine;

public class CutSceneLevel7 : MonoBehaviour
{
	public GameObject towerDoor1;

	public GameObject towerDoor2;

	public GameObject door1;

	public GameObject door2;

	public Transform palanqeen;

	public GirlOutside girl;

	public Animator girlJump;

	public Mpalankeen1 mpalankeen1;

	public Mpalankeen2 mpalankeen2;

	public GameObject roofMonster;

	public Animator fallMonster;

	public MonsterHunt monsterHunt;

	public GameObject butterfly;

	public Animator door;

	public GameObject mpalankeen1Butterfly;

	public GameObject mpalankeen2Butterfly;

	public GameObject hand1;

	public GameObject hand2;

	public GameObject viewObj1;

	public GameObject viewObj2;

	private bool sceneEnd;

	private int status;

	private float speed;

	private bool speedDown;

	internal bool startTrans;

	private float waitTime;

	private Vector2 pos1;

	private Vector2 pos2;

	private float lerpF;

	private float nextStop;

	private void Start()
	{
		mpalankeen1.InitMonster();
		mpalankeen2.InitMonster();
		towerDoor1.SetActive(value: true);
		towerDoor2.SetActive(value: false);
		door1.SetActive(value: true);
		door2.SetActive(value: false);
		mpalankeen1Butterfly.SetActive(value: false);
		mpalankeen2Butterfly.SetActive(value: false);
		palanqeen.gameObject.SetActive(value: false);
		butterfly.SetActive(value: false);
		girlJump.gameObject.SetActive(value: false);
		girl.gameObject.SetActive(value: false);
		monsterHunt.gameObject.SetActive(value: false);
		fallMonster.gameObject.SetActive(value: false);
		viewObj1.SetActive(value: false);
		viewObj2.SetActive(value: false);
		speed = 3f;
	}

	public void StartCutScene()
	{
		if (!sceneEnd)
		{
			sceneEnd = true;
			palanqeen.gameObject.SetActive(value: true);
			Main.hero.stopActions = true;
			Main.hero.SetDirect(n: false);
			Main.hero.SetAn("SeeGirl");
			Singleton<Sounds>.use.So("cutSceneWindow");
			Main.game.cam.SetPos(new Vector3(Main.hero.heroT.position.x - 1.5f, -0.5f, 0f));
			Main.game.cam.BreakFollow();
			status = 1;
			waitTime = 0f;
			palanqeen.position = new Vector3(palanqeen.position.x, -1.4f);
			mpalankeen1.SetAn("Run");
			mpalankeen2.SetAn("Run");
			Singleton<ManagerFunctions>.use.addFunction(CutScene);
		}
	}

	private void CutScene()
	{
		if (status == 1)
		{
			waitTime += Time.deltaTime;
			if (waitTime > 0.5f && Main.hero.AnCompleted())
			{
				status = 50;
				Main.hero.SetDirect(n: true);
				Main.hero.SetAn("Run");
			}
		}
		else if (status == 2)
		{
			palanqeen.Translate(speed * Time.deltaTime * Vector3.right);
			Main.game.cam.transform.Translate(3f * Time.deltaTime * Vector3.left);
			if (Main.game.cam.transform.position.x < 10f)
			{
				status = 3;
				Singleton<Sounds>.use.So("evilGo");
			}
		}
		else if (status == 3)
		{
			palanqeen.Translate(speed * Time.deltaTime * Vector3.right);
			if (palanqeen.position.x > 10f)
			{
				Main.game.cam.transform.position = new Vector3(palanqeen.position.x, Main.game.cam.transform.position.y, -10f);
			}
			if (!speedDown)
			{
				if (palanqeen.position.x > 44f)
				{
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
			if (palanqeen.position.x > 47.25f)
			{
				status = 4;
			}
		}
		else if (status == 4)
		{
			palanqeen.Translate(Vector3.down * Time.deltaTime);
			if (palanqeen.position.y < -1.7f)
			{
				palanqeen.position = new Vector3(palanqeen.position.x, -1.7f);
				status = 5;
				mpalankeen1.SetAn("Take2");
				mpalankeen2.SetAn("Take2");
				hand1.SetActive(value: false);
				hand2.SetActive(value: false);
			}
		}
		else if (status == 5)
		{
			if (mpalankeen1.AnCompleted())
			{
				mpalankeen1.SetDirect(n: true);
				mpalankeen1.SetAn("Run2");
				status = 6;
			}
		}
		else if (status == 6)
		{
			mpalankeen1.transform.Translate(Vector3.left * Time.deltaTime);
			if (mpalankeen1.transform.localPosition.x < -3f)
			{
				mpalankeen1.SetDirect(n: false);
				mpalankeen1.sp.sortingOrder = 500;
				status = 7;
			}
		}
		else if (status == 7)
		{
			mpalankeen1.transform.Translate(Vector3.right * Time.deltaTime);
			if (mpalankeen1.transform.localPosition.y > -0.143f)
			{
				mpalankeen1.transform.Translate(0.5f * Vector3.down * Time.deltaTime);
			}
			if (mpalankeen1.transform.localPosition.x > -1.6f)
			{
				mpalankeen1.transform.localPosition = new Vector3(-1.6f, -0.143f);
				status = 8;
				mpalankeen1.SetAn("OpenDoor");
				door1.SetActive(value: false);
			}
		}
		else if (status == 8)
		{
			if (mpalankeen1.an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
			{
				door2.SetActive(value: true);
			}
			if (mpalankeen1.AnCompleted())
			{
				status = 9;
				girlJump.gameObject.SetActive(value: true);
				girlJump.Play("Jump", -1, 0f);
			}
		}
		else if (status == 9)
		{
			if (girlJump.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
			{
				status = 10;
				girlJump.gameObject.SetActive(value: false);
				girl.gameObject.SetActive(value: true);
				girl.SetDirect(n: false);
				girl.SetAn("Run");
			}
		}
		else if (status == 10)
		{
			girl.transform.Translate(Vector3.left * Time.deltaTime);
			if (girl.transform.localPosition.x < -4f)
			{
				status = 11;
				girl.SetDirect(n: true);
				girl.SetAn("Butterfly");
				girl.an.speed = 0f;
				mpalankeen2.SetAn("Run2");
			}
		}
		else if (status == 11)
		{
			mpalankeen2.transform.Translate(Vector3.right * Time.deltaTime);
			if (mpalankeen2.transform.localPosition.x > 2.45f)
			{
				mpalankeen2.SetDirect(n: true);
				mpalankeen2.sp.sortingOrder = 500;
				status = 12;
			}
		}
		else if (status == 12)
		{
			mpalankeen2.transform.Translate(Vector3.left * Time.deltaTime);
			if (mpalankeen2.transform.localPosition.y > -0.084f)
			{
				mpalankeen2.transform.Translate(0.5f * Vector3.down * Time.deltaTime);
			}
			if (mpalankeen2.transform.localPosition.x < 1.8f)
			{
				mpalankeen2.transform.localPosition = new Vector3(1.8f, -0.084f);
				status = 13;
				mpalankeen1.SetAn("Fall");
				mpalankeen2.SetAn("Fall");
			}
		}
		else if (status == 13)
		{
			if (mpalankeen1.an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
			{
				roofMonster.gameObject.SetActive(value: false);
				fallMonster.gameObject.SetActive(value: true);
			}
			if (mpalankeen1.an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
			{
				status = 14;
				girl.an.speed = 1f;
			}
		}
		else if (status == 14)
		{
			if (startTrans)
			{
				butterfly.SetActive(value: true);
				status = 15;
				pos1 = butterfly.transform.position;
				pos2 = new Vector2(44.59f, -0.5f);
				lerpF = 0f;
			}
		}
		else if (status == 15)
		{
			lerpF += Time.deltaTime * 0.5f;
			butterfly.transform.position = Vector2.Lerp(pos1, pos2, lerpF);
			if (lerpF >= 1f)
			{
				pos1 = butterfly.transform.position;
				pos2 = new Vector2(47.69f, -2.14f);
				lerpF = 0f;
				status = 16;
			}
		}
		else if (status == 16)
		{
			lerpF += Time.deltaTime;
			if (lerpF >= 1f)
			{
				girl.SetAn("Command");
				status = 171;
				Singleton<Sounds>.use.So("com2_zvuk");
			}
		}
		else if (status == 171)
		{
			if (girl.AnCompleted())
			{
				status = 17;
				lerpF = 0f;
			}
		}
		else if (status == 17)
		{
			lerpF += Time.deltaTime * 2.5f;
			butterfly.transform.position = Vector2.Lerp(pos1, pos2, lerpF);
			if (lerpF >= 1f)
			{
				butterfly.SetActive(value: false);
				status = 18;
				fallMonster.gameObject.SetActive(value: false);
				monsterHunt.gameObject.SetActive(value: true);
				monsterHunt.SetAn("Turn");
			}
		}
		else if (status == 18)
		{
			if (monsterHunt.AnCompleted())
			{
				status = 19;
				monsterHunt.SetAn("Idle");
				lerpF = 0f;
			}
		}
		else if (status == 19)
		{
			lerpF += Time.deltaTime;
			if (lerpF >= 1f)
			{
				status = 20;
				girl.SetDirect(n: false);
				girl.SetAn("Run");
				monsterHunt.SetAn("Run");
			}
		}
		else if (status == 20)
		{
			girl.transform.Translate(Vector3.left * Time.deltaTime);
			if (girl.transform.localPosition.x < -4.2f)
			{
				monsterHunt.transform.Translate(2f * Vector3.left * Time.deltaTime);
			}
			if (girl.transform.localPosition.x < -4.97f)
			{
				girl.SetAn("Open");
				status = 21;
			}
		}
		else if (status == 21)
		{
			if (girl.an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
			{
				door.Play("OpenDoor");
				status = 22;
			}
		}
		else if (status == 22)
		{
			if (door.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
			{
				status = 23;
				girl.gameObject.SetActive(value: false);
			}
		}
		else if (status == 23)
		{
			monsterHunt.transform.Translate(2f * Vector3.left * Time.deltaTime);
			if (monsterHunt.transform.localPosition.x < -5f)
			{
				status = 35;
				monsterHunt.gameObject.SetActive(value: false);
				door.Play("CloseDoor");
				lerpF = 0f;
			}
		}
		else if (status == 35)
		{
			if (door.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
			{
				if (mpalankeen1.myAn == "Sleep" && mpalankeen1.AnCompleted())
				{
					mpalankeen1Butterfly.SetActive(value: true);
				}
				if (mpalankeen2.transform.localPosition.x > 0.79f)
				{
					mpalankeen2.transform.Translate(Vector3.left * Time.deltaTime * 0.5f);
				}
				else if (mpalankeen2.direct)
				{
					mpalankeen2.SetDirect(n: false);
					mpalankeen2.SetAn("Sleep");
					lerpF += 1f;
				}
				if (mpalankeen1.transform.localPosition.x < -1.188f)
				{
					mpalankeen1.transform.Translate(Vector3.right * Time.deltaTime * 0.5f);
				}
				else if (!mpalankeen1.direct)
				{
					mpalankeen1.SetDirect(n: true);
					mpalankeen1.SetAn("Sleep");
					lerpF += 1f;
				}
				if (lerpF >= 1.5f)
				{
					status = 25;
					lerpF = 0f;
				}
			}
		}
		else if (status == 25)
		{
			if (mpalankeen2.AnCompleted())
			{
				mpalankeen1Butterfly.SetActive(value: true);
				mpalankeen2Butterfly.SetActive(value: true);
				status = 24;
				lerpF = 0f;
			}
		}
		else if (status == 24)
		{
			lerpF += Time.deltaTime;
			if (lerpF >= 3f)
			{
				status = 26;
			}
		}
		else if (status == 26)
		{
			Main.game.cam.transform.position = new Vector3(Main.game.cam.transform.position.x - 6f * Time.deltaTime, Main.game.cam.transform.position.y, -10f);
			if (Main.game.cam.transform.position.x <= 27.07f)
			{
				status = 27;
				lerpF = 0f;
				Main.game.cam.transform.position = new Vector3(27.07f, Main.game.cam.transform.position.y, -10f);
			}
		}
		else if (status == 27)
		{
			lerpF += Time.deltaTime;
			if (lerpF >= 1f)
			{
				Singleton<Sounds>.use.So2("doorOpen_2", 0.4f);
				towerDoor1.SetActive(value: true);
				towerDoor2.SetActive(value: false);
				Main.game.cam.isFollow = true;
				Main.hero.ReturnToGame();
				Main.hero.SetDirect(n: true);
				Main.hero.SetAn("Idle");
				Main.hero.transform.position = new Vector2(26.62f, Main.hero.transform.position.y);
				Singleton<ManagerFunctions>.use.removeFunction(CutScene);
			}
		}
		else if (status == 50)
		{
			Main.hero.heroT.position = new Vector2(Main.hero.heroT.position.x + Main.hero.maxSpeed * 60f * Time.deltaTime, Main.hero.heroT.position.y);
			if (Main.hero.heroT.position.x >= 26.3f)
			{
				Singleton<Sounds>.use.So2("doorOpen_2", 0.4f);
				Main.hero.gameObject.SetActive(value: false);
				status = 2;
				towerDoor1.SetActive(value: false);
				towerDoor2.SetActive(value: true);
				viewObj1.SetActive(value: true);
				viewObj2.SetActive(value: true);
			}
		}
	}
}
