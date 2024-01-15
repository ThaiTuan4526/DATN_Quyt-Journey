using UnityEngine;

public class ChangeSizeCamera : MonoBehaviour
{
	public float desiredSize;

	private void OnTriggerEnter2D(Collider2D n)
	{
		if (n.gameObject.CompareTag("Hero"))
		{
			Main.game.cam.SetSmoothChangeSize(desiredSize);
			Object.Destroy(base.gameObject);
		}
	}
}
