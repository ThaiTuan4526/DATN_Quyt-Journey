using UnityEngine;

public class BGmove : MonoBehaviour
{
	public Transform bg1;

	public Transform bg2;

	public Transform bg3;

	public Transform bgStatic;

	[HideInInspector]
	public Transform cam;

	public void Init()
	{
		cam = Main.game.cam.transform;
		Main.game.bgmoveEx = true;
	}

	private void Update()
	{
		SetPos();
	}

	internal void SetPos()
	{
		float x = cam.position.x;
		float y = cam.position.y;
		bg1.position = new Vector2(x * 0.8f, y);
		bg2.position = new Vector2(x * 0.4f, y);
		bg3.position = new Vector2(-0.51f * x, -0.7f * y);
		bgStatic.position = new Vector2(x, y);
	}
}
