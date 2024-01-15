using UnityEngine;
using UnityEngine.SceneManagement;

public class DeqafScene : MonoBehaviour
{
	private float t;

	public SpriteRenderer sp;

	private bool isSet;

	private int status = 1;

	private void Start()
	{
		sp.color = new Color(255f, 255f, 255f, 0f);
		Singleton<Sounds>.use.So2("deqaf", 0.7f);
		Singleton<LoaderBg>.use.HideLoader();
	}

	private void Update()
	{
		t += Time.deltaTime;
		if (status == 1)
		{
			sp.color = new Color(255f, 255f, 255f, t * 0.5f);
			if (t > 2f)
			{
				status = 2;
			}
		}
		else if (t >= 5.01f)
		{
			sp.color = new Color(255f, 255f, 255f, 6.01f - t);
		}
		if (!isSet && t >= 6.01f)
		{
			Singleton<LoaderBg>.use.SetLoader();
			isSet = true;
		}
		if (t >= 6.1f)
		{
			SceneManager.LoadScene("Menu");
			t = -100f;
		}
	}
}
