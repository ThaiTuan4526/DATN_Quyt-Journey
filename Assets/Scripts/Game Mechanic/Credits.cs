using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
	public Transform creditsObj;

	private float pauseStart;

	private void Start()
	{
		Singleton<LoaderBg>.use.HideLoader();
		Singleton<ManagerFunctions>.use.isPause = false;
		Cursor.visible = false;
	}

	private void Update()
	{
		if (pauseStart > 2f)
		{
			creditsObj.Translate(Vector3.up * Time.deltaTime);
			if (creditsObj.position.y > 38f)
			{
				creditsObj.position = new Vector2(0f, -7f);
			}
			if (Input.GetButtonDown("Cancel"))
			{
				SceneManager.LoadScene("Menu");
			}
		}
		else
		{
			pauseStart += Time.deltaTime;
		}
	}
}
