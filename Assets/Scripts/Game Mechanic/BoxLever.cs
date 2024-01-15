using UnityEngine;

public class BoxLever : ActiveObj, InterfaceDragBox
{
	internal bool isCatch;

	public float sizeOfBox;

	public float distPerObj;

	public GameObject blocks1;

	public GameObject blocks2;

	public Transform dream1;

	public Transform dream2;

	internal float speed;

	private AudioSource audioSource;

	private const float dist = 14.5f;

	private bool sideMode;

	public float getsizeOfBox => sizeOfBox;

	public float getdistPerObj => distPerObj;

	public int getBlockID => base.gameObject.GetInstanceID();

	public bool getRbStatusFall()
	{
		return false;
	}

	private void Start()
	{
		InitBox();
		Init();
	}

	private void InitBox()
	{
		sideMode = true;
		audioSource = base.transform.GetComponent<AudioSource>();
		blocks1.SetActive(value: false);
		blocks2.SetActive(value: false);
		Singleton<ManagerFunctions>.use.addFunction(MoveDream);
	}

	public override void DoAction()
	{
		SetCatch();
	}

	public void SetCatch()
	{
		if (!isCatch)
		{
			if (Mathf.Abs(Main.hero.heroT.position.y - base.gameObject.transform.position.y) < 0.2f && Main.hero.catchHero.StartDragObj(base.gameObject.transform))
			{
				isCatch = true;
				blocks1.SetActive(Main.hero.direct);
				blocks2.SetActive(!Main.hero.direct);
			}
		}
		else
		{
			Main.hero.catchHero.CatchBreak();
		}
	}

	public void PlaySo(bool n)
	{
		if (n)
		{
			if (!audioSource.isPlaying)
			{
				audioSource.Play();
			}
		}
		else
		{
			audioSource.Pause();
		}
	}

	public void ReturnBlocks()
	{
		blocks1.SetActive(value: false);
		blocks2.SetActive(value: false);
		isCatch = false;
		audioSource.Pause();
	}

	private void MoveDream()
	{
		if (isCatch)
		{
			if (base.gameObject.transform.position.x > 2.52f)
			{
				speed = base.gameObject.transform.position.x - 2.52f;
				speed *= 0.05f;
				speed += 0.01f;
				speed *= 60f * Time.deltaTime;
			}
			else if (base.gameObject.transform.position.x < 2.12f)
			{
				speed = base.gameObject.transform.position.x - 2.12f;
				speed *= 0.05f;
				speed -= 0.01f;
				speed *= 60f * Time.deltaTime;
			}
			else
			{
				speed = 0f;
			}
		}
		dream1.Translate(Vector3.right * Time.timeScale * speed);
		if (speed > 0f)
		{
			if (sideMode)
			{
				if (dream1.position.x > 14.5f)
				{
					sideMode = !sideMode;
					dream1.position = new Vector2(dream2.position.x - 14.5f, 0f);
				}
			}
			else if (dream2.position.x > 14.5f)
			{
				sideMode = !sideMode;
				dream2.position = new Vector2(dream1.position.x - 14.5f, 0f);
			}
		}
		else if (!sideMode)
		{
			if (dream1.position.x < -14.5f)
			{
				sideMode = !sideMode;
				dream1.position = new Vector2(dream2.position.x + 14.5f, 0f);
			}
		}
		else if (dream2.position.x < -14.5f)
		{
			sideMode = !sideMode;
			dream2.position = new Vector2(dream1.position.x + 14.5f, 0f);
		}
		if (sideMode)
		{
			dream2.position = new Vector2(dream1.position.x - 14.5f, 0f);
		}
		else
		{
			dream2.position = new Vector2(dream1.position.x + 14.5f, 0f);
		}
	}
}
