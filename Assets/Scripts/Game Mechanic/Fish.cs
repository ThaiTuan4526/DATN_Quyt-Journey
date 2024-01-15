using UnityEngine;

public class Fish : MonoBehaviour
{
	public Animator an;

	private bool waitMode;

	private float waitTime;

	internal bool stopFish;

	private void Start()
	{
		waitMode = false;
		waitTime = 0f;
	}

	private void Update()
	{
		if (waitMode)
		{
			waitTime -= Time.deltaTime;
			if (!(waitTime <= 0f))
			{
				return;
			}
			if (stopFish)
			{
				base.gameObject.SetActive(value: false);
				return;
			}
			an.gameObject.transform.localPosition = new Vector2(5.7f + 5.3f * Random.value, an.gameObject.transform.localPosition.y);
			if (Random.value > 0.5f)
			{
				an.gameObject.GetComponent<SpriteRenderer>().flipX = true;
			}
			else
			{
				an.gameObject.GetComponent<SpriteRenderer>().flipX = false;
			}
			an.Play("Fish", -1, 0f);
			waitMode = false;
			if (Main.hero.heroT.position.x > 40f)
			{
				Singleton<Sounds>.use.So2("fishSplash", 0.5f);
			}
		}
		else if (an.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
		{
			waitTime = 1.5f + 1f * Random.value;
			waitMode = true;
		}
	}
}
