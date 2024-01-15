using UnityEngine;

public class ButterflyHouse : MonoBehaviour
{
	public Transform[] pointsT;

	private float speed = 0.015f;

	private Vector3[] targets;

	private int nextPoint;

	private void Start()
	{
		targets = new Vector3[pointsT.Length];
		for (int i = 0; i < pointsT.Length; i++)
		{
			targets[i] = pointsT[i].localPosition;
		}
	}

	private void GoGo()
	{
		base.transform.Translate(Vector3.up * speed * 60f * Time.deltaTime);
		Vector3 normalized = (targets[nextPoint] - base.transform.localPosition).normalized;
		float num = Mathf.Atan2(normalized.y, normalized.x) * 57.29578f;
		Quaternion b = default(Quaternion);
		b.eulerAngles = new Vector3(0f, 0f, num - 90f);
		float num2 = 0.025f + 0.1f * (1f - Quaternion.Dot(base.transform.rotation, b));
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, Quaternion.Euler(0f, 0f, num - 90f), num2 * 60f * Time.deltaTime);
	}

	private void Update()
	{
		GoGo();
		if (Vector3.Distance(base.transform.localPosition, targets[nextPoint]) < 0.1f)
		{
			nextPoint++;
			if (nextPoint >= targets.Length)
			{
				nextPoint = 0;
			}
		}
	}
}
