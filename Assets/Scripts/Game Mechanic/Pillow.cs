using UnityEngine;

public class Pillow : MonoBehaviour
{
	public Animator an;

	public BoxCollider2D keyCollider2D;

	public bool down;

	private void Start()
	{
		keyCollider2D.enabled = false;
	}

	private void OnTriggerStay2D(Collider2D n)
	{
		if (!down && n.gameObject.CompareTag("Hero") && Main.hero.fall)
		{
			Singleton<Sounds>.use.So("pillowDrop");
			keyCollider2D.enabled = true;
			an.Play("Fall");
			down = true;
		}
	}
}
