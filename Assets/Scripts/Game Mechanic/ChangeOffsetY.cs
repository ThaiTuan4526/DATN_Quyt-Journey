using UnityEngine;

public class ChangeOffsetY : MonoBehaviour
{
	public float desiredOffset;

	private void OnTriggerEnter2D(Collider2D n)
	{
		if (n.gameObject.CompareTag("Hero"))
		{
			Main.game.cam.SwitchOffsetY(desiredOffset);
		}
	}
}
