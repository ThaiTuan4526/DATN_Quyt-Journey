using UnityEngine;

public class EndLevel4 : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D n)
	{
		if (Main.location == 4 && n.gameObject.CompareTag("Hero"))
		{
			Singleton<LoaderBg>.use.SetLoader();
			Singleton<Saves>.use.keep("level5", 1);
			Main.location = 5;
			Singleton<Saves>.use.SaveData();
			Main.game.InvokeNewLoc();
		}
	}
}
