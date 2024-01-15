using UnityEngine;

public class CatchDream : MonoBehaviour
{
	public int dream;

	public BoxLever boxLever;

	private bool canCheck;

	private void Update()
	{
		if (boxLever.speed != 0f)
		{
			dream = 0;
			canCheck = false;
		}
		else
		{
			canCheck = true;
		}
	}

	private void OnTriggerStay2D(Collider2D n)
	{
		if (canCheck)
		{
			n.GetComponent<Dream>().SetMode(n: true);
			dream = n.GetComponent<Dream>().dream;
		}
		else
		{
			n.GetComponent<Dream>().SetMode(n: false);
		}
	}

	private void OnTriggerExit2D(Collider2D n)
	{
		n.GetComponent<Dream>().SetMode(n: false);
	}
}
