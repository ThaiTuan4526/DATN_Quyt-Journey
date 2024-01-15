using UnityEngine;

public class SpiritsHelp : MonoBehaviour
{
	public Spirit[] spirits;

	private void OnTriggerStay2D(Collider2D n)
	{
		if (n.gameObject.CompareTag("Hero"))
		{
			Singleton<Sounds>.use.So("ghostStart");
			Main.hero.stopActions = true;
			Main.hero.SetDirect(n: true);
			Spirit[] array = spirits;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].HelpSpirit();
			}
			Object.Destroy(base.gameObject);
		}
	}
}
