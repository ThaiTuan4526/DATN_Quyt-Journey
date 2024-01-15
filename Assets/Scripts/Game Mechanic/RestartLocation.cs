using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLocation : MonoBehaviour
{
	private float waitTime;

	private void Update()
	{
		waitTime += Time.deltaTime;
		if (waitTime > 1f)
		{
			SceneManager.LoadScene("Game");
			waitTime = -100f;
		}
	}
}
