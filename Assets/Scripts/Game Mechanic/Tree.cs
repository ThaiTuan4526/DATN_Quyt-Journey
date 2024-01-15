using UnityEngine;

public class Tree : MonoBehaviour, InterfaceBreakObj
{
	private Transform heroT;

	private Hero heroS;

	public GameObject anTree;

	public Light lightOfTree;

	public Light lightOfTree2;

	public MonsterUnder monster;

	public float newJumpSpeed = 6.5f;

	private int nOf;

	public Transform[] leafs;

	private BoxCollider2D[] colliderLeaf;

	private FixedJoint2D[] fixedJoint2Ds;

	private bool isActive;

	private bool activateLeafs;

	private float nextLeaf;

	private int nextID;

	private LeafSkin[] skins;

	private AudioSource runMusic;

	private bool runDown;

	private bool canUse;

	private void Start()
	{
		isActive = false;
		activateLeafs = false;
		Invoke("Init", 0.1f);
	}

	private void Init()
	{
		heroT = Main.hero.heroT;
		heroS = Main.hero;
		nOf = leafs.Length;
		runMusic = base.gameObject.GetComponent<AudioSource>();
		colliderLeaf = new BoxCollider2D[nOf];
		fixedJoint2Ds = new FixedJoint2D[nOf];
		skins = new LeafSkin[nOf];
		for (int i = 0; i < nOf; i++)
		{
			colliderLeaf[i] = leafs[i].gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>();
			fixedJoint2Ds[i] = leafs[i].gameObject.transform.GetChild(0).gameObject.GetComponent<FixedJoint2D>();
			skins[i] = leafs[i].gameObject.transform.GetChild(0).gameObject.GetComponent<LeafSkin>();
		}
		for (int j = 0; j < nOf; j++)
		{
			leafs[j] = leafs[j].gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform;
		}
		isActive = true;
		SetLeafs(n: false);
		Main.game.arrBreakObj.Add(this);
	}

	public void BreakObj()
	{
		if (!monster.dead)
		{
			SetLeafs(n: false);
			activateLeafs = false;
			nextLeaf = 0f;
			nextID = 0;
		}
		runMusic.Stop();
		runDown = false;
	}

	public void SetLeafs(bool n)
	{
		if (!n)
		{
			for (int i = 0; i < nOf; i++)
			{
				fixedJoint2Ds[i].dampingRatio = 0f;
				fixedJoint2Ds[i].frequency = 1.2f;
				colliderLeaf[i].enabled = true;
				fixedJoint2Ds[i].gameObject.SetActive(value: false);
				fixedJoint2Ds[i].gameObject.SetActive(value: true);
				skins[i].SetSkin(n: false);
			}
			for (int j = 0; j < nOf; j++)
			{
				colliderLeaf[j].enabled = false;
			}
			canUse = false;
			anTree.SetActive(value: false);
			lightOfTree.gameObject.SetActive(value: false);
			lightOfTree2.gameObject.SetActive(value: false);
			anTree.GetComponent<Animator>().Play("idle");
		}
		else if (!activateLeafs)
		{
			activateLeafs = true;
			nextLeaf = 0f;
			nextID = -1;
			runMusic.Play();
			runMusic.volume = 0f;
		}
	}

	public void MonsterDead()
	{
		runDown = true;
	}

	private void Update()
	{
		if (runDown)
		{
			runMusic.volume -= 0.5f * Time.deltaTime;
			if (runMusic.volume < 0.1f)
			{
				runMusic.Stop();
				runDown = false;
			}
		}
		if (!isActive)
		{
			return;
		}
		if (activateLeafs)
		{
			if (runMusic.volume < 1f)
			{
				runMusic.volume += 0.7f * Time.deltaTime;
			}
			if (lightOfTree.intensity < 14f)
			{
				lightOfTree.intensity += 4f * Time.deltaTime;
			}
			if (lightOfTree2.intensity < 7f)
			{
				lightOfTree2.intensity += 4f * Time.deltaTime;
			}
			if (nextID == -1)
			{
				nextLeaf += Time.deltaTime;
				if (nextLeaf > 0.7f)
				{
					nextLeaf = 0f;
					nextID = 0;
					lightOfTree.gameObject.SetActive(value: true);
					lightOfTree2.gameObject.SetActive(value: true);
					lightOfTree.intensity = 0f;
					lightOfTree2.intensity = 0f;
					anTree.SetActive(value: true);
					anTree.GetComponent<Animator>().Play("Grow");
					Singleton<Sounds>.use.So("treeGrow");
					Singleton<Sounds>.use.So("tree");
					canUse = true;
				}
			}
			else
			{
				nextLeaf += Time.deltaTime;
				if (nextLeaf > 0.3f)
				{
					nextLeaf = 0f;
					fixedJoint2Ds[nextID].dampingRatio = 0.5f;
					fixedJoint2Ds[nextID].frequency = 5f;
					fixedJoint2Ds[nextID].gameObject.SetActive(value: false);
					fixedJoint2Ds[nextID].gameObject.SetActive(value: true);
					skins[nextID].SetSkin(n: true);
					nextID++;
					if (nextID >= nOf)
					{
						activateLeafs = false;
					}
				}
			}
		}
		if (!canUse)
		{
			return;
		}
		if (heroS.heroT.position.x > 49f && heroS.heroT.position.x < 54f && heroS.heroT.position.y > -8.1f)
		{
			heroS.jumpSpeed = newJumpSpeed;
		}
		else
		{
			heroS.jumpSpeed = 6f;
		}
		float num = heroT.position.y + 0.1f;
		if (heroS.jump)
		{
			return;
		}
		if (heroS.onPlatform)
		{
			for (int i = 0; i < nOf; i++)
			{
				if (leafs[i].position.y - 0.12f > num)
				{
					colliderLeaf[i].enabled = false;
				}
			}
			return;
		}
		for (int j = 0; j < nOf; j++)
		{
			if (leafs[j].position.y < num)
			{
				colliderLeaf[j].enabled = true;
			}
		}
	}
}
