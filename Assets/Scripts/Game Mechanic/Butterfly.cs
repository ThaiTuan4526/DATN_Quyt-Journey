using UnityEngine;

public class Butterfly : MonoBehaviour
{
	public SpriteRenderer sp;

	public ParticleSystem ps;

	public Light lightObj;

	private const float speed = 0.02f;

	private Vector3 targetV = new Vector3(5.802f, 3.47f, 0f);

	private bool isEnd;

	private float n;

	private void Start()
	{
		lightObj.intensity = 0f;
		base.gameObject.SetActive(value: false);
	}

	public void Spawn()
	{
		base.gameObject.SetActive(value: true);
	}

	private void Update()
	{
		if (isEnd)
		{
			n += Time.deltaTime;
			if (n > 4f)
			{
				base.gameObject.SetActive(value: false);
			}
			return;
		}
		if (lightObj.intensity < 6f)
		{
			lightObj.intensity += 6f * Time.deltaTime;
		}
		base.transform.Translate(Vector3.up * 0.02f * 60f * Time.deltaTime);
		Vector3 normalized = (targetV - base.transform.position).normalized;
		float num = Mathf.Atan2(normalized.y, normalized.x) * 57.29578f;
		Quaternion b = default(Quaternion);
		b.eulerAngles = new Vector3(0f, 0f, num - 90f);
		float num2 = 0.015f + 0.1f * (1f - Quaternion.Dot(base.transform.rotation, b));
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, Quaternion.Euler(0f, 0f, num - 90f), num2 * 60f * Time.deltaTime);
		if (Vector3.Distance(base.transform.position, targetV) < 0.1f)
		{
			isEnd = true;
			sp.enabled = false;
			ps.Stop();
			lightObj.intensity = 0f;
		}
	}
}
