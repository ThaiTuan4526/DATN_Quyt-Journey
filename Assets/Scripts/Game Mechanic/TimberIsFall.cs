using UnityEngine;

public class TimberIsFall : MonoBehaviour
{
	public Timber timber;

	private void OnTriggerEnter2D(Collider2D n)
	{
		if (Main.locationStep != 4 && n.gameObject.CompareTag("Bro"))
		{
			Main.game.cam.SwitchOffsetY(-3f);
			Main.locationStep = 4;
			timber.SetDownMode();
			Object.Destroy(base.gameObject);
		}
	}
}
