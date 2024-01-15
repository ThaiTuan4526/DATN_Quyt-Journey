using UnityEngine;

public class ButterflyStart : MonoBehaviour
{
	public Transform broT;

	public Transform[] pointsT;

	public ParticleSystem particleSystem;

	private float speed = 0.05f;

	private Vector3[] targets;

	private int nextPoint;

	private bool go = true;

	private bool goHome;

	private void Start()
	{
		targets = new Vector3[pointsT.Length];
		for (int i = 0; i < pointsT.Length; i++)
		{
			targets[i] = pointsT[i].position;
		}
		base.gameObject.SetActive(value: false);
	}

	public void Spawn()
	{
		go = true;
		base.gameObject.SetActive(value: true);
	}

	public void SetWait(bool n)
	{
		go = !n;
		if (!go)
		{
			particleSystem.Stop();
		}
		else
		{
			particleSystem.Play();
		}
	}

	public void SetSpeedUp()
	{
		speed = 0.09f;
	}

	public void SetSlowUp()
	{
		speed = 0.06f;
	}

	private void GoGo()
	{
		base.transform.Translate(Vector3.up * speed * 60f * Time.deltaTime);
		Vector3 normalized = (targets[nextPoint] - base.transform.position).normalized;
		float num = Mathf.Atan2(normalized.y, normalized.x) * 57.29578f;
		Quaternion b = default(Quaternion);
		b.eulerAngles = new Vector3(0f, 0f, num - 90f);
		float num2 = 0.025f + 0.1f * (1f - Quaternion.Dot(base.transform.rotation, b));
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, Quaternion.Euler(0f, 0f, num - 90f), num2 * 60f * Time.deltaTime);
	}

	private void Update()
	{
		if (goHome)
		{
			if (go)
			{
				GoGo();
				if (Vector3.Distance(base.transform.position, targets[nextPoint]) < 0.1f)
				{
					base.gameObject.SetActive(value: false);
				}
			}
		}
		else
		{
			if (!go && !(base.transform.position.x - 1f < broT.position.x))
			{
				return;
			}
			GoGo();
			if (Vector3.Distance(base.transform.position, targets[nextPoint]) < 0.1f)
			{
				nextPoint++;
				if (nextPoint >= targets.Length - 1)
				{
					goHome = true;
					go = false;
				}
			}
		}
	}
}
