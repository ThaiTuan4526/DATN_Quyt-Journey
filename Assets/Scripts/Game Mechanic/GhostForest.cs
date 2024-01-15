using UnityEngine;

public class GhostForest : MonoBehaviour
{
	private GameObject ghost;

	private GameObject m1;

	private GameObject m2;

	private bool direct;

	private float waitTime;

	private int status = 1;

	private float speed;

	private float posShow;

	private void Start()
	{
		ghost = base.gameObject.transform.GetChild(0).gameObject;
		m1 = base.gameObject.transform.GetChild(1).gameObject;
		m2 = base.gameObject.transform.GetChild(2).gameObject;
		ghost.SetActive(value: false);
		Singleton<ManagerFunctions>.use.addFunction(Go);
		SetNewWait();
		status = 1;
	}

	private void Go()
	{
		if (status == 1)
		{
			waitTime -= Time.deltaTime;
			if (waitTime < 0f)
			{
				direct = Random.value > 0.5f;
				speed = 0.5f;
				m1.SetActive(direct);
				m2.SetActive(!direct);
				ghost.GetComponent<SpriteRenderer>().flipX = direct;
				posShow = 0.33f + Random.value * 0.33f;
				if (!direct)
				{
					speed *= -1f;
					posShow *= -1f;
				}
				status = 2;
				ghost.SetActive(value: true);
				if (direct)
				{
					ghost.transform.localPosition = new Vector2(-0.7f, ghost.transform.localPosition.y);
				}
				else
				{
					ghost.transform.localPosition = new Vector2(0.7f, ghost.transform.localPosition.y);
				}
			}
		}
		else if (status == 2)
		{
			ghost.transform.localPosition = new Vector2(ghost.transform.localPosition.x + speed * Time.deltaTime, ghost.transform.localPosition.y);
			if ((direct && ghost.transform.localPosition.x > posShow) || (!direct && ghost.transform.localPosition.x < posShow))
			{
				status = 3;
				waitTime = 2f + Random.value * 2f;
			}
		}
		else if (status == 3)
		{
			waitTime -= Time.deltaTime;
			if (waitTime < 0f)
			{
				status = 4;
			}
		}
		else if (status == 4)
		{
			ghost.transform.localPosition = new Vector2(ghost.transform.localPosition.x - speed * Time.deltaTime, ghost.transform.localPosition.y);
			if ((direct && ghost.transform.localPosition.x < -0.5f) || (!direct && ghost.transform.localPosition.x > 0.5f))
			{
				status = 1;
				ghost.SetActive(value: false);
				SetNewWait();
			}
		}
	}

	private void SetNewWait()
	{
		waitTime = 5f + Random.value * 10f;
	}
}
