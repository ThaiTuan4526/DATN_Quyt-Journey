using UnityEngine;

public class Jumper : MonoBehaviour
{
	public float power = 2.2f;

	public GameObject skin1;

	public GameObject skin2;

	public ParticleSystem effect;

	private float timeWait;

	private void Start()
	{
		skin2.SetActive(value: false);
		effect.Stop();
	}

	private void OnTriggerEnter2D(Collider2D n)
	{
		if (n.gameObject.CompareTag("Hero") && Main.hero.fall)
		{
			effect.Play();
			Main.hero.SetAn("Jump");
			Main.hero.rb.velocity = new Vector2(Main.hero.rb.velocity.x, 0f);
			Main.hero.rb.velocity = new Vector2(0f, Main.hero.rb.velocity.y);
			Main.hero.rb.AddForce(Main.hero.heroT.up * (power * Main.hero.jumpSpeed), ForceMode2D.Impulse);
			Main.hero.fall = false;
			Main.hero.jump = true;
			skin1.SetActive(value: false);
			skin2.SetActive(value: true);
			timeWait = 0f;
			Singleton<ManagerFunctions>.use.addFunction(Jump);
			Singleton<Sounds>.use.So("mushroomJump");
		}
	}

	private void Jump()
	{
		timeWait += Time.deltaTime;
		if (timeWait > 0.5f)
		{
			skin2.SetActive(value: false);
			skin1.SetActive(value: true);
			Singleton<ManagerFunctions>.use.removeFunction(Jump);
		}
	}
}
