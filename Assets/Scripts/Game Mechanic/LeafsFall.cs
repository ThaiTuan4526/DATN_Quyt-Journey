using UnityEngine;

public class LeafsFall : MonoBehaviour
{
	private Transform camT;

	public void Init()
	{
		camT = Main.game.cam.gameObject.transform;
	}

	private void Update()
	{
		base.transform.position = new Vector3(camT.position.x, camT.position.y + 7f, 0f);
	}
}
