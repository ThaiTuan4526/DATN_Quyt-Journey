using UnityEngine;

public class ChangeUnderOffset : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D n)
	{
		if (Main.locationStep == 4 && n.gameObject.CompareTag("Hero"))
		{
			Main.game.cam.SwitchOffsetY(3f);
		}
	}
}
