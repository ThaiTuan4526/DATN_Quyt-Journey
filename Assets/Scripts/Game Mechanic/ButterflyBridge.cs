using UnityEngine;

public class ButterflyBridge : MonoBehaviour, InterfaceBreakObj
{
	public Transform[] pointsT;

	public Transform monsterAim;

	public MonsterBridge monsterBridge;

	private float speed = 0.015f;

	private Vector3[] targets;

	private int nextPoint;

	internal bool goLoop;

	private float timeGo;

	private void Start()
	{
		targets = new Vector3[pointsT.Length];
		for (int i = 0; i < pointsT.Length; i++)
		{
			targets[i] = pointsT[i].position;
		}
		goLoop = true;
		Main.game.arrBreakObj.Add(this);
	}

	public void BreakObj()
	{
		goLoop = true;
		base.gameObject.SetActive(value: true);
		speed = 0.015f;
	}

	public void GoTo()
	{
		goLoop = false;
		speed = 0.1f;
		timeGo = 0f;
	}

	private void GoGo()
	{
		base.transform.Translate(Vector3.up * speed * 60f * Time.deltaTime);
		Vector3 vector = (goLoop ? targets[nextPoint] : monsterAim.position);
		Vector3 normalized = (vector - base.transform.position).normalized;
		float num = Mathf.Atan2(normalized.y, normalized.x) * 57.29578f;
		Quaternion b = default(Quaternion);
		b.eulerAngles = new Vector3(0f, 0f, num - 90f);
		float num2 = 1f;
		if (goLoop)
		{
			num2 = 0.025f + 0.1f * (1f - Quaternion.Dot(base.transform.rotation, b));
		}
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, Quaternion.Euler(0f, 0f, num - 90f), num2 * 60f * Time.deltaTime);
	}

	private void Update()
	{
		GoGo();
		if (goLoop)
		{
			if (Vector3.Distance(base.transform.position, targets[nextPoint]) < 0.1f)
			{
				nextPoint++;
				if (nextPoint >= targets.Length)
				{
					nextPoint = 0;
				}
			}
		}
		else if ((timeGo += 1f) > 10f && Vector3.Distance(base.transform.position, monsterAim.position) < 0.2f)
		{
			monsterBridge.SetWakeUp();
			base.gameObject.SetActive(value: false);
		}
	}
}
