using UnityEngine;

public class CandleFire : MonoBehaviour
{
	private Light lightObj;

	private bool direct;

	private float defaultPower;

	private float downBorder;

	private void Start()
	{
		lightObj = base.gameObject.GetComponent<Light>();
		defaultPower = lightObj.intensity;
		SetDownBorder();
		direct = true;
	}

	private void Update()
	{
		if (direct)
		{
			lightObj.intensity -= 4f * Time.deltaTime;
			if (lightObj.intensity < downBorder)
			{
				direct = false;
			}
			return;
		}
		lightObj.intensity += 4f * Time.deltaTime;
		if (lightObj.intensity > defaultPower)
		{
			direct = true;
			SetDownBorder();
		}
	}

	private void SetDownBorder()
	{
		downBorder = defaultPower - (0.1f + Random.value * 0.4f);
	}
}
