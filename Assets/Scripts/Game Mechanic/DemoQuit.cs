using UnityEngine;

public class DemoQuit : MonoBehaviour
{
	private int nOfPress;

	private void Update()
	{
		if (Input.anyKeyDown)
		{
			nOfPress++;
			if (nOfPress > 1)
			{
				Application.Quit();
			}
		}
	}
}
