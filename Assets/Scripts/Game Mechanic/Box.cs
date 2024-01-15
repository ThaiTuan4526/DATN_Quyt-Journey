using UnityEngine;

public class Box : ActiveObj, InterfaceDragBox
{
	[HideInInspector]
	public Rigidbody2D rb;

	[HideInInspector]
	public bool isCatch;

	public GameObject blockObj;

	public float sizeOfBox;

	public float distPerObj;

	private AudioSource audioSource;

	private float idleTime;

	public float getsizeOfBox => sizeOfBox;

	public float getdistPerObj => distPerObj;

	public int getBlockID => blockObj.GetInstanceID();

	public bool getRbStatusFall()
	{
		if (rb.velocity.y < -1.2f)
		{
			return true;
		}
		return false;
	}

	private void Start()
	{
		InitBox();
	}

	protected void InitBox()
	{
		Init();
		rb = base.transform.GetComponent<Rigidbody2D>();
		audioSource = base.transform.GetComponent<AudioSource>();
		blockObj.SetActive(value: true);
		rb.bodyType = RigidbodyType2D.Kinematic;
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
				blockObj.SetActive(!isCatch);
				rb.bodyType = RigidbodyType2D.Dynamic;
				isCatch = true;
				OverObj(n: false);
			}
		}
		else
		{
			OverObj(n: true);
			Main.hero.catchHero.CatchBreak();
			blockObj.SetActive(!isCatch);
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
		isCatch = false;
		idleTime = 0f;
		audioSource.Pause();
		Singleton<ManagerFunctions>.use.addFunction(ReturnToKinematic);
	}

	private void ReturnToKinematic()
	{
		if (Mathf.Abs(rb.angularVelocity) < 0.01f && rb.velocity.magnitude < 0.01f)
		{
			idleTime += Time.deltaTime;
			if (idleTime > 0.2f)
			{
				rb.velocity = Vector2.zero;
				rb.bodyType = RigidbodyType2D.Kinematic;
				Singleton<ManagerFunctions>.use.removeFunction(ReturnToKinematic);
			}
		}
	}
}
