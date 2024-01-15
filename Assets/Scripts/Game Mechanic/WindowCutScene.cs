using UnityEngine;

public class WindowCutScene : MonoBehaviour
{
	private int status;

	internal WindowHome windowHome;

	private float goTime;

	private float waitTime;

	private GameObject movie;

	public Animator m1;

	public Animator m2;

	private bool toCage;

	private bool m1Idle;

	private void Start()
	{
		status = 1;
		goTime = 0f;
		movie = base.gameObject.transform.GetChild(0).gameObject;
		Main.game.canShowPauseTab = false;
		m1.Play("cageRun");
		Singleton<Sounds>.use.So("bang_zvuk");
		Singleton<InputChecker>.use.SetSpecialVibro(0.1f);
	}

	private void Update()
	{
		if (status == 1)
		{
			m1.gameObject.transform.localPosition = new Vector2(m1.gameObject.transform.localPosition.x - 2.3999999f * Time.deltaTime, m1.gameObject.transform.localPosition.y);
			if (!toCage && m1.gameObject.transform.localPosition.x < -3.01f)
			{
				m2.Play("cage");
				toCage = true;
			}
			if (m1.gameObject.transform.localPosition.x < -3.45f)
			{
				m1.Play("cage");
				status = 2;
				Singleton<Sounds>.use.So("setCage");
				Singleton<InputChecker>.use.SetSpecialVibro(0.15f);
			}
		}
		else if (status == 2)
		{
			if (m2.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
			{
				status = 4;
				waitTime = 0f;
				Singleton<InputChecker>.use.SetSpecialVibro(0.25f);
			}
		}
		else if (status == 4)
		{
			waitTime += Time.deltaTime;
			if (waitTime > 1f)
			{
				m2.Play("Run");
				m2.gameObject.transform.localPosition = new Vector2(m2.gameObject.transform.localPosition.x - 9.18f * Time.deltaTime, m2.gameObject.transform.localPosition.y + 0.032f);
				status = 3;
				Singleton<InputChecker>.use.SetSpecialVibro(0.35f);
			}
			if (!m1Idle && m1.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
			{
				m1Idle = true;
				m1.Play("Idle");
				m1.gameObject.GetComponent<SpriteRenderer>().flipX = true;
				m1.gameObject.transform.position = new Vector3(m1.gameObject.transform.position.x + 0.5f, m1.gameObject.transform.position.y, 0f);
			}
		}
		else if (status == 3)
		{
			if (m1.gameObject.GetComponent<SpriteRenderer>().flipX && m2.gameObject.transform.localPosition.x > 1f)
			{
				m1.gameObject.GetComponent<SpriteRenderer>().flipX = false;
			}
			m2.gameObject.transform.localPosition = new Vector2(m2.gameObject.transform.localPosition.x + 1.8f * Time.deltaTime, m2.gameObject.transform.localPosition.y);
			if (m2.gameObject.transform.localPosition.x > 3.4f)
			{
				Singleton<InputChecker>.use.StopVibro();
				Main.hero.gameObject.SetActive(value: true);
				Singleton<Sounds>.use.So("fallWindow");
				Singleton<Music>.use.MusicStart("forestCreepy");
				Main.hero.stopActions = true;
				Main.hero.SetAn("FallAss");
				Main.game.scenes["forest"].SetActive(value: true);
				windowHome.busket.gameObject.SetActive(value: true);
				windowHome.busket.ToFall();
				status = 100;
				goTime = 0f;
				movie.SetActive(value: false);
				Main.game.cam.isFollow = true;
			}
		}
		else if (status == 100)
		{
			goTime += Time.deltaTime;
			if (goTime > 0f && goTime < 0.4f)
			{
				Main.hero.heroT.position = new Vector2(Main.hero.heroT.position.x + 1.8f * Time.deltaTime, Main.hero.heroT.position.y);
			}
			if (goTime > 2f)
			{
				status = 101;
				goTime = 0f;
				Main.hero.stopActions = false;
				Main.hero.SetSomeAn("Stand");
			}
		}
		else if (status == 101)
		{
			goTime += Time.deltaTime;
			if (goTime > 0f && goTime < 0.4f)
			{
				Main.hero.heroT.position = new Vector2(Main.hero.heroT.position.x - 1.1999999f * Time.deltaTime, Main.hero.heroT.position.y);
			}
			if (goTime > 1f)
			{
				Main.game.canShowPauseTab = true;
				Object.Destroy(base.gameObject);
			}
		}
	}
}
