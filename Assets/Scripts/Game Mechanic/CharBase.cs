using System.Collections.Generic;
using UnityEngine;

public class CharBase : MonoBehaviour
{
	protected int i;

	internal int iC;

	internal string myAn = "";

	internal GameObject heroObj;

	internal bool idle;

	protected internal bool run;

	internal bool jump;

	internal bool fall;

	internal bool classicAn;

	internal bool onPlatform;

	internal float speedX;

	public bool stopActions;

	internal bool direct;

	internal Animator an;

	internal SpriteRenderer sp;

	internal Rigidbody2D rb;

	protected Vector3 v3;

	internal string idleAn;

	internal string runAn;

	internal string runStartAn;

	internal string jumpAn;

	internal string fallAn;

	internal string fall2An;

	protected LayerMask groundLayer = 1024;

	internal LayerMask blockRightLayer = 2048;

	internal LayerMask blockLeftLayer = 4096;

	internal LayerMask blockBoxLayer = 16384;

	protected Collider2D[] colliders;

	protected Collider2D[] collidersObj;

	private float anTime;

	private Dictionary<string, Vector2> posAn = new Dictionary<string, Vector2>();

	protected void SetPos()
	{
		posAn.Add("Idle", new Vector2(0f, 0f));
		posAn.Add("Idle3", new Vector2(0f, 0f));
		posAn.Add("Jump", new Vector2(0f, 0f));
		posAn.Add("Landing", new Vector2(0f, 0f));
		posAn.Add("Idle1_2", new Vector2(0.026f, 0.011f));
		posAn.Add("Run", new Vector2(0f, 0.02f));
		posAn.Add("RunStart", new Vector2(0.012f, 0.018f));
		posAn.Add("Wall", new Vector2(0.146f, 0f));
		posAn.Add("Fear", new Vector2(-0.012f, 0f));
		posAn.Add("LandingFall", new Vector2(0.237f, -0.027f));
		posAn.Add("Take", new Vector2(0.346f, -0.016f));
		posAn.Add("NoActions", new Vector2(0f, 0f));
		posAn.Add("Watch", new Vector2(-0.022f, 0f));
		posAn.Add("View", new Vector2(0f, 0f));
		posAn.Add("ViewFear1", new Vector2(0.023f, 0f));
		posAn.Add("ViewFear2", new Vector2(-0.012f, 0f));
		posAn.Add("HandHi", new Vector2(0.148f, 0f));
		posAn.Add("Jump3", new Vector2(-0.043f, 0f));
		posAn.Add("Landing3", new Vector2(0.038f, 0f));
		posAn.Add("FallIdle", new Vector2(0.029f, -0.01f));
		posAn.Add("Idle2", new Vector2(-0.012f, 0f));
		posAn.Add("Jump2", new Vector2(0f, 0f));
		posAn.Add("Landing2", new Vector2(0f, 0f));
		posAn.Add("Run2", new Vector2(-0.033f, 0.023f));
		posAn.Add("RunStart2", new Vector2(0.078f, 0.018f));
		posAn.Add("CollectMush", new Vector2(0.332f, -0.033f));
		posAn.Add("PutMush", new Vector2(0.113f, 0f));
		posAn.Add("Knock", new Vector2(0.205f, 0f));
		posAn.Add("FallAss", new Vector2(0.073f, -0.026f));
		posAn.Add("Stand", new Vector2(-0.034f, -0.016f));
		posAn.Add("SeeUp", new Vector2(-0.008f, 0.004f));
		posAn.Add("SeeUp2", new Vector2(-0.008f, 0.004f));
		posAn.Add("windowSee", new Vector2(0.107f, 0.003f));
		posAn.Add("windowSee2", new Vector2(0.107f, 0.003f));
		posAn.Add("DragBox", new Vector2(0.025f, -0.057f));
		posAn.Add("TakeBox", new Vector2(0.135f, 0f));
		posAn.Add("TakeBox2", new Vector2(0.135f, 0f));
		posAn.Add("ToChest", new Vector2(-0.355f, 0.231f));
		posAn.Add("OutChest", new Vector2(-0.355f, 0.231f));
		posAn.Add("Swamp", new Vector2(-0.04f, 0f));
		posAn.Add("Lever", new Vector2(0.217f, 0f));
		posAn.Add("Spike", new Vector2(0.352f, -0.189f));
		posAn.Add("Dive", new Vector2(-0.141f, -0.382f));
		posAn.Add("Take2", new Vector2(0.171f, 0f));
		posAn.Add("OpenDoor", new Vector2(0.098f, 0f));
		posAn.Add("Rabbit", new Vector2(0.29f, 0f));
		posAn.Add("Needle", new Vector2(0.134f, 0f));
		posAn.Add("Well", new Vector2(0.134f, 0f));
		posAn.Add("Well2", new Vector2(0.134f, 0f));
		posAn.Add("Throw", new Vector2(0.206f, 0f));
		posAn.Add("Water", new Vector2(0.134f, 0f));
		posAn.Add("Water2", new Vector2(0.161f, 0f));
		posAn.Add("OpenDoor2", new Vector2(0.098f, 0f));
		posAn.Add("Bottle", new Vector2(0.243f, 0f));
		posAn.Add("Poison", new Vector2(0.231f, 0.104f));
		posAn.Add("HideCup", new Vector2(0.117f, -0.008f));
		posAn.Add("OutCup", new Vector2(0.117f, -0.008f));
		posAn.Add("Kindle", new Vector2(0.058f, -0.257f));
		posAn.Add("Kindle2", new Vector2(0.058f, -0.257f));
		posAn.Add("Kneel", new Vector2(-0.05f, -0.025f));
		posAn.Add("Kneel2", new Vector2(-0.05f, -0.025f));
		posAn.Add("DownStairs", new Vector2(0.014f, 0.011f));
		posAn.Add("Stairs", new Vector2(0.014f, 0.264f));
		posAn.Add("PutMushBowler", new Vector2(0.283f, 0f));
		posAn.Add("PipeGood", new Vector2(0.13f, 0f));
		posAn.Add("PipeBad", new Vector2(0.084f, 0f));
		posAn.Add("Hang", new Vector2(0.011f, -0.015f));
		posAn.Add("FlyDead", new Vector2(0.129f, 0f));
		posAn.Add("Sleep", new Vector2(-0.06f, 0.151f));
		posAn.Add("WakeUp", new Vector2(-0.06f, 0.151f));
		posAn.Add("WakeUpFear", new Vector2(-0.06f, 0.151f));
		posAn.Add("PopToFace", new Vector2(-0.381f, -0.042f));
		posAn.Add("HideBush", new Vector2(0.024f, 0f));
		posAn.Add("SeeGirl", new Vector2(0.016f, 0f));
		posAn.Add("Winch1", new Vector2(0.199f, -0.005f));
		posAn.Add("Winch2", new Vector2(0.199f, -0.005f));
		posAn.Add("Bell", new Vector2(0.116f, 0.056f));
		posAn.Add("Cord", new Vector2(0.087f, 0f));
		posAn.Add("Climb", new Vector2(0.021f, 0.034f));
		posAn.Add("Climb2", new Vector2(0.012f, 0.083f));
		posAn.Add("Fishing", new Vector2(0.913f, -1.159f));
		posAn.Add("FishingEnd", new Vector2(0.913f, -1.159f));
		posAn.Add("FishingLadder", new Vector2(1.54f, 1.432f));
		posAn.Add("ToScary", new Vector2(0.072f, 0.004f));
		posAn.Add("FishingLever", new Vector2(0.95f, -2.684f));
		posAn.Add("FishingPrey", new Vector2(0.924f, -0.682f));
		posAn.Add("SeeGirl2", new Vector2(0.016f, 0f));
		posAn.Add("Fly", new Vector2(0.039f, -0.054f));
		posAn.Add("Bank", new Vector2(0.255f, 0f));
		posAn.Add("Bank2", new Vector2(0.255f, 0f));
		posAn.Add("KillWorm", new Vector2(-0.015f, -0.007f));
		posAn.Add("FeedMonster", new Vector2(0.193f, 0f));
		posAn.Add("FeedFail", new Vector2(0.193f, 0f));
		posAn.Add("HugBro", new Vector2(0.477f, 0f));
		posAn.Add("Lower", new Vector2(-0.012f, 0.413f));
		posAn.Add("CareMonster", new Vector2(0.24f, 0f));
		posAn.Add("Connect", new Vector2(0f, 0f));
		posAn.Add("Butterfly", new Vector2(0.207f, 0f));
		posAn.Add("Vege", new Vector2(0.211f, 0f));
	}

	public void SetRootLayer()
	{
		sp.sortingLayerName = "hero";
		sp.sortingOrder = 0;
		rb.isKinematic = false;
	}

	public void SetDirect(bool n)
	{
		direct = n;
		sp.flipX = !n;
	}

	public bool AnCompleted()
	{
		if (Time.realtimeSinceStartup > anTime + 0.05f)
		{
			return an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;
		}
		return false;
	}

	public bool AnFrame(float n)
	{
		if (Time.realtimeSinceStartup > anTime + 0.05f)
		{
			return an.GetCurrentAnimatorStateInfo(0).normalizedTime >= n;
		}
		return false;
	}

	public void SetDirectAn(string n)
	{
		myAn = "";
		SetAn(n);
	}

	public void SetAn(string n)
	{
		if (myAn != n)
		{
			anTime = Time.realtimeSinceStartup;
			an.Play(n, -1, 0f);
			myAn = n;
			Vector2 vector = posAn[n];
			if (!direct)
			{
				vector.x *= -1f;
			}
			heroObj.transform.localPosition = vector;
		}
	}
}
