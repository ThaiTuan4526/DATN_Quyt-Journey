using UnityEngine;

public class CollectMushrooms : MonoBehaviour
{
	public GameObject rightB;

	public GameObject leftB;

	public Bro bro;

	private int collected;

	private void Start()
	{
		collected = 0;
		Invoke("Init", 1f);
	}

	private void Init()
	{
		leftB.transform.position = new Vector2(Main.lCornerX * 0.01f, leftB.transform.position.y);
	}

	public void AddMush()
	{
		collected++;
		if (collected >= 4)
		{
			bro.SetButterfly();
			rightB.SetActive(value: false);
		}
	}
}
