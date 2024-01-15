using UnityEngine;

public class WheelsStairsRotate : MonoBehaviour
{
	private Box box;

	public Transform wheel1;

	public Transform wheel2;

	public GameObject blockLadder;

	private void Start()
	{
		box = base.gameObject.GetComponent<Box>();
	}

	private void Update()
	{
		if (box.isCatch)
		{
			blockLadder.SetActive(value: true);
			if (Main.hero.an.speed > 0f)
			{
				if (Main.hero.goX < 0f)
				{
					wheel1.Rotate(Vector3.forward * Time.deltaTime * 600f);
					wheel2.Rotate(Vector3.forward * Time.deltaTime * 600f);
				}
				else
				{
					wheel1.Rotate(Vector3.back * Time.deltaTime * 600f);
					wheel2.Rotate(Vector3.back * Time.deltaTime * 600f);
				}
			}
		}
		else
		{
			blockLadder.SetActive(value: false);
		}
	}
}
