using UnityEngine;

public class Spike : MonoBehaviour
{
	private bool isDead;

	private float anGo;

	private void OnTriggerEnter2D(Collider2D n)
	{
		if (!isDead && n.gameObject.CompareTag("Hero"))
		{
			isDead = true;
			Main.hero.stopActions = true;
			anGo = 0f;
			Main.hero.SetAn("Spike");
			Singleton<ManagerFunctions>.use.addFunction(Dead);
			Singleton<Sounds>.use.So("spike");
		}
	}

	private void Dead()
	{
		anGo += Time.deltaTime;
		if (anGo > 1f && Main.hero.AnCompleted())
		{
			Singleton<ManagerFunctions>.use.removeFunction(Dead);
			isDead = false;
			Main.game.ReLoadScene();
		}
	}
}
