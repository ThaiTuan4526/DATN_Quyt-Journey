using UnityEngine;

public class BoardStone : MonoBehaviour
{
	public int type;

	public GameObject rune;

	public BoardGame boardGame;

	internal int pos;

	private Transform skinT;

	internal bool showed;

	private bool go;

	internal bool goBack;

	private bool falseGo;

	private float waitTime;

	private float targetPos;

	private Vector2 defPos;

	private bool soundsGo;

	private void Start()
	{
		pos = 1;
		rune.SetActive(value: false);
		skinT = base.gameObject.transform;
		defPos = skinT.localPosition;
	}

	public void ToShow()
	{
		if (!showed)
		{
			showed = true;
			base.gameObject.SetActive(value: true);
		}
	}

	public void Go()
	{
		if (pos < 6)
		{
			soundsGo = false;
			pos += type;
			if (pos > 6)
			{
				pos = 6;
			}
			GetPos();
			go = true;
			rune.SetActive(value: true);
			waitTime = 0f;
		}
		else
		{
			falseGo = true;
			waitTime = 0f;
		}
	}

	public void SetBack()
	{
		if (pos > 1)
		{
			goBack = true;
			pos = 1;
			GetPos();
		}
	}

	public void SetWin()
	{
		rune.SetActive(value: true);
	}

	private void GetPos()
	{
		targetPos = -2.432f + (float)(pos - 1) * 0.9548f;
	}

	private void Update()
	{
		if (go)
		{
			waitTime += Time.deltaTime;
			if (waitTime > 0.5f)
			{
				if (!soundsGo)
				{
					soundsGo = true;
					Singleton<Sounds>.use.So("moveStone");
				}
				skinT.localPosition = new Vector2(skinT.localPosition.x, skinT.localPosition.y + 3f * Time.deltaTime);
				if (skinT.localPosition.y > targetPos)
				{
					Singleton<Sounds>.use.StopS();
					skinT.localPosition = new Vector2(skinT.localPosition.x, targetPos);
					go = false;
					rune.SetActive(value: false);
					boardGame.CheckMove();
				}
			}
		}
		else if (goBack)
		{
			skinT.localPosition = new Vector2(skinT.localPosition.x, skinT.localPosition.y - 5f * Time.deltaTime);
			if (skinT.localPosition.y < targetPos)
			{
				skinT.localPosition = new Vector2(skinT.localPosition.x, targetPos);
				goBack = false;
			}
		}
		else if (falseGo)
		{
			waitTime += Time.deltaTime;
			if (waitTime > 0.5f)
			{
				falseGo = false;
				boardGame.EndMove();
			}
		}
	}
}
